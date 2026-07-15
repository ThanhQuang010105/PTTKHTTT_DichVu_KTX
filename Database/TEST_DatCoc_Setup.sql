-- ============================================================
-- FILE TEST: Chuẩn bị dữ liệu để test màn hình Tra cứu Đặt cọc
-- và Cập nhật hồ sơ lưu trú
-- ============================================================
USE KTX_HomeStay_Full;
GO

PRINT '── BƯỚC 1: DỌN DẸP DỮ LIỆU CŨ ──';
-- Chú ý: Dọn dẹp các phiếu đặt cọc mẫu (Mã bắt đầu bằng PDC)
DELETE FROM Phieu_Dat_Coc WHERE MaPhieu LIKE 'PDC%';
GO

PRINT '── BƯỚC 2: TẠO DỮ LIỆU MẪU (CHI NHÁNH, NHÂN VIÊN, KHÁCH HÀNG, PHÒNG) ──';

-- 1. Tạo 1 Chi nhánh mẫu
IF NOT EXISTS (SELECT 1 FROM Chi_Nhanh WHERE MaCN = 'CN001')
BEGIN
    INSERT INTO Chi_Nhanh(MaCN, TenChiNhanh, DiaChi, SDT, KhuVuc, TrangThai) 
    VALUES ('CN001', N'Chi nhánh KTX Trung Tâm', N'123 Đường A, Quận 1', '0901234567', N'Quận 1', N'Hoạt động');
END

-- 2. Tạo 1 Nhân viên mẫu
IF NOT EXISTS (SELECT 1 FROM Nhan_Vien WHERE MaNV = 'NV001')
BEGIN
    INSERT INTO Nhan_Vien(MaNV, MaCN, HoTen, SDT, Email, VaiTro)
    VALUES ('NV001', 'CN001', N'Trần Nhân Viên', '0912345678', 'nhanvien1@ktx.com', N'Quản lý');
END

-- 3. Tạo 5 Khách hàng mẫu (KH001 -> KH005)
DECLARE @j INT = 1;
WHILE @j <= 5
BEGIN
    DECLARE @MaKhach VARCHAR(20) = 'KH' + RIGHT('000' + CAST(@j AS VARCHAR), 3);
    DECLARE @CCCD VARCHAR(20) = '000111222' + RIGHT('000' + CAST(@j AS VARCHAR), 3);
    
    IF NOT EXISTS (SELECT 1 FROM Khach_hang WHERE maKH = @MaKhach)
    BEGIN
        INSERT INTO Khach_hang(maKH, hoTen, CCCD, SDT, email, gioiTinh)
        VALUES (
            @MaKhach, 
            CASE WHEN @j % 2 = 1 THEN N'Nguyễn Văn Khách ' ELSE N'Trần Thị Khách ' END + CAST(@j AS VARCHAR), 
            @CCCD, 
            '0909000' + RIGHT('000' + CAST(@j AS VARCHAR), 3), 
            'khach' + CAST(@j AS VARCHAR) + '@gmail.com', 
            CASE WHEN @j % 2 = 1 THEN N'Nam' ELSE N'Nữ' END
        );
    END
    SET @j = @j + 1;
END

-- 4. Tạo 3 Phòng mẫu (PH001 -> PH003)
SET @j = 1;
WHILE @j <= 3
BEGIN
    DECLARE @MaPhongTest VARCHAR(20) = 'PH' + RIGHT('000' + CAST(@j AS VARCHAR), 3);
    IF NOT EXISTS (SELECT 1 FROM Phong WHERE MaPhong = @MaPhongTest)
    BEGIN
        INSERT INTO Phong(MaPhong, MaCN, TenPhong, LoaiPhong, GiaThuePhong, TrangThaiPhong, SoLuongGiuong, GioiTinhQuyDinh)
        VALUES (
            @MaPhongTest, 
            'CN001', 
            N'Phòng ' + CAST(@j AS VARCHAR) + '01', 
            N'Phòng 8 giường', 
            2000000, 
            N'Trống',
            8,
            CASE WHEN @j % 2 = 1 THEN N'Nam' ELSE N'Nữ' END
        );
    END
    SET @j = @j + 1;
END

-- Đảm bảo các phòng test cũ cũng được cập nhật giới tính
UPDATE Phong SET GioiTinhQuyDinh = N'Nam' WHERE MaPhong IN ('PH001', 'PH003') AND GioiTinhQuyDinh IS NULL;
UPDATE Phong SET GioiTinhQuyDinh = N'Nữ' WHERE MaPhong = 'PH002' AND GioiTinhQuyDinh IS NULL;
GO

PRINT '── BƯỚC 3: TẠO 30 PHIẾU ĐẶT CỌC MẪU ──';
DECLARE @i INT = 1;

WHILE @i <= 30
BEGIN
    DECLARE @MaPhieu VARCHAR(20) = 'PDC' + RIGHT('000' + CAST(@i AS VARCHAR), 3);
    DECLARE @TrangThai NVARCHAR(50);
    
    -- Lấy ngẫu nhiên Phòng (1-3)
    DECLARE @phongIndex INT = (@i % 3) + 1;
    DECLARE @MaPhong VARCHAR(20) = 'PH' + RIGHT('000' + CAST(@phongIndex AS VARCHAR), 3);
    DECLARE @RoomGender NVARCHAR(10) = CASE WHEN @phongIndex % 2 = 1 THEN N'Nam' ELSE N'Nữ' END;
        
    -- Tạo một khách hàng đại diện duy nhất cho phiếu này để không bị trùng lặp nhóm
    DECLARE @maKH VARCHAR(20) = 'KHD_' + RIGHT('000' + CAST(@i AS VARCHAR), 3);
    IF NOT EXISTS (SELECT 1 FROM Khach_hang WHERE maKH = @maKH)
    BEGIN
        INSERT INTO Khach_hang(maKH, hoTen, CCCD, SDT, email, gioiTinh)
        VALUES (@maKH, N'Khách đại diện ' + CAST(@i AS VARCHAR), '0123' + CAST(@i AS VARCHAR), '09' + CAST(@i AS VARCHAR), 'kh' + CAST(@i AS VARCHAR) + '@a.com', @RoomGender);
    END
    
    DECLARE @MaNV VARCHAR(20) = 'NV001';

    DECLARE @SoGiuong INT = ABS(CHECKSUM(NEWID())) % 8 + 1; -- Số giường random 1 đến 8
    
    -- Phân chia trạng thái theo số lượng giường
    IF @SoGiuong = 1
    BEGIN
        IF @i % 3 = 1 SET @TrangThai = N'Chờ phê duyệt';
        ELSE IF @i % 3 = 2 SET @TrangThai = N'Đã duyệt';
        ELSE SET @TrangThai = N'Đã hủy / Quá hạn';
    END
    ELSE
    BEGIN
        IF @i % 4 = 1 SET @TrangThai = N'Chờ bổ sung';
        ELSE IF @i % 4 = 2 SET @TrangThai = N'Chờ phê duyệt';
        ELSE IF @i % 4 = 3 SET @TrangThai = N'Đã duyệt';
        ELSE SET @TrangThai = N'Đã hủy / Quá hạn';
    END

    IF NOT EXISTS (SELECT 1 FROM Phieu_Dat_Coc WHERE MaPhieu = @MaPhieu)
    BEGIN
        INSERT INTO Phieu_Dat_Coc (MaPhieu, maKH, MaNV, MaPhong, NgayDatCoc, SoTienCoc, SoGiuongCoc, HinhThucThanhToan, TrangThai)
        VALUES (
            @MaPhieu, 
            @maKH, 
            @MaNV, 
            @MaPhong, 
            DATEADD(DAY, -@i, GETDATE()), -- Ngày đặt cọc lùi dần về quá khứ
            CASE WHEN @i % 2 = 0 THEN 1000000.00 ELSE 500000.00 END, -- Giá tiền thay đổi
            @SoGiuong, -- Số giường random 1 đến 8
            CASE WHEN @i % 2 = 0 THEN N'Chuyển khoản' ELSE N'Tiền mặt' END, 
            @TrangThai
        );
        
        -- Nếu trạng thái là 'Chờ phê duyệt' hoặc 'Đã duyệt', tạo danh sách thành viên cho đủ số giường
        IF @TrangThai IN (N'Chờ phê duyệt', N'Đã duyệt')
        BEGIN
            DECLARE @MaNhomTest VARCHAR(20) = 'NT_PDC_' + CAST(@i AS VARCHAR);
            
            IF NOT EXISTS (SELECT 1 FROM Nhom_Thue WHERE MaNhom = @MaNhomTest)
            BEGIN
                -- Tạo nhóm thuê
                INSERT INTO Nhom_Thue(MaNhom, SoLuong, NguoiDaiDien) VALUES (@MaNhomTest, @SoGiuong, @maKH);
                
                -- Gán nhóm cho khách đại diện
                UPDATE Khach_hang SET MaNhom = @MaNhomTest WHERE maKH = @maKH;
                
                -- Tạo thêm các thành viên còn lại (nếu số giường > 1)
                DECLARE @k INT = 1;
                WHILE @k < @SoGiuong
                BEGIN
                    DECLARE @MemKH VARCHAR(20) = 'KHM_' + CAST(@i AS VARCHAR) + '_' + CAST(@k AS VARCHAR);
                    INSERT INTO Khach_hang(maKH, hoTen, CCCD, SDT, gioiTinh, MaNhom)
                    VALUES (@MemKH, N'Thành viên ' + CAST(@k AS VARCHAR) + N' phiếu ' + CAST(@i AS VARCHAR), '011' + CAST(@i AS VARCHAR) + CAST(@k AS VARCHAR), '088' + CAST(@i AS VARCHAR) + CAST(@k AS VARCHAR), @RoomGender, @MaNhomTest);
                    SET @k = @k + 1;
                END
            END
        END
    END
    
    SET @i = @i + 1;
END

PRINT '✅ Đã tạo 30 phiếu đặt cọc test thành công (Mã từ PDC001 đến PDC030)';
PRINT 'Trạng thái được phân bố: Chờ bổ sung, Đã duyệt, Đã hủy / Quá hạn.';
GO

-- ── BƯỚC 4: KIỂM TRA LẠI DỮ LIỆU ĐÃ TẠO VỚI QUERY CỦA FORM ──
-- Cột STT, Mã đặt cọc, Khách đại diện, Số điện thoại, Phòng/Giường cọc, Số giường đặt, Tiền cọc, Trạng thái
SELECT 
    ROW_NUMBER() OVER(ORDER BY pdc.NgayDatCoc DESC) AS [STT],
    pdc.MaPhieu AS [Mã đặt cọc],
    kh.hoTen AS [Khách đại diện],
    kh.SDT AS [Số điện thoại],
    p.TenPhong AS [Phòng/Giường cọc],
    pdc.SoGiuongCoc AS [Số giường đặt],
    pdc.SoTienCoc AS [Tiền cọc],
    pdc.TrangThai AS [Trạng thái]
FROM Phieu_Dat_Coc pdc
INNER JOIN Khach_hang kh ON pdc.maKH = kh.maKH
INNER JOIN Phong p ON pdc.MaPhong = p.MaPhong
WHERE pdc.MaPhieu LIKE 'PDC%'
ORDER BY pdc.NgayDatCoc DESC;
GO

-- �?m b?o kh�ch h�ng cu trong DB d�ng gi?i t�nh theo k?ch b?n (KH l? = Nam, KH ch?n = N?)
UPDATE Khach_hang SET gioiTinh = N'Nam' WHERE maKH IN ('KH001', 'KH003', 'KH005');
UPDATE Khach_hang SET gioiTinh = N'Nữ' WHERE maKH IN ('KH002', 'KH004');
GO

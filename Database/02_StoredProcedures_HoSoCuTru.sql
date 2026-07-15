USE KTX_HomeStay_Full;
GO

-- Cập nhật sp_GetPhieuDatCocByFilter để lấy thêm GioiTinhPhong
IF OBJECT_ID('sp_GetPhieuDatCocByFilter', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetPhieuDatCocByFilter;
GO

CREATE PROCEDURE sp_GetPhieuDatCocByFilter
    @maPhieu VARCHAR(50) = NULL,
    @sdt VARCHAR(15) = NULL,
    @cccd VARCHAR(20) = NULL,
    @trangThai NVARCHAR(50) = NULL
AS
BEGIN
    SELECT 
        ROW_NUMBER() OVER(ORDER BY p.NgayDatCoc DESC) AS STT,
        p.MaPhieu AS MaDatCoc,
        k.hoTen AS KhachDaiDien,
        k.SDT AS SoDienThoai,
        ph.TenPhong AS PhongGiuongCoc,
        p.SoGiuongCoc AS SoGiuongDat,
        p.SoTienCoc AS TienCoc,
        p.TrangThai AS TrangThai,
        p.MaPhieu AS MaPhieuGoc,
        ph.GioiTinhQuyDinh AS GioiTinhPhong
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    LEFT JOIN Phong ph ON p.MaPhong = ph.MaPhong
    WHERE 
        (@maPhieu IS NULL OR @maPhieu = '' OR p.MaPhieu LIKE '%' + @maPhieu + '%')
        AND (@sdt IS NULL OR @sdt = '' OR k.SDT LIKE '%' + @sdt + '%')
        AND (@cccd IS NULL OR @cccd = '' OR k.CCCD LIKE '%' + @cccd + '%')
        AND (@trangThai IS NULL OR @trangThai = '' OR p.TrangThai = @trangThai);
END
GO

-- SP2: Lấy số lượng người ở hiện tại của 1 phiếu đặt cọc
IF OBJECT_ID('sp_CountNguoiOByDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_CountNguoiOByDatCoc;
GO

CREATE PROCEDURE sp_CountNguoiOByDatCoc
    @maDatCoc VARCHAR(20)
AS
BEGIN
    DECLARE @maNhom VARCHAR(20);
    SELECT @maNhom = k.MaNhom 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
        SELECT COUNT(*) FROM Khach_hang WHERE MaNhom = @maNhom;
    ELSE
        SELECT 1;
END
GO

-- Thêm người mới vào hồ sơ cư trú
IF OBJECT_ID('sp_InsertHoSoCuTru', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertHoSoCuTru;
GO

CREATE PROCEDURE sp_InsertHoSoCuTru
    @maDatCoc VARCHAR(20),
    @hoTen NVARCHAR(100),
    @cccd VARCHAR(20),
    @sdt VARCHAR(15),
    @gioiTinh NVARCHAR(10),
    @queQuan NVARCHAR(50)
AS
BEGIN
    DECLARE @maNhom VARCHAR(20);
    DECLARE @maKHDaiDien VARCHAR(20);
    
    SELECT @maNhom = k.MaNhom, @maKHDaiDien = k.maKH 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    -- Nếu khách đại diện chưa có nhóm, tự tạo 1 nhóm
    IF @maNhom IS NULL
    BEGIN
        SET @maNhom = 'NT_' + LEFT(REPLACE(NEWID(), '-', ''), 10);
        INSERT INTO Nhom_Thue (MaNhom, SoLuong, NguoiDaiDien) VALUES (@maNhom, 1, @maKHDaiDien);
        UPDATE Khach_hang SET MaNhom = @maNhom WHERE maKH = @maKHDaiDien;
    END

    -- Sinh mã cư trú và mã KH ngẫu nhiên (vì DB dùng VARCHAR)
    DECLARE @maCuTruMoi VARCHAR(20) = 'CT_' + LEFT(REPLACE(NEWID(), '-', ''), 10);
    DECLARE @maKHMoi VARCHAR(20) = 'KH_' + LEFT(REPLACE(NEWID(), '-', ''), 10);

    INSERT INTO Thong_tin_cu_tru (maCuTru, Tinh, ngayKhaiBao, trangThaiTamTru)
    VALUES (@maCuTruMoi, @queQuan, GETDATE(), N'Chờ duyệt');
    
    INSERT INTO Khach_hang (maKH, hoTen, CCCD, SDT, gioiTinh, QuocTich, KhaNangTC, MaNhom, maCuTru)
    VALUES (@maKHMoi, @hoTen, @cccd, @sdt, @gioiTinh, @queQuan, N'Tốt', @maNhom, @maCuTruMoi);
END
GO

-- SP3: Lấy danh sách người ở trong 1 phiếu đặt cọc
IF OBJECT_ID('sp_GetHoSoCuTruByDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetHoSoCuTruByDatCoc;
GO

CREATE PROCEDURE sp_GetHoSoCuTruByDatCoc
    @maDatCoc VARCHAR(20)
AS
BEGIN
    DECLARE @maNhom VARCHAR(20);
    DECLARE @maKHDaiDien VARCHAR(20);
    
    SELECT @maNhom = k.MaNhom, @maKHDaiDien = k.maKH 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
    BEGIN
        SELECT 
            ROW_NUMBER() OVER(ORDER BY kh.maKH) AS STT,
            kh.hoTen,
            kh.CCCD,
            kh.SDT,
            kh.gioiTinh,
            ct.Tinh AS queQuan
        FROM Khach_hang kh
        LEFT JOIN Thong_tin_cu_tru ct ON kh.maCuTru = ct.maCuTru
        WHERE kh.MaNhom = @maNhom;
    END
    ELSE IF @maKHDaiDien IS NOT NULL
    BEGIN
        SELECT 
            1 AS STT,
            kh.hoTen,
            kh.CCCD,
            kh.SDT,
            kh.gioiTinh,
            ct.Tinh AS queQuan
        FROM Khach_hang kh
        LEFT JOIN Thong_tin_cu_tru ct ON kh.maCuTru = ct.maCuTru
        WHERE kh.maKH = @maKHDaiDien;
    END
END
GO

-- SP4: Cập nhật trạng thái phiếu đặt cọc
IF OBJECT_ID('sp_UpdateTrangThaiPhieuDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateTrangThaiPhieuDatCoc;
GO

CREATE PROCEDURE sp_UpdateTrangThaiPhieuDatCoc
    @maDatCoc VARCHAR(20),
    @trangThai NVARCHAR(50)
AS
BEGIN
    UPDATE Phieu_Dat_Coc
    SET TrangThai = @trangThai
    WHERE MaPhieu = @maDatCoc;
END
GO

-- Cập nhật sp_GetHoSoCuTruByDatCoc để trả về thêm maKH
IF OBJECT_ID('sp_GetHoSoCuTruByDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetHoSoCuTruByDatCoc;
GO

CREATE PROCEDURE sp_GetHoSoCuTruByDatCoc
    @maDatCoc VARCHAR(20)
AS
BEGIN
    DECLARE @maNhom VARCHAR(20);
    DECLARE @maKHDaiDien VARCHAR(20);
    
    SELECT @maNhom = k.MaNhom, @maKHDaiDien = k.maKH 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
    BEGIN
        SELECT 
            ROW_NUMBER() OVER(ORDER BY kh.maKH) AS STT,
            kh.maKH,
            kh.hoTen,
            kh.CCCD,
            kh.SDT,
            kh.gioiTinh,
            ct.Tinh AS queQuan
        FROM Khach_hang kh
        LEFT JOIN Thong_tin_cu_tru ct ON kh.maCuTru = ct.maCuTru
        WHERE kh.MaNhom = @maNhom;
    END
    ELSE IF @maKHDaiDien IS NOT NULL
    BEGIN
        SELECT 
            1 AS STT,
            kh.maKH,
            kh.hoTen,
            kh.CCCD,
            kh.SDT,
            kh.gioiTinh,
            ct.Tinh AS queQuan
        FROM Khach_hang kh
        LEFT JOIN Thong_tin_cu_tru ct ON kh.maCuTru = ct.maCuTru
        WHERE kh.maKH = @maKHDaiDien;
    END
END
GO

-- SP5: Xóa hồ sơ cư trú của 1 thành viên
IF OBJECT_ID('sp_DeleteHoSoCuTru', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteHoSoCuTru;
GO

CREATE PROCEDURE sp_DeleteHoSoCuTru
    @maKH VARCHAR(20)
AS
BEGIN
    DECLARE @maCuTru VARCHAR(20);
    SELECT @maCuTru = maCuTru FROM Khach_hang WHERE maKH = @maKH;

    -- Xóa thông tin khách hàng trước (do có khóa ngoại tới thông tin cư trú)
    DELETE FROM Khach_hang WHERE maKH = @maKH;

    -- Xóa thông tin cư trú
    IF @maCuTru IS NOT NULL
    BEGIN
        DELETE FROM Thong_tin_cu_tru WHERE maCuTru = @maCuTru;
    END
END
GO

-- Cập nhật lại sp_CountNguoiOByDatCoc
IF OBJECT_ID('sp_CountNguoiOByDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_CountNguoiOByDatCoc;
GO

CREATE PROCEDURE sp_CountNguoiOByDatCoc
    @maDatCoc VARCHAR(20)
AS
BEGIN
    DECLARE @maNhom VARCHAR(20);
    SELECT @maNhom = k.MaNhom 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
        SELECT COUNT(*) FROM Khach_hang WHERE MaNhom = @maNhom;
    ELSE
        SELECT 0;
END
GO

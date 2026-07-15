-- ==============================================================================
-- FILE TEST TỔNG HỢP - KTX HomeStay Dorm
-- Phủ toàn bộ chức năng: Đặt cọc, Hồ sơ cư trú, Hợp đồng, Tra phòng, Đối soát
-- Chạy 1 lần duy nhất để có đủ dữ liệu demo/test tất cả màn hình
-- ==============================================================================
USE KTX_HomeStay_Full;
GO

PRINT N'== BƯỚC 0: DỌN DẸP DỮ LIỆU TEST CŨ ==';
DELETE FROM Bang_Doi_Soat_Coc   WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Phi_Phat_Sinh        WHERE MaKyTT IN (SELECT MaKyTT FROM Ky_Thanh_Toan_Thue WHERE MaHopDong LIKE 'HDTEST%');
DELETE FROM Chi_So_Dien_Nuoc     WHERE MaKyTT IN (SELECT MaKyTT FROM Ky_Thanh_Toan_Thue WHERE MaHopDong LIKE 'HDTEST%');
DELETE FROM Ky_Thanh_Toan_Thue   WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Vat_Dung_Cap_Phat    WHERE MaBienBan IN (SELECT MaBienBan FROM Bien_Ban_Ban_Giao WHERE MaHopDong LIKE 'HDTEST%');
DELETE FROM Bien_Ban_Ban_Giao    WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Hop_Dong_Dich_Vu     WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Phieu_Thu            WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Hop_Dong_Thue        WHERE MaHopDong LIKE 'HDTEST%';
DELETE FROM Phieu_Dat_Coc        WHERE MaPhieu  LIKE 'PDCTEST%';
DELETE FROM Khach_hang           WHERE maKH     LIKE 'KHT%' OR maKH LIKE 'KHM_%';
DELETE FROM Nhom_Thue            WHERE MaNhom   LIKE 'NTT%' OR MaNhom LIKE 'NT_PDC_%';
DELETE FROM Thong_tin_cu_tru     WHERE maCuTru  LIKE 'CTT%';
DELETE FROM Lich_xem_phong       WHERE MaLich   LIKE 'LXT%';
DELETE FROM Dang_ky_thue         WHERE MaDK     LIKE 'DKT%';
DELETE FROM Giuong               WHERE MaGiuong LIKE 'GT%';
DELETE FROM Phong                WHERE MaPhong  LIKE 'PT%';
DELETE FROM Nhan_Vien            WHERE MaNV     LIKE 'NVT%';
DELETE FROM Chi_Nhanh            WHERE MaCN     LIKE 'CNT%';
PRINT N'✅ Đã dọn dữ liệu test cũ xong.';
GO

-- ==============================================================================
-- BƯỚC 1: DỮ LIỆU NỀN (Chi nhánh, Nhân viên 4 vai trò, Phòng, Giường)
-- ==============================================================================
PRINT N'== BƯỚC 1: TẠO DỮ LIỆU NỀN ==';

INSERT INTO Chi_Nhanh (MaCN, TenChiNhanh, DiaChi, SDT, Email, KhuVuc, TrangThai, NgayThanhLap)
VALUES
('CNT01', N'KTX HomeStay – CS Quận 1', N'227 Nguyễn Văn Cừ, Q1, TP.HCM', '0901111001', 'cs1@homestay.vn', N'Quận 1',       N'Hoạt động', '2020-01-15'),
('CNT02', N'KTX HomeStay – CS Bình Thạnh', N'15 Ung Văn Khiêm, Bình Thạnh', '0901111002', 'cs2@homestay.vn', N'Bình Thạnh', N'Hoạt động', '2021-06-01');

INSERT INTO Nhan_Vien (MaNV, MaCN, HoTen, SDT, Email, VaiTro, MatKhau)
VALUES
('NVT01', 'CNT01', N'Nguyễn Quản Lý',       '0911000001', 'quanly@homestay.vn',    N'Quản lý',              '123456'),
('NVT02', 'CNT01', N'Trần Kế Toán',          '0911000002', 'ketoan@homestay.vn',    N'Kế toán',              '123456'),
('NVT03', 'CNT01', N'Lê Văn Sale',           '0911000003', 'sale@homestay.vn',      N'Sale',                 '123456'),
('NVT04', 'CNT01', N'Phạm Phụ Trách',        '0911000004', 'nvphutrach@homestay.vn',N'Nhân viên phụ trách', '123456');

-- 4 Phòng (2 Nam, 2 Nữ)
INSERT INTO Phong (MaPhong, MaCN, TenPhong, KhuVuc, LoaiPhong, GioiTinhQuyDinh, SucChuaToiDa, GiaThuePhong, TrangThaiPhong, SoLuongGiuong)
VALUES
('PT101', 'CNT01', N'Phòng T101', N'Tầng 1', N'Phòng 4 người', N'Nam', 4, 1500000, N'Hoạt động', 4),
('PT102', 'CNT01', N'Phòng T102', N'Tầng 1', N'Phòng 4 người', N'Nữ',  4, 1500000, N'Hoạt động', 4),
('PT201', 'CNT01', N'Phòng T201', N'Tầng 2', N'Phòng đơn',     N'Nam', 1, 3000000, N'Hoạt động', 1),
('PT202', 'CNT01', N'Phòng T202', N'Tầng 2', N'Phòng đơn',     N'Nữ',  1, 3000000, N'Hoạt động', 1);

-- 10 giường
INSERT INTO Giuong (MaGiuong, MaPhong, TenGiuong, GiaThueGiuong, TrangThaiGiuong)
VALUES
('GT101A','PT101',N'Giường A',1500000,N'Đã thuê'),('GT101B','PT101',N'Giường B',1500000,N'Đã thuê'),
('GT101C','PT101',N'Giường C',1500000,N'Trống'),  ('GT101D','PT101',N'Giường D',1500000,N'Trống'),
('GT102A','PT102',N'Giường A',1500000,N'Đã thuê'),('GT102B','PT102',N'Giường B',1500000,N'Trống'),
('GT102C','PT102',N'Giường C',1500000,N'Trống'),  ('GT102D','PT102',N'Giường D',1500000,N'Trống'),
('GT201A','PT201',N'Giường Đơn',3000000,N'Đã thuê'),
('GT202A','PT202',N'Giường Đơn',3000000,N'Trống');

PRINT N'✅ Xong Bước 1.';
GO

-- ==============================================================================
-- BƯỚC 2: KHÁCH HÀNG (đa dạng quốc tịch, giới tính)
-- ==============================================================================
PRINT N'== BƯỚC 2: TẠO KHÁCH HÀNG ==';

INSERT INTO Thong_tin_cu_tru (maCuTru, diaChiThuongTru, Tinh, QuanHuyen, ngayKhaiBao, trangThaiTamTru)
VALUES
('CTT01',N'12 Lê Lợi, Q1',N'TP.HCM',N'Quận 1','2026-01-10',N'Đã đăng ký'),
('CTT02',N'45 Trần Phú',  N'Đà Nẵng',N'Hải Châu','2026-02-01',N'Đã đăng ký'),
('CTT03',N'99 Nguyễn Huệ',N'TP.HCM',N'Quận 3','2026-03-01',N'Đã đăng ký');

INSERT INTO Nhom_Thue (MaNhom, SoLuong, NguoiDaiDien)
VALUES ('NTT01',2,'KHT01'),('NTT02',1,'KHT03');

INSERT INTO Khach_hang (maKH, MaNhom, maCuTru, hoTen, CCCD, SDT, email, gioiTinh, QuocTich, KhaNangTC)
VALUES
('KHT01','NTT01','CTT01',N'Nguyễn Văn An',    '079101000001','0909111001','an.nv@gmail.com',    N'Nam',N'Việt Nam',  N'Tốt'),
('KHT02','NTT01','CTT02',N'Trần Thị Bích',    '079101000002','0909111002','bich.tt@gmail.com',  N'Nữ', N'Việt Nam',  N'Khá'),
('KHT03','NTT02','CTT03',N'Lê Minh Cường',    '079101000003','0909111003','cuong.lm@gmail.com', N'Nam',N'Việt Nam',  N'Khá'),
('KHT04', NULL,  NULL,   N'John Smith',        '079101000004','0909111004','john@gmail.com',     N'Nam',N'Mỹ',        N'Tốt'),
('KHT05', NULL,  NULL,   N'Võ Thị Thu Hương', '079101000005','0909111005','huong.vt@gmail.com', N'Nữ', N'Việt Nam',  N'Trung bình'),
('KHT06', NULL,  NULL,   N'Đỗ Hữu Trí',       '079101000006','0909111006','tri.dh@gmail.com',   N'Nam',N'Việt Nam',  N'Tốt'),
('KHT07', NULL,  NULL,   N'Phạm Hoàng Oanh',  '079101000007','0909111007','oanh.ph@gmail.com',  N'Nữ', N'Việt Nam',  N'Khá'),
('KHT08', NULL,  NULL,   N'Tanaka Yuki',       '079101000008','0909111008','yuki@jp.com',        N'Nữ', N'Nhật Bản', N'Tốt');

PRINT N'✅ Xong Bước 2.';
GO

-- ==============================================================================
-- BƯỚC 3: ĐĂNG KÝ THUÊ & LỊCH XEM PHÒNG (test màn hình Sale)
-- ==============================================================================
PRINT N'== BƯỚC 3: ĐĂNG KÝ THUÊ & LỊCH XEM PHÒNG ==';

INSERT INTO Dang_ky_thue (MaDK, SoNguoiDuKien, KhuVucMongMuon, HinhThucThue, LoaiPhongMongMuon, MucGiaMongMuon, ThoiGianDuKienVaoO, ThoiHanThue, TrangThai, NgayDangKy, GhiChu)
VALUES
('DKT01',1,N'Quận 1',N'Thuê giường',N'Phòng 4 người',N'1-2 triệu','2026-08-01',N'6 tháng',N'Chờ duyệt',  '2026-07-10',N'Muốn phòng tầng 1'),
('DKT02',2,N'Bình Thạnh',N'Thuê phòng',N'Phòng đơn', N'2-4 triệu','2026-08-15',N'1 năm',  N'Đã duyệt',   '2026-07-05',N'Ưu tiên phòng yên tĩnh'),
('DKT03',1,N'Quận 1',N'Thuê giường',N'Phòng 4 người',N'1-2 triệu','2026-09-01',N'3 tháng',N'Đã hủy',     '2026-06-20',N'Khách đổi kế hoạch'),
('DKT04',1,N'Quận 1',N'Thuê giường',N'Phòng 4 người',N'1-2 triệu','2026-08-01',N'6 tháng',N'Đã duyệt',   '2026-07-01',NULL);

INSERT INTO Lich_xem_phong (MaLich, MaDK, MaGiuong, ThoiGianHen, TrangThaiLich, GhiChu)
VALUES
('LXT01','DKT01','GT101C','2026-07-18 09:00:00',N'Chờ xác nhận',N'Khách muốn xem tầng 1'),
('LXT02','DKT02','GT201A','2026-07-20 14:30:00',N'Đã xác nhận', N'Khách đã được xem phòng'),
('LXT03','DKT04','GT101C','2026-07-16 10:00:00',N'Đã xem',      N'Khách hài lòng');

PRINT N'✅ Xong Bước 3.';
GO

-- ==============================================================================
-- BƯỚC 4: PHIẾU ĐẶT CỌC (đủ trạng thái để test màn hình tra cứu đặt cọc)
-- ==============================================================================
PRINT N'== BƯỚC 4: PHIẾU ĐẶT CỌC ==';

INSERT INTO Phieu_Dat_Coc (MaPhieu, maKH, MaNV, MaPhong, NgayDatCoc, SoTienCoc, SoGiuongCoc, HinhThucThanhToan, TrangThai)
VALUES
('PDCTEST01','KHT01','NVT03','PT101','2026-05-01',3000000,2,N'Chuyển khoản',N'Đã duyệt'),
('PDCTEST02','KHT03','NVT03','PT201','2026-06-01',3000000,1,N'Tiền mặt',    N'Đã duyệt'),
('PDCTEST03','KHT04','NVT03','PT102','2026-07-01',1500000,1,N'Chuyển khoản',N'Chờ phê duyệt'),
('PDCTEST04','KHT05','NVT03','PT101','2026-07-05',1500000,1,N'Tiền mặt',    N'Chờ bổ sung'),
('PDCTEST05','KHT06','NVT03','PT102','2026-06-15',1500000,1,N'Chuyển khoản',N'Đã hủy / Quá hạn'),
('PDCTEST06','KHT07','NVT03','PT202','2026-07-10',3000000,1,N'Tiền mặt',    N'Đã duyệt'),
('PDCTEST07','KHT08','NVT03','PT102','2026-07-12',1500000,1,N'Chuyển khoản',N'Chờ phê duyệt');

-- Bổ sung tạo thêm 30 phiếu đặt cọc để có nhiều dữ liệu cho tra cứu và cập nhật hồ sơ
DECLARE @i INT = 8;
WHILE @i <= 37
BEGIN
    DECLARE @MaPhieu VARCHAR(20) = 'PDCTEST' + RIGHT('00' + CAST(@i AS VARCHAR), 2);
    DECLARE @TrangThai NVARCHAR(50);
    
    DECLARE @phongIndex INT = (@i % 2) + 1;
    DECLARE @MaPhong VARCHAR(20) = CASE WHEN @phongIndex = 1 THEN 'PT101' ELSE 'PT102' END;
    DECLARE @RoomGender NVARCHAR(10) = CASE WHEN @phongIndex = 1 THEN N'Nam' ELSE N'Nữ' END;
        
    DECLARE @maKH VARCHAR(20) = 'KHT_PDC_' + CAST(@i AS VARCHAR);
    IF NOT EXISTS (SELECT 1 FROM Khach_hang WHERE maKH = @maKH)
    BEGIN
        INSERT INTO Khach_hang(maKH, hoTen, CCCD, SDT, email, gioiTinh)
        VALUES (@maKH, N'Khách đại diện ' + CAST(@i AS VARCHAR), '0123' + CAST(@i AS VARCHAR), '09' + CAST(@i AS VARCHAR), 'kh' + CAST(@i AS VARCHAR) + '@a.com', @RoomGender);
    END
    
    DECLARE @MaNV VARCHAR(20) = 'NVT03';
    DECLARE @SoGiuong INT = ABS(CHECKSUM(NEWID())) % 4 + 1;
    
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
            @MaPhieu, @maKH, @MaNV, @MaPhong, DATEADD(DAY, -@i, GETDATE()),
            CASE WHEN @i % 2 = 0 THEN 1500000.00 ELSE 500000.00 END,
            @SoGiuong, CASE WHEN @i % 2 = 0 THEN N'Chuyển khoản' ELSE N'Tiền mặt' END, @TrangThai
        );
        
        IF @TrangThai IN (N'Chờ phê duyệt', N'Đã duyệt')
        BEGIN
            DECLARE @MaNhomTest VARCHAR(20) = 'NT_PDC_' + CAST(@i AS VARCHAR);
            IF NOT EXISTS (SELECT 1 FROM Nhom_Thue WHERE MaNhom = @MaNhomTest)
            BEGIN
                INSERT INTO Nhom_Thue(MaNhom, SoLuong, NguoiDaiDien) VALUES (@MaNhomTest, @SoGiuong, @maKH);
                UPDATE Khach_hang SET MaNhom = @MaNhomTest WHERE maKH = @maKH;
                
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

PRINT N'✅ Xong Bước 4 – 37 phiếu đặt cọc (đủ 5 trạng thái).';
GO

-- ==============================================================================
-- BƯỚC 5: HỢP ĐỒNG THUÊ (test màn hình Nhân viên phụ trách & Kế toán)
-- ==============================================================================
PRINT N'== BƯỚC 5: HỢP ĐỒNG THUÊ ==';

INSERT INTO Hop_Dong_Thue (MaHopDong, maKH, MaPhong, MaPhieu, NgayLap, NgayBatDau, NgayKetThuc, GiaThue, TrangThai, KyThanhToan, QuyDinhHoanCoc, NoiQuy)
VALUES
('HDTEST01','KHT01','PT101','PDCTEST01','2026-05-05','2026-06-01','2026-12-01',1500000,N'Đang hiệu lực',N'Hàng tháng',N'Hoàn 100% nếu báo trước 30 ngày',N'Không hút thuốc, không nuôi thú cưng'),
('HDTEST02','KHT03','PT201','PDCTEST02','2026-06-05','2026-07-01','2027-07-01',3000000,N'Đang hiệu lực',N'Hàng tháng',N'Hoàn 80% nếu báo trước 30 ngày', N'Không hút thuốc'),
('HDTEST03','KHT07','PT202','PDCTEST06','2026-07-15','2026-08-01','2027-02-01',3000000,N'Chờ ký kết',  N'Hàng tháng',N'Hoàn 100% nếu báo trước 15 ngày',N'Không hút thuốc');

INSERT INTO Hop_Dong_Dich_Vu (MaHopDong, MaDV)
SELECT 'HDTEST01', MaDV FROM Dich_Vu WHERE MaDV IN ('DV001','DV002');

PRINT N'✅ Xong Bước 5 – 3 hợp đồng thuê.';
GO

-- ==============================================================================
-- BƯỚC 6: KỲ THANH TOÁN & PHÍ PHÁT SINH (test màn hình Kế toán)
-- ==============================================================================
PRINT N'== BƯỚC 6: KỲ THANH TOÁN & PHÍ PHÁT SINH ==';

INSERT INTO Ky_Thanh_Toan_Thue (MaKyTT, MaHopDong, NgayBatDauKy, NgayKetThucKy, SoTienPhaiTra, SoTienDaTra, SoTienConNo, NgayThanhToan, TrangThai)
VALUES
('KTTT01','HDTEST01','2026-06-01','2026-07-01',1500000,1500000,0,'2026-06-01',N'Đã đóng'),
('KTTT02','HDTEST01','2026-07-01','2026-08-01',1500000,1500000,0,'2026-07-02',N'Đã đóng'),
('KTTT03','HDTEST01','2026-08-01','2026-09-01',1500000,0,1500000,NULL,        N'Chưa đóng'),
('KTTT04','HDTEST02','2026-07-01','2026-08-01',3000000,3000000,0,'2026-07-01',N'Đã đóng'),
('KTTT05','HDTEST02','2026-08-01','2026-09-01',3000000,0,3000000,NULL,        N'Chưa đóng');

INSERT INTO Phi_Phat_Sinh (MaPhiPS, MaKyTT, LoaiPhi, MoTa, SoTien, NgayGhiNhan, NhanVienGhiNhan)
VALUES
('PPST01','KTTT02',N'Hư hỏng',N'Cửa tủ gãy bản lề', 250000,'2026-07-15','NVT01'),
('PPST02','KTTT02',N'Mất mát', N'Mất 1 thẻ từ phòng',100000,'2026-07-15','NVT01'),
('PPST03','KTTT04',N'Hư hỏng',N'Vỡ kính cửa sổ',    500000,'2026-07-20','NVT01');

INSERT INTO Chi_So_Dien_Nuoc (MaChiSo, MaPhong, MaKyTT, ThangNam, ChiSoDienDauKy, ChiSoDienCuoiKy, ChiSoNuocDauKy, ChiSoNuocCuoiKy, DonGiaDien, DonGiaNuoc, TrangThaiThanhToan)
VALUES
('CSDN01','PT101','KTTT02','07/2026',1200,1350,300,315,3500,12000,N'Đã thanh toán'),
('CSDN02','PT201','KTTT04','07/2026',500, 560, 100,108,3500,12000,N'Đã thanh toán');

PRINT N'✅ Xong Bước 6.';
GO

-- ==============================================================================
-- BƯỚC 7: BIÊN BẢN BÀN GIAO (test màn hình Quản lý)
-- ==============================================================================
PRINT N'== BƯỚC 7: BIÊN BẢN BÀN GIAO ==';

INSERT INTO Bien_Ban_Ban_Giao (MaBienBan, MaHopDong, NgayBanGiao, HienTrangKhuVuc, TrangThai, NguoiNhan, NguoiBanGiao)
VALUES
('BBT01','HDTEST01','2026-06-01',N'Phòng sạch sẽ, đầy đủ thiết bị',N'Đã ký kết',N'Nguyễn Văn An',   N'Lê Văn Sale'),
('BBT02','HDTEST02','2026-07-01',N'Phòng mới sơn, tốt',             N'Đã ký kết',N'Lê Minh Cường',   N'Lê Văn Sale');

INSERT INTO Vat_Dung_Cap_Phat (MaVatDung, MaBienBan, TenVatDung, SoLuong, TinhTrang, DonGiaDenBu)
VALUES
('VDT01','BBT01',N'Tủ quần áo',  1,N'Mới 100%',   2000000),
('VDT02','BBT01',N'Thẻ từ phòng',2,N'Hoạt động',   100000),
('VDT03','BBT02',N'Tủ đầu giường',1,N'Mới 90%',   500000),
('VDT04','BBT02',N'Thẻ từ phòng',1,N'Hoạt động',   100000);

PRINT N'✅ Xong Bước 7.';
GO

-- ==============================================================================
-- BƯỚC 8: ĐỐI SOÁT CỌC (test màn hình Kế toán – Đối soát)
-- Hợp đồng HDTEST01 ở trạng thái "Chờ đối soát" để test full flow
-- ==============================================================================
PRINT N'== BƯỚC 8: RESET HỢP ĐỒNG CHO TEST ĐỐI SOÁT ==';

UPDATE Hop_Dong_Thue SET TrangThai = N'Chờ đối soát' WHERE MaHopDong = 'HDTEST01';
-- Xóa đối soát cũ nếu có để chạy lại
DELETE FROM Bang_Doi_Soat_Coc WHERE MaHopDong = 'HDTEST01';

PRINT N'✅ HDTEST01 → Chờ đối soát. Vào màn hình Đối soát cọc, tìm 0909111001 để test.';
GO

-- ==============================================================================
-- BƯỚC 9: TÀI KHOẢN NHÂN VIÊN (đảm bảo đủ 4 role demo)
-- ==============================================================================
PRINT N'== BƯỚC 9: ĐẢM BẢO TÀI KHOẢN DEMO ==';

-- Đảm bảo các alias đăng nhập (email) đúng để NhanVienDAL tìm được
UPDATE Nhan_Vien SET Email = 'sale'       WHERE MaNV = 'NVT03';
UPDATE Nhan_Vien SET Email = 'quanly'     WHERE MaNV = 'NVT01';
UPDATE Nhan_Vien SET Email = 'ketoan'     WHERE MaNV = 'NVT02';
UPDATE Nhan_Vien SET Email = 'nvphutrach' WHERE MaNV = 'NVT04';

PRINT N'✅ Xong Bước 9.';
GO

-- ==============================================================================
-- KIỂM TRA TỔNG QUAN
-- ==============================================================================
PRINT N'== TỔNG QUAN DỮ LIỆU ĐÃ TẠO ==';
SELECT N'Chi nhánh'      AS [Bảng], COUNT(*) AS [Số dòng] FROM Chi_Nhanh       WHERE MaCN LIKE 'CNT%'
UNION ALL SELECT N'Nhân viên',COUNT(*) FROM Nhan_Vien     WHERE MaNV LIKE 'NVT%'
UNION ALL SELECT N'Khách hàng',COUNT(*) FROM Khach_hang   WHERE maKH LIKE 'KHT%'
UNION ALL SELECT N'Phòng',    COUNT(*) FROM Phong          WHERE MaPhong LIKE 'PT%'
UNION ALL SELECT N'Giường',   COUNT(*) FROM Giuong         WHERE MaGiuong LIKE 'GT%'
UNION ALL SELECT N'Đăng ký',  COUNT(*) FROM Dang_ky_thue  WHERE MaDK LIKE 'DKT%'
UNION ALL SELECT N'Lịch xem', COUNT(*) FROM Lich_xem_phong WHERE MaLich LIKE 'LXT%'
UNION ALL SELECT N'Đặt cọc',  COUNT(*) FROM Phieu_Dat_Coc  WHERE MaPhieu LIKE 'PDCTEST%'
UNION ALL SELECT N'Hợp đồng', COUNT(*) FROM Hop_Dong_Thue  WHERE MaHopDong LIKE 'HDTEST%'
UNION ALL SELECT N'Kỳ TT',    COUNT(*) FROM Ky_Thanh_Toan_Thue WHERE MaHopDong LIKE 'HDTEST%'
UNION ALL SELECT N'Phí PS',   COUNT(*) FROM Phi_Phat_Sinh   WHERE MaKyTT IN (SELECT MaKyTT FROM Ky_Thanh_Toan_Thue WHERE MaHopDong LIKE 'HDTEST%')
UNION ALL SELECT N'Biên bản', COUNT(*) FROM Bien_Ban_Ban_Giao WHERE MaHopDong LIKE 'HDTEST%';
GO

PRINT N'';
PRINT N'=====================================================';
PRINT N'✅ HOÀN TẤT! Dữ liệu test đã sẵn sàng.';
PRINT N'-----------------------------------------------------';
PRINT N'ĐĂNG NHẬP:  sale / quanly / ketoan / nvphutrach';
PRINT N'MẬT KHẨU:   123456';
PRINT N'-----------------------------------------------------';
PRINT N'KỊCH BẢN TEST GỢI Ý:';
PRINT N'[Sale]       Đăng ký: DKT01 | Đặt cọc: PDCTEST03 chờ duyệt';
PRINT N'[QuanLy]     Phê duyệt DKT02 | Biên bản BBT01 | Xem phòng PT101';
PRINT N'[KeToan]     Đối soát HDTEST01 (tìm 0909111001) | KỳTT KTTT03 chưa đóng';
PRINT N'[NvPhuTrach] Lập HĐ từ PDCTEST06 | Xem HĐ HDTEST02';
PRINT N'=====================================================';
GO

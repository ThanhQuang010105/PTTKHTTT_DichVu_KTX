USE HomeStayDorm;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.ChiNhanh WHERE SoDienThoai = N'02811110001')
BEGIN
    INSERT INTO dbo.ChiNhanh (TenChiNhanh, KhuVuc, DiaChi, SoDienThoai)
    VALUES (N'HomeStay Dorm Quận 1', N'Quận 1', N'12 Nguyễn Trãi, Quận 1, TP.HCM', N'02811110001');
END

IF NOT EXISTS (SELECT 1 FROM dbo.ChiNhanh WHERE SoDienThoai = N'02822220002')
BEGIN
    INSERT INTO dbo.ChiNhanh (TenChiNhanh, KhuVuc, DiaChi, SoDienThoai)
    VALUES (N'HomeStay Dorm Bình Thạnh', N'Bình Thạnh', N'88 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP.HCM', N'02822220002');
END

IF NOT EXISTS (SELECT 1 FROM dbo.ChiNhanh WHERE SoDienThoai = N'02833330003')
BEGIN
    INSERT INTO dbo.ChiNhanh (TenChiNhanh, KhuVuc, DiaChi, SoDienThoai)
    VALUES (N'HomeStay Dorm Thủ Đức', N'Thủ Đức', N'25 Võ Văn Ngân, Thủ Đức, TP.HCM', N'02833330003');
END

UPDATE dbo.ChiNhanh
SET TenChiNhanh = N'HomeStay Dorm Quận 1',
    KhuVuc = N'Quận 1',
    DiaChi = N'12 Nguyễn Trãi, Quận 1, TP.HCM',
    TrangThai = N'Hoạt động'
WHERE SoDienThoai = N'02811110001';

UPDATE dbo.ChiNhanh
SET TenChiNhanh = N'HomeStay Dorm Bình Thạnh',
    KhuVuc = N'Bình Thạnh',
    DiaChi = N'88 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP.HCM',
    TrangThai = N'Hoạt động'
WHERE SoDienThoai = N'02822220002';

UPDATE dbo.ChiNhanh
SET TenChiNhanh = N'HomeStay Dorm Thủ Đức',
    KhuVuc = N'Thủ Đức',
    DiaChi = N'25 Võ Văn Ngân, Thủ Đức, TP.HCM',
    TrangThai = N'Hoạt động'
WHERE SoDienThoai = N'02833330003';
GO

DECLARE @DefaultPasswordHash NVARCHAR(128) = N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92';
DECLARE @SeedChiNhanhQ1 INT = (SELECT MaChiNhanh FROM dbo.ChiNhanh WHERE SoDienThoai = N'02811110001');

IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE TenDangNhap = N'sale')
BEGIN
    INSERT INTO dbo.NhanVien (HoTen, TenDangNhap, Email, SoDienThoai, VaiTro, MaChiNhanh, MatKhauHash, TrangThai, NgayVaoLam)
    VALUES (N'Nhân viên Sale Demo', N'sale', N'sale@homestaydorm.local', N'0901000001', N'Sale', @SeedChiNhanhQ1, @DefaultPasswordHash, N'Đang làm', CONVERT(date, GETDATE()));
END

IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE TenDangNhap = N'quanly')
BEGIN
    INSERT INTO dbo.NhanVien (HoTen, TenDangNhap, Email, SoDienThoai, VaiTro, MaChiNhanh, MatKhauHash, TrangThai, NgayVaoLam)
    VALUES (N'Quản lý Demo', N'quanly', N'quanly@homestaydorm.local', N'0901000002', N'QuanLy', @SeedChiNhanhQ1, @DefaultPasswordHash, N'Đang làm', CONVERT(date, GETDATE()));
END

IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE TenDangNhap = N'ketoan')
BEGIN
    INSERT INTO dbo.NhanVien (HoTen, TenDangNhap, Email, SoDienThoai, VaiTro, MaChiNhanh, MatKhauHash, TrangThai, NgayVaoLam)
    VALUES (N'Kế toán Demo', N'ketoan', N'ketoan@homestaydorm.local', N'0901000003', N'KeToan', @SeedChiNhanhQ1, @DefaultPasswordHash, N'Đang làm', CONVERT(date, GETDATE()));
END

UPDATE dbo.NhanVien
SET MatKhauHash = @DefaultPasswordHash,
    TrangThai = N'Đang làm'
WHERE TenDangNhap IN (N'sale', N'quanly', N'ketoan');
GO

DECLARE @ChiNhanhQ1 INT = (SELECT MaChiNhanh FROM dbo.ChiNhanh WHERE TenChiNhanh = N'HomeStay Dorm Quận 1');
DECLARE @ChiNhanhBT INT = (SELECT MaChiNhanh FROM dbo.ChiNhanh WHERE TenChiNhanh = N'HomeStay Dorm Bình Thạnh');
DECLARE @ChiNhanhTD INT = (SELECT MaChiNhanh FROM dbo.ChiNhanh WHERE TenChiNhanh = N'HomeStay Dorm Thủ Đức');

IF NOT EXISTS (SELECT 1 FROM dbo.Phong WHERE TenPhong = N'A101')
BEGIN
    INSERT INTO dbo.Phong (MaChiNhanh, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChua, GiaThue, TrangThaiPhong)
    VALUES (@ChiNhanhQ1, N'A101', N'Quận 1', N'Nam', N'Tiêu chuẩn', 4, 3500000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Phong WHERE TenPhong = N'A102')
BEGIN
    INSERT INTO dbo.Phong (MaChiNhanh, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChua, GiaThue, TrangThaiPhong)
    VALUES (@ChiNhanhQ1, N'A102', N'Quận 1', N'Nữ', N'Tiêu chuẩn', 4, 3600000, N'Đang ghép');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Phong WHERE TenPhong = N'B201')
BEGIN
    INSERT INTO dbo.Phong (MaChiNhanh, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChua, GiaThue, TrangThaiPhong)
    VALUES (@ChiNhanhBT, N'B201', N'Bình Thạnh', N'Không yêu cầu', N'Studio', 2, 6500000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Phong WHERE TenPhong = N'C301')
BEGIN
    INSERT INTO dbo.Phong (MaChiNhanh, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChua, GiaThue, TrangThaiPhong)
    VALUES (@ChiNhanhTD, N'C301', N'Thủ Đức', N'Nam', N'Cao cấp', 6, 5200000, N'Trống');
END

UPDATE dbo.Phong
SET MaChiNhanh = @ChiNhanhQ1,
    KhuVuc = N'Quận 1',
    GioiTinhQuyDinh = N'Nam',
    LoaiPhong = N'Tiêu chuẩn',
    SucChua = 4,
    GiaThue = 3500000,
    TrangThaiPhong = N'Trống'
WHERE TenPhong = N'A101';

UPDATE dbo.Phong
SET MaChiNhanh = @ChiNhanhQ1,
    KhuVuc = N'Quận 1',
    GioiTinhQuyDinh = N'Nữ',
    LoaiPhong = N'Tiêu chuẩn',
    SucChua = 4,
    GiaThue = 3600000,
    TrangThaiPhong = N'Đang ghép'
WHERE TenPhong = N'A102';

UPDATE dbo.Phong
SET MaChiNhanh = @ChiNhanhBT,
    KhuVuc = N'Bình Thạnh',
    GioiTinhQuyDinh = N'Không yêu cầu',
    LoaiPhong = N'Studio',
    SucChua = 2,
    GiaThue = 6500000,
    TrangThaiPhong = N'Trống'
WHERE TenPhong = N'B201';

UPDATE dbo.Phong
SET MaChiNhanh = @ChiNhanhTD,
    KhuVuc = N'Thủ Đức',
    GioiTinhQuyDinh = N'Nam',
    LoaiPhong = N'Cao cấp',
    SucChua = 6,
    GiaThue = 5200000,
    TrangThaiPhong = N'Trống'
WHERE TenPhong = N'C301';
GO

DECLARE @PhongA101 INT = (SELECT MaPhong FROM dbo.Phong WHERE TenPhong = N'A101');
DECLARE @PhongA102 INT = (SELECT MaPhong FROM dbo.Phong WHERE TenPhong = N'A102');
DECLARE @PhongB201 INT = (SELECT MaPhong FROM dbo.Phong WHERE TenPhong = N'B201');
DECLARE @PhongC301 INT = (SELECT MaPhong FROM dbo.Phong WHERE TenPhong = N'C301');

IF NOT EXISTS (SELECT 1 FROM dbo.Giuong WHERE MaPhong = @PhongA101)
BEGIN
    INSERT INTO dbo.Giuong (MaPhong, TenGiuong, GiaThue, TrangThaiGiuong)
    VALUES
        (@PhongA101, N'A101-G1', 1700000, N'Trống'),
        (@PhongA101, N'A101-G2', 1700000, N'Trống'),
        (@PhongA101, N'A101-G3', 1700000, N'Trống'),
        (@PhongA101, N'A101-G4', 1700000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Giuong WHERE MaPhong = @PhongA102)
BEGIN
    INSERT INTO dbo.Giuong (MaPhong, TenGiuong, GiaThue, TrangThaiGiuong)
    VALUES
        (@PhongA102, N'A102-G1', 1800000, N'Đang ở'),
        (@PhongA102, N'A102-G2', 1800000, N'Trống'),
        (@PhongA102, N'A102-G3', 1800000, N'Trống'),
        (@PhongA102, N'A102-G4', 1800000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Giuong WHERE MaPhong = @PhongB201)
BEGIN
    INSERT INTO dbo.Giuong (MaPhong, TenGiuong, GiaThue, TrangThaiGiuong)
    VALUES
        (@PhongB201, N'B201-G1', 3200000, N'Trống'),
        (@PhongB201, N'B201-G2', 3200000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Giuong WHERE MaPhong = @PhongC301)
BEGIN
    INSERT INTO dbo.Giuong (MaPhong, TenGiuong, GiaThue, TrangThaiGiuong)
    VALUES
        (@PhongC301, N'C301-G1', 2400000, N'Trống'),
        (@PhongC301, N'C301-G2', 2400000, N'Trống'),
        (@PhongC301, N'C301-G3', 2400000, N'Trống'),
        (@PhongC301, N'C301-G4', 2400000, N'Trống'),
        (@PhongC301, N'C301-G5', 2400000, N'Trống'),
        (@PhongC301, N'C301-G6', 2400000, N'Trống');
END

UPDATE dbo.Giuong SET GiaThue = 1700000, TrangThaiGiuong = N'Trống' WHERE TenGiuong IN (N'A101-G1', N'A101-G2', N'A101-G3', N'A101-G4');
UPDATE dbo.Giuong SET GiaThue = 1800000, TrangThaiGiuong = N'Đang ở' WHERE TenGiuong = N'A102-G1';
UPDATE dbo.Giuong SET GiaThue = 1800000, TrangThaiGiuong = N'Trống' WHERE TenGiuong IN (N'A102-G2', N'A102-G3', N'A102-G4');
UPDATE dbo.Giuong SET GiaThue = 3200000, TrangThaiGiuong = N'Trống' WHERE TenGiuong IN (N'B201-G1', N'B201-G2');
UPDATE dbo.Giuong SET GiaThue = 2400000, TrangThaiGiuong = N'Trống' WHERE TenGiuong IN (N'C301-G1', N'C301-G2', N'C301-G3', N'C301-G4', N'C301-G5', N'C301-G6');
GO

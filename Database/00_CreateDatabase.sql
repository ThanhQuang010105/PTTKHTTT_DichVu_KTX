-- =========================================================================
-- KHỞI TẠO TOÀN DIỆN CƠ SỞ DỮ LIỆU KTX HOMESTAY DORM (19 BẢNG)
-- =========================================================================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KTX_HomeStay_Full')
BEGIN
    CREATE DATABASE KTX_HomeStay_Full;
END
GO

USE KTX_HomeStay_Full;
GO

-- =========================================================================
-- PHASE 0: XÓA CÁC BẢNG CŨ THEO THỨ TỰ TỪ CON LÊN CHA (ĐỂ KHÔNG LỖI FK)
-- =========================================================================
DROP TABLE IF EXISTS Vat_Dung_Cap_Phat;
DROP TABLE IF EXISTS Bien_Ban_Ban_Giao;
DROP TABLE IF EXISTS Hop_Dong_Dich_Vu; -- Bảng trung gian cho quan hệ N-N của HopDong-DichVu
DROP TABLE IF EXISTS Dich_Vu;
DROP TABLE IF EXISTS Phi_Phat_Sinh;
DROP TABLE IF EXISTS Chi_So_Dien_Nuoc;
DROP TABLE IF EXISTS Ky_Thanh_Toan_Thue;
DROP TABLE IF EXISTS Bang_Doi_Soat_Coc;
DROP TABLE IF EXISTS Phieu_Thu;
DROP TABLE IF EXISTS Hop_Dong_Thue;
DROP TABLE IF EXISTS Thong_Tin_Dat_Coc;
DROP TABLE IF EXISTS Phieu_Dat_Coc;
DROP TABLE IF EXISTS Lich_xem_phong;
DROP TABLE IF EXISTS Giuong;
DROP TABLE IF EXISTS Phong;
DROP TABLE IF EXISTS Nhan_Vien;
DROP TABLE IF EXISTS Chi_Nhanh;
DROP TABLE IF EXISTS Dang_ky_thue;
DROP TABLE IF EXISTS Khach_hang;
DROP TABLE IF EXISTS Thong_tin_cu_tru;
DROP TABLE IF EXISTS Nhom_Thue;
GO

-- =========================================================================
-- PHASE 1: TẠO CÁC BẢNG ĐỘC LẬP (KHÔNG CHỨA KHÓA NGOẠI)
-- =========================================================================

-- 1. Nhom_Thue
CREATE TABLE Nhom_Thue (
    MaNhom VARCHAR(20) PRIMARY KEY,
    SoLuong INT NOT NULL DEFAULT 1,
    NguoiDaiDien VARCHAR(20)
);

-- 2. Thong_tin_cu_tru
CREATE TABLE Thong_tin_cu_tru (
    maCuTru VARCHAR(20) PRIMARY KEY,
    diaChiThuongTru NVARCHAR(255),
    Tinh NVARCHAR(100),
    QuanHuyen NVARCHAR(100),
    ngayKhaiBao DATE,
    trangThaiTamTru NVARCHAR(50)
);

-- 3. Chi_Nhanh
CREATE TABLE Chi_Nhanh (
    MaCN VARCHAR(20) PRIMARY KEY,
    TenChiNhanh NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SDT VARCHAR(15),
    Email VARCHAR(100),
    KhuVuc NVARCHAR(50),
    TrangThai NVARCHAR(50),
    NgayThanhLap DATE
);

-- 4. Dang_ky_thue
CREATE TABLE Dang_ky_thue (
    MaDK VARCHAR(20) PRIMARY KEY,
    SoNguoiDuKien INT,
    KhuVucMongMuon NVARCHAR(100),
    HinhThucThue NVARCHAR(50),
    LoaiPhongMongMuon NVARCHAR(50),
    MucGiaMongMuon NVARCHAR(50),
    ThoiGianDuKienVaoO DATE,
    ThoiHanThue NVARCHAR(50),
    TieuChiUuTien NVARCHAR(255),
    TrangThai NVARCHAR(50),
    NgayDangKy DATE,
    GhiChu NVARCHAR(500)
);

-- 5. Dich_Vu
CREATE TABLE Dich_Vu (
    MaDV VARCHAR(20) PRIMARY KEY,
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL
);

-- =========================================================================
-- PHASE 2: TẠO CÁC BẢNG CHA CẤP 2 (CÓ KHÓA NGOẠI NỐI ĐẾN PHASE 1)
-- =========================================================================

-- 6. Khach_hang
CREATE TABLE Khach_hang (
    maKH VARCHAR(20) PRIMARY KEY,
    MaNhom VARCHAR(20),
    maCuTru VARCHAR(20),
    hoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) UNIQUE NOT NULL,
    SDT VARCHAR(15),
    email VARCHAR(100),
    gioiTinh NVARCHAR(10),
    QuocTich NVARCHAR(50),
    KhaNangTC NVARCHAR(100),
    FOREIGN KEY (MaNhom) REFERENCES Nhom_Thue(MaNhom),
    FOREIGN KEY (maCuTru) REFERENCES Thong_tin_cu_tru(maCuTru)
);

-- 7. Nhan_Vien
CREATE TABLE Nhan_Vien (
    MaNV VARCHAR(20) PRIMARY KEY,
    MaCN VARCHAR(20) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    DiaChi NVARCHAR(255),
    Email VARCHAR(100),
    VaiTro NVARCHAR(50),
    MatKhau VARCHAR(255) NOT NULL DEFAULT '123456', -- Đã bổ sung trường Mật Khẩu
    FOREIGN KEY (MaCN) REFERENCES Chi_Nhanh(MaCN)
);

-- 8. Phong
CREATE TABLE Phong (
    MaPhong VARCHAR(20) PRIMARY KEY,
    MaCN VARCHAR(20) NOT NULL,
    TenPhong NVARCHAR(50) NOT NULL,
    KhuVuc NVARCHAR(50),
    LoaiPhong NVARCHAR(50),
    GioiTinhQuyDinh NVARCHAR(10),
    SucChuaToiDa INT,
    GiaThuePhong DECIMAL(18,2) NOT NULL,
    TrangThaiPhong NVARCHAR(50),
    SoLuongGiuong INT,
    FOREIGN KEY (MaCN) REFERENCES Chi_Nhanh(MaCN)
);

-- =========================================================================
-- PHASE 3: TẠO CÁC BẢNG NGHIỆP VỤ LIÊN KẾT PHÒNG & KHÁCH
-- =========================================================================

-- 9. Giuong
CREATE TABLE Giuong (
    MaGiuong VARCHAR(20) PRIMARY KEY,
    MaPhong VARCHAR(20) NOT NULL,
    TenGiuong NVARCHAR(50),
    GiaThueGiuong DECIMAL(18,2),
    TrangThaiGiuong NVARCHAR(50),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);

-- 10. Lich_xem_phong
CREATE TABLE Lich_xem_phong (
    MaLich VARCHAR(20) PRIMARY KEY,
    MaDK VARCHAR(20) NOT NULL,
    MaGiuong VARCHAR(20) NOT NULL,
    ThoiGianHen DATETIME,
    TrangThaiLich NVARCHAR(50),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaDK) REFERENCES Dang_ky_thue(MaDK),
    FOREIGN KEY (MaGiuong) REFERENCES Giuong(MaGiuong)
);

-- 11. Phieu_Dat_Coc
CREATE TABLE Phieu_Dat_Coc (
    MaPhieu VARCHAR(20) PRIMARY KEY,
    maKH VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20),
    NgayDatCoc DATETIME,
    SoTienCoc DECIMAL(18,2),
    SoGiuongCoc INT,
    HinhThucThanhToan NVARCHAR(50),
    HinhAnhMinhChung VARCHAR(255),
    TrangThai NVARCHAR(50),
    FOREIGN KEY (maKH) REFERENCES Khach_hang(maKH),
    FOREIGN KEY (MaNV) REFERENCES Nhan_Vien(MaNV),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);

-- 11.5. Thong_Tin_Dat_Coc
CREATE TABLE Thong_Tin_Dat_Coc (
    MaPhieu VARCHAR(20),
    MaGiuong VARCHAR(20),
    PRIMARY KEY (MaPhieu, MaGiuong),
    FOREIGN KEY (MaPhieu) REFERENCES Phieu_Dat_Coc(MaPhieu),
    FOREIGN KEY (MaGiuong) REFERENCES Giuong(MaGiuong)
);

-- 12. Hop_Dong_Thue
CREATE TABLE Hop_Dong_Thue (
    MaHopDong VARCHAR(20) PRIMARY KEY,
    maKH VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    MaPhieu VARCHAR(20) NOT NULL,
    NgayLap DATETIME,
    NgayBatDau DATE,
    GiaThue DECIMAL(18,2),
    TrangThai NVARCHAR(50),
    NgayKetThuc DATE,
    KyThanhToan NVARCHAR(50),
    QuyDinhHoanCoc NVARCHAR(255),
    NoiQuy NVARCHAR(MAX),
    FOREIGN KEY (maKH) REFERENCES Khach_hang(maKH),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    FOREIGN KEY (MaPhieu) REFERENCES Phieu_Dat_Coc(MaPhieu)
);

-- =========================================================================
-- PHASE 4: TẠO CÁC BẢNG TÀI CHÍNH, HÓA ĐƠN & TÀI SẢN CHI TIẾT
-- =========================================================================

-- 13. Phieu_Thu
CREATE TABLE Phieu_Thu (
    MaPhieu VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL,
    NgayLap DATETIME,
    TienThueKyDau DECIMAL(18,2),
    PhiDichVuBanDau DECIMAL(18,2),
    TongTienThu DECIMAL(18,2),
    HinhThucThanhToan NVARCHAR(50),
    TrangThai NVARCHAR(50),
    FOREIGN KEY (MaHopDong) REFERENCES Hop_Dong_Thue(MaHopDong)
);

-- 14. Hop_Dong_Dich_Vu
CREATE TABLE Hop_Dong_Dich_Vu (
    MaHopDong VARCHAR(20),
    MaDV VARCHAR(20),
    PRIMARY KEY (MaHopDong, MaDV),
    FOREIGN KEY (MaHopDong) REFERENCES Hop_Dong_Thue(MaHopDong),
    FOREIGN KEY (MaDV) REFERENCES Dich_Vu(MaDV)
);

-- 15. Bien_Ban_Ban_Giao
CREATE TABLE Bien_Ban_Ban_Giao (
    MaBienBan VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL,
    NgayBanGiao DATETIME,
    HienTrangKhuVuc NVARCHAR(255),
    TrangThai NVARCHAR(50),
    NguoiNhan NVARCHAR(100),
    NguoiBanGiao NVARCHAR(100),
    FOREIGN KEY (MaHopDong) REFERENCES Hop_Dong_Thue(MaHopDong)
);

-- 16. Vat_Dung_Cap_Phat
CREATE TABLE Vat_Dung_Cap_Phat (
    MaVatDung VARCHAR(20) PRIMARY KEY,
    MaBienBan VARCHAR(20) NOT NULL,
    TenVatDung NVARCHAR(100),
    SoLuong INT,
    TinhTrang NVARCHAR(100),
    DonGiaDenBu DECIMAL(18,2),
    FOREIGN KEY (MaBienBan) REFERENCES Bien_Ban_Ban_Giao(MaBienBan)
);

-- 17. Ky_Thanh_Toan_Thue
CREATE TABLE Ky_Thanh_Toan_Thue (
    MaKyTT VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL,
    NgayBatDauKy DATE,
    NgayKetThucKy DATE,
    SoTienPhaiTra DECIMAL(18,2),
    SoTienDaTra DECIMAL(18,2),
    SoTienConNo DECIMAL(18,2),
    NgayThanhToan DATETIME,
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaHopDong) REFERENCES Hop_Dong_Thue(MaHopDong)
);

-- 18. Chi_So_Dien_Nuoc
CREATE TABLE Chi_So_Dien_Nuoc (
    MaChiSo VARCHAR(20) PRIMARY KEY,
    MaPhong VARCHAR(20) NOT NULL,
    MaKyTT VARCHAR(20),
    ThangNam VARCHAR(10),
    ChiSoDienDauKy INT,
    ChiSoDienCuoiKy INT,
    ChiSoNuocDauKy INT,
    ChiSoNuocCuoiKy INT,
    DonGiaDien DECIMAL(18,2),
    DonGiaNuoc DECIMAL(18,2),
    TrangThaiThanhToan NVARCHAR(50),
    NgayThanhToan DATETIME,
    SoTienDaThu DECIMAL(18,2),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    FOREIGN KEY (MaKyTT) REFERENCES Ky_Thanh_Toan_Thue(MaKyTT)
);

-- 19. Phi_Phat_Sinh
CREATE TABLE Phi_Phat_Sinh (
    MaPhiPS VARCHAR(20) PRIMARY KEY,
    MaKyTT VARCHAR(20) NOT NULL,
    LoaiPhi NVARCHAR(100),
    MoTa NVARCHAR(500),
    SoTien DECIMAL(18,2),
    NgayGhiNhan DATETIME,
    NhanVienGhiNhan VARCHAR(20),
    LinkHinhAnh VARCHAR(255),
    FOREIGN KEY (MaKyTT) REFERENCES Ky_Thanh_Toan_Thue(MaKyTT),
    FOREIGN KEY (NhanVienGhiNhan) REFERENCES Nhan_Vien(MaNV)
);

-- 20. Bang_Doi_Soat_Coc
CREATE TABLE Bang_Doi_Soat_Coc (
    MaDS VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL UNIQUE,
    NgayTraPhong DATE,
    DaKyHopDong BIT,
    HetHangHopDong BIT,
    TyLeHoanCoc DECIMAL(5,2),
    SoTienCocDuocXetHoan DECIMAL(18,2),
    TongTienKhauTru DECIMAL(18,2),
    SoTienHoanLai DECIMAL(18,2),
    SoTienPhaiDongThem DECIMAL(18,2),
    NgayLapDoiSoat DATETIME,
    TrangThaiDuyet NVARCHAR(50),
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaHopDong) REFERENCES Hop_Dong_Thue(MaHopDong)
);
GO

-- =========================================================================
-- ĐỔ DỮ LIỆU THỰC TẾ (MOCK DATA) ĐỒNG BỘ 19 BẢNG
-- =========================================================================

-- 1. Chi_Nhanh
INSERT INTO Chi_Nhanh (MaCN, TenChiNhanh, DiaChi, SDT, Email, KhuVuc, TrangThai) VALUES
('CN01', N'KTX HomeStay Cơ sở Quận 1', N'227 Nguyễn Văn Cừ, Quận 1, TP.HCM', '0901234567', 'cs1@homestay.com', N'Quận 1', N'Hoạt động'),
('CN02', N'KTX HomeStay Cơ sở Bình Thạnh', N'15 Ung Văn Khiêm, Bình Thạnh, TP.HCM', '0909876543', 'cs2@homestay.com', N'Bình Thạnh', N'Hoạt động');

-- 2. Nhom_Thue
INSERT INTO Nhom_Thue (MaNhom, SoLuong, NguoiDaiDien) VALUES
('N01', 2, 'KH002'),
('N02', 1, 'KH003');

-- 3. Thong_tin_cu_tru
INSERT INTO Thong_tin_cu_tru (maCuTru, diaChiThuongTru, Tinh, QuanHuyen, ngayKhaiBao, trangThaiTamTru) VALUES
('CT001', N'123 Đường Ba Tháng Hai', N'TP Hồ Chí Minh', N'Quận 10', '2026-01-01', N'Đã đăng ký'),
('CT002', N'456 Lê Duẩn', N'Đà Nẵng', N'Hải Châu', '2026-02-01', N'Đã đăng ký');

-- 4. Nhan_Vien
INSERT INTO Nhan_Vien (MaNV, MaCN, HoTen, SDT, DiaChi, Email, VaiTro, MatKhau) VALUES
('NV001', 'CN01', N'Đặng Dương Thanh Quang', '0987111222', N'Thủ Đức', 'quang.ddt@homestay.com', N'Quản lý', '123456'),
('NV002', 'CN01', N'Đỗ Anh Khoa', '0987333444', N'Quận 3', 'khoa.da@homestay.com', N'Kế toán', '123456'),
('NV003', 'CN02', N'Đào Đức Thịnh', '0987555666', N'Bình Thạnh', 'thinh.dd@homestay.com', N'Sale', '123456');

-- 5. Phong
INSERT INTO Phong (MaPhong, MaCN, TenPhong, KhuVuc, LoaiPhong, GioiTinhQuyDinh, SucChuaToiDa, GiaThuePhong, TrangThaiPhong, SoLuongGiuong) VALUES
('P101', 'CN01', N'Phòng 101', N'Tầng 1', N'Phòng 4 Người', N'Nam', 4, 1500000.00, N'Hoạt động', 4),
('P102', 'CN01', N'Phòng 102', N'Tầng 1', N'Phòng Đơn', N'Nam', 1, 3000000.00, N'Hoạt động', 1);

-- 6. Giuong
INSERT INTO Giuong (MaGiuong, MaPhong, TenGiuong, GiaThueGiuong, TrangThaiGiuong) VALUES
('G101A', 'P101', N'Giường 101-A', 1500000.00, N'Đã thuê'),
('G101B', 'P101', N'Giường 101-B', 1500000.00, N'Trống'),
('G102A', 'P102', N'Giường Đơn 102', 3000000.00, N'Đã thuê');

-- 7. Khach_hang
INSERT INTO Khach_hang (maKH, MaNhom, maCuTru, hoTen, CCCD, SDT, email, gioiTinh, QuocTich, KhaNangTC) VALUES
('KH001', NULL, 'CT001', N'Nguyễn Văn A', '123456789012', '0909123456', 'nguyenvana@gmail.com', N'Nam', N'Việt Nam', N'Khá'),
('KH002', 'N01', 'CT002', N'Lê Hoàng B', '987654321098', '0912345678', 'lehoangb@gmail.com', N'Nam', N'Việt Nam', N'Trung bình');

-- 8. Dang_ky_thue
INSERT INTO Dang_ky_thue (MaDK, SoNguoiDuKien, KhuVucMongMuon, HinhThucThue, LoaiPhongMongMuon, MucGiaMongMuon, ThoiGianDuKienVaoO, ThoiHanThue, TrangThai, NgayDangKy) VALUES
('DK001', 1, N'Quận 1', N'Thuê giường', N'Phòng 4 người', N'1.5 - 2 triệu', '2026-03-01', N'6 tháng', N'Đã duyệt', '2026-02-20');

-- 9. Lich_xem_phong
INSERT INTO Lich_xem_phong (MaLich, MaDK, MaGiuong, ThoiGianHen, TrangThaiLich, GhiChu) VALUES
('L001', 'DK001', 'G101A', '2026-02-22 14:00:00', N'Đã xem', N'Khách ưng ý phòng sạch sẽ');

-- 10. Phieu_Dat_Coc
INSERT INTO Phieu_Dat_Coc (MaPhieu, maKH, MaNV, MaPhong, NgayDatCoc, SoTienCoc, SoGiuongCoc, HinhThucThanhToan, TrangThai) VALUES
('PC000123', 'KH001', 'NV003', 'P101', '2026-03-01', 3000000.00, 1, N'Tiền mặt', NULL, N'HoanTat');

-- 10.5 Thong_Tin_Dat_Coc
INSERT INTO Thong_Tin_Dat_Coc (MaPhieu, MaGiuong) VALUES
('PC000123', 'G101A');

-- 11. Hop_Dong_Thue
INSERT INTO Hop_Dong_Thue (MaHopDong, maKH, MaPhong, MaPhieu, NgayLap, NgayBatDau, GiaThue, TrangThai, NgayKetThuc, KyThanhToan) VALUES
('HD000123', 'KH001', 'P101', 'PC000123', '2026-03-01', '2026-03-01', 1500000.00, N'Đang hiệu lực', '2026-09-01', N'Hàng tháng');

-- 12. Phieu_Thu
INSERT INTO Phieu_Thu (MaPhieu, MaHopDong, NgayLap, TienThueKyDau, PhiDichVuBanDau, TongTienThu, HinhThucThanhToan, TrangThai) VALUES
('PT001', 'HD000123', '2026-03-01', 1500000.00, 500000.00, 2000000.00, N'Chuyển khoản', N'Đã thu');

-- 13. Dich_Vu
INSERT INTO Dich_Vu (MaDV, TenDV, DonGia) VALUES
('DV001', N'Giặt sấy tự động', 50000.00),
('DV002', N'Gửi xe máy', 100000.00);

-- 14. Hop_Dong_Dich_Vu
INSERT INTO Hop_Dong_Dich_Vu (MaHopDong, MaDV) VALUES
('HD000123', 'DV002');

-- 15. Bien_Ban_Ban_Giao
INSERT INTO Bien_Ban_Ban_Giao (MaBienBan, MaHopDong, NgayBanGiao, HienTrangKhuVuc, TrangThai, NguoiNhan, NguoiBanGiao) VALUES
('BB001', 'HD000123', '2026-03-01', N'Phòng sạch sẽ, đầy đủ thiết bị', N'Đã ký kết', N'Nguyễn Văn A', N'Đào Đức Thịnh');

-- 16. Vat_Dung_Cap_Phat
INSERT INTO Vat_Dung_Cap_Phat (MaVatDung, MaBienBan, TenVatDung, SoLuong, TinhTrang, DonGiaDenBu) VALUES
('VD001', 'BB001', N'Tủ gỗ quần áo', 1, N'Mới 100%', 2000000.00),
('VD002', 'BB001', N'Thẻ từ phòng', 1, N'Hoạt động tốt', 100000.00);

-- 17. Ky_Thanh_Toan_Thue
INSERT INTO Ky_Thanh_Toan_Thue (MaKyTT, MaHopDong, NgayBatDauKy, NgayKetThucKy, SoTienPhaiTra, SoTienDaTra, SoTienConNo, NgayThanhToan, TrangThai, GhiChu) VALUES
('KTT01', 'HD000123', '2026-03-01', '2026-04-01', 1500000.00, 1500000.00, 0, '2026-03-01', N'Đã đóng', NULL),
('KTT02', 'HD000123', '2026-04-01', '2026-05-01', 1500000.00, 1500000.00, 0, '2026-04-02', N'Đã đóng', NULL);

-- 18. Chi_So_Dien_Nuoc
INSERT INTO Chi_So_Dien_Nuoc (MaChiSo, MaPhong, MaKyTT, ThangNam, ChiSoDienDauKy, ChiSoDienCuoiKy, ChiSoNuocDauKy, ChiSoNuocCuoiKy, DonGiaDien, DonGiaNuoc, TrangThaiThanhToan) VALUES
('CS001', 'P101', 'KTT02', '04/2026', 1000, 1100, 200, 210, 3500.00, 12000.00, N'Đã thanh toán');

-- 19. Phi_Phat_Sinh
INSERT INTO Phi_Phat_Sinh (MaPhiPS, MaKyTT, LoaiPhi, MoTa, SoTien, NgayGhiNhan, NhanVienGhiNhan) VALUES
('PS001', 'KTT02', N'Hư hỏng', N'Cửa tủ bị gãy bản lề', 200000.00, '2026-04-15', 'NV001'),
('PS002', 'KTT02', N'Mất mát', N'Làm mất 1 thẻ từ', 100000.00, '2026-04-15', 'NV001');

-- 20. Bang_Doi_Soat_Coc
INSERT INTO Bang_Doi_Soat_Coc (MaDS, MaHopDong, NgayTraPhong, DaKyHopDong, HetHangHopDong, TyLeHoanCoc, SoTienCocDuocXetHoan, TongTienKhauTru, SoTienHoanLai, SoTienPhaiDongThem, NgayLapDoiSoat, TrangThaiDuyet) VALUES
('DS001', 'HD000123', '2026-06-03', 1, 0, 100.00, 3000000.00, 400000.00, 770000.00, 0, '2026-06-03 15:30:00', N'Chờ kế toán xác nhận');
GO

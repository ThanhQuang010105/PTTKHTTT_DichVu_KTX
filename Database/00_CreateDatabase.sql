USE master;
GO

IF DB_ID('HomeStayDorm') IS NOT NULL
BEGIN
    ALTER DATABASE HomeStayDorm SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HomeStayDorm;
END
GO

CREATE DATABASE HomeStayDorm;
GO

USE HomeStayDorm;
GO

-- 1. Chi_Nhanh
CREATE TABLE Chi_Nhanh (
    MaCN INT IDENTITY(1,1) PRIMARY KEY,
    TenChiNhanh NVARCHAR(100),
    DiaChi NVARCHAR(255),
    SDT VARCHAR(15),
    Email VARCHAR(100),
    KhuVuc NVARCHAR(100),
    TrangThai NVARCHAR(50),
    NgayThanhLap DATE
);

-- 2. Nhan_Vien
CREATE TABLE Nhan_Vien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    SDT VARCHAR(15),
    DiaChi NVARCHAR(255),
    Email VARCHAR(100),
    VaiTro NVARCHAR(50),
    MaCN INT FOREIGN KEY REFERENCES Chi_Nhanh(MaCN)
);

-- 3. Phong
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100),
    KhuVuc NVARCHAR(100),
    LoaiPhong NVARCHAR(50),
    GioiTinhQuyDinh NVARCHAR(10),
    SucChuaToiDa INT,
    GiaThuePhong DECIMAL(18,0),
    TrangThaiPhong NVARCHAR(50),
    SoLuongGiuong INT,
    MaCN INT FOREIGN KEY REFERENCES Chi_Nhanh(MaCN)
);

-- 4. Giuong
CREATE TABLE Giuong (
    MaGiuong INT IDENTITY(1,1) PRIMARY KEY,
    TenGiuong NVARCHAR(100),
    GiaThueGiuong DECIMAL(18,0),
    TrangThaiGiuong NVARCHAR(50),
    MaPhong INT FOREIGN KEY REFERENCES Phong(MaPhong)
);

-- 5. Nhom_Thue
CREATE TABLE Nhom_Thue (
    MaNhom INT IDENTITY(1,1) PRIMARY KEY,
    SoLuong INT,
    NguoiDaiDien INT -- FK sẽ được add sau khi tạo bảng Khach_hang
);

-- 6. Khach_hang
CREATE TABLE Khach_hang (
    maKH INT IDENTITY(1,1) PRIMARY KEY,
    hoTen NVARCHAR(100),
    CCCD VARCHAR(20),
    SDT VARCHAR(15),
    email VARCHAR(100),
    gioiTinh NVARCHAR(10),
    QuocTich NVARCHAR(50),
    KhachHangTC NVARCHAR(50),
    MaNhom INT FOREIGN KEY REFERENCES Nhom_Thue(MaNhom)
);

-- Thêm khóa ngoại NguoiDaiDien cho Nhom_Thue trỏ về Khach_hang
ALTER TABLE Nhom_Thue ADD CONSTRAINT FK_NhomThue_NguoiDaiDien FOREIGN KEY (NguoiDaiDien) REFERENCES Khach_hang(maKH);

-- 7. Thong_tin_cu_tru
CREATE TABLE Thong_tin_cu_tru (
    maCuTru INT IDENTITY(1,1) PRIMARY KEY,
    diaChiThuongTru NVARCHAR(255),
    Tinh NVARCHAR(100),
    QuanHuyen NVARCHAR(100),
    ngayKhaiBao DATE,
    trangThaiTamTru NVARCHAR(50),
    maKH INT FOREIGN KEY REFERENCES Khach_hang(maKH)
);

-- 8. Dang_ky_thue
CREATE TABLE Dang_ky_thue (
    MaDK INT IDENTITY(1,1) PRIMARY KEY,
    SoNguoiDuKien INT,
    KhuVucMongMuon NVARCHAR(100),
    HinhThucThue NVARCHAR(50),
    LoaiPhongMongMuon NVARCHAR(50),
    MucGiaMongMuon DECIMAL(18,0),
    ThoiGianDuKienVaoO DATE,
    ThoiHanThue INT,
    TieuChiUuTien NVARCHAR(255),
    TrangThai NVARCHAR(50),
    NgayDangKy DATE,
    GhiChu NVARCHAR(255),
    maKH INT FOREIGN KEY REFERENCES Khach_hang(maKH),
    MaNV INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV)
);

-- 9. Lich_xem_phong
CREATE TABLE Lich_xem_phong (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    ThoiGianHen DATETIME,
    TrangThaiLich NVARCHAR(50),
    GhiChu NVARCHAR(255),
    MaDK INT FOREIGN KEY REFERENCES Dang_ky_thue(MaDK),
    MaPhong INT FOREIGN KEY REFERENCES Phong(MaPhong)
);

-- 10. Phieu_Dat_Coc
CREATE TABLE Phieu_Dat_Coc (
    MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
    NgayDatCoc DATETIME,
    SoTienCoc DECIMAL(18,0),
    SoGiuongCoc INT,
    HinhThucThanhToan NVARCHAR(50),
    HinhAnhMinhChung NVARCHAR(MAX),
    TrangThai NVARCHAR(50),
    maKH INT FOREIGN KEY REFERENCES Khach_hang(maKH),
    MaNV INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV),
    MaPhong INT FOREIGN KEY REFERENCES Phong(MaPhong)
);

-- 11. Dich_Vu
CREATE TABLE Dich_Vu (
    MaDV INT IDENTITY(1,1) PRIMARY KEY,
    TenDV NVARCHAR(100),
    DonGia DECIMAL(18,0)
);

-- 12. Hop_Dong_Thue
CREATE TABLE Hop_Dong_Thue (
    MaHopDong INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATE,
    NgayBatDau DATE,
    GiaThue DECIMAL(18,0),
    TrangThai NVARCHAR(50),
    NgayKetThuc DATE,
    KyThanhToan NVARCHAR(50),
    QuyDinhChung NVARCHAR(MAX),
    NoiQuy NVARCHAR(MAX),
    maKH INT FOREIGN KEY REFERENCES Khach_hang(maKH),
    MaNV INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV),
    MaPhong INT FOREIGN KEY REFERENCES Phong(MaPhong),
    MaPhieu INT FOREIGN KEY REFERENCES Phieu_Dat_Coc(MaPhieu),
    MaDV INT FOREIGN KEY REFERENCES Dich_Vu(MaDV)
);

-- 13. Bang_Doi_Soat_Coc
CREATE TABLE Bang_Doi_Soat_Coc (
    MaDS INT IDENTITY(1,1) PRIMARY KEY,
    NgayTraPhong DATE,
    DaKyHopDong BIT,
    HetHanHopDong BIT,
    TyLeHoanCoc DECIMAL(5,2),
    SoTienCocDuocXetHoan DECIMAL(18,0),
    TongTienKhauTru DECIMAL(18,0),
    SoTienHoanLai DECIMAL(18,0),
    SoTienPhaiDongThem DECIMAL(18,0),
    NgayLapDoiSoat DATE,
    TrangThaiDuyet NVARCHAR(50),
    GhiChu NVARCHAR(255),
    MaHopDong INT FOREIGN KEY REFERENCES Hop_Dong_Thue(MaHopDong),
    MaNV INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV)
);

-- 14. Ky_Thanh_Toan_Thue
CREATE TABLE Ky_Thanh_Toan_Thue (
    MaKyTT INT IDENTITY(1,1) PRIMARY KEY,
    NgayBatDauKy DATE,
    NgayKetThucKy DATE,
    SoTienPhaiTra DECIMAL(18,0),
    SoTienDaTra DECIMAL(18,0),
    SoTienConNo DECIMAL(18,0),
    NgayThanhToan DATE,
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(255),
    MaHopDong INT FOREIGN KEY REFERENCES Hop_Dong_Thue(MaHopDong)
);

-- 15. Chi_So_Dien_Nuoc
CREATE TABLE Chi_So_Dien_Nuoc (
    MaChiSo INT IDENTITY(1,1) PRIMARY KEY,
    ThangNam VARCHAR(10),
    ChiSoDienDauKy INT,
    ChiSoDienCuoiKy INT,
    ChiSoNuocDauKy INT,
    ChiSoNuocCuoiKy INT,
    DonGiaDien DECIMAL(18,0),
    DonGiaNuoc DECIMAL(18,0),
    TrangThaiThanhToan NVARCHAR(50),
    NgayThanhToan DATE,
    SoTienDaThu DECIMAL(18,0),
    MaKyTT INT FOREIGN KEY REFERENCES Ky_Thanh_Toan_Thue(MaKyTT)
);

-- 16. Phi_Phat_Sinh
CREATE TABLE Phi_Phat_Sinh (
    MaPhiPS INT IDENTITY(1,1) PRIMARY KEY,
    LoaiPhi NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    SoTien DECIMAL(18,0),
    NgayGhiNhan DATE,
    NhanVienGhiNhan INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV),
    LinkHinhAnh NVARCHAR(MAX),
    MaKyTT INT FOREIGN KEY REFERENCES Ky_Thanh_Toan_Thue(MaKyTT)
);

-- 17. Phieu_Thu
CREATE TABLE Phieu_Thu (
    MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATE,
    TienThueKyNay DECIMAL(18,0),
    PhiDichVuKemTheo DECIMAL(18,0),
    TongTienThu DECIMAL(18,0),
    HinhThucThanhToan NVARCHAR(50),
    TrangThai NVARCHAR(50),
    MaHopDong INT FOREIGN KEY REFERENCES Hop_Dong_Thue(MaHopDong),
    MaNV INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV)
);

-- 18. Bien_Ban_Ban_Giao
CREATE TABLE Bien_Ban_Ban_Giao (
    MaBienBan INT IDENTITY(1,1) PRIMARY KEY,
    NgayBanGiao DATE,
    HienTrangKhuVuc NVARCHAR(MAX),
    TrangThai NVARCHAR(50),
    NguoiNhan INT FOREIGN KEY REFERENCES Khach_hang(maKH),
    NguoiBanGiao INT FOREIGN KEY REFERENCES Nhan_Vien(MaNV),
    MaHopDong INT FOREIGN KEY REFERENCES Hop_Dong_Thue(MaHopDong)
);

-- 19. Vat_Dung_Cap_Phat
CREATE TABLE Vat_Dung_Cap_Phat (
    MaVatDung INT IDENTITY(1,1) PRIMARY KEY,
    TenVatDung NVARCHAR(100),
    SoLuong INT,
    TinhTrang NVARCHAR(50),
    DonGiaDenBu DECIMAL(18,0),
    MaBienBan INT FOREIGN KEY REFERENCES Bien_Ban_Ban_Giao(MaBienBan)
);

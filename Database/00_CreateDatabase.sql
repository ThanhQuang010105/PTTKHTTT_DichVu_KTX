IF DB_ID(N'HomeStayDorm') IS NULL
BEGIN
    CREATE DATABASE HomeStayDorm;
END
GO

USE HomeStayDorm;
GO

IF OBJECT_ID(N'dbo.ChiNhanh', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ChiNhanh
    (
        MaChiNhanh INT IDENTITY(1,1) CONSTRAINT PK_ChiNhanh PRIMARY KEY,
        TenChiNhanh NVARCHAR(100) NOT NULL,
        KhuVuc NVARCHAR(100) NOT NULL,
        DiaChi NVARCHAR(255) NOT NULL,
        SoDienThoai NVARCHAR(20) NULL,
        TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_ChiNhanh_TrangThai DEFAULT N'Hoạt động'
    );
END
GO

IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhachHang
    (
        MaKhachHang INT IDENTITY(1,1) CONSTRAINT PK_KhachHang PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        SoDienThoai NVARCHAR(20) NOT NULL,
        CCCD NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL,
        GioiTinh NVARCHAR(30) NOT NULL,
        NgayTao DATETIME2(0) NOT NULL CONSTRAINT DF_KhachHang_NgayTao DEFAULT SYSDATETIME()
    );

    CREATE UNIQUE INDEX UX_KhachHang_SoDienThoai ON dbo.KhachHang(SoDienThoai);
END
GO

IF OBJECT_ID(N'dbo.Phong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Phong
    (
        MaPhong INT IDENTITY(1,1) CONSTRAINT PK_Phong PRIMARY KEY,
        MaChiNhanh INT NOT NULL,
        TenPhong NVARCHAR(50) NOT NULL,
        KhuVuc NVARCHAR(100) NOT NULL,
        GioiTinhQuyDinh NVARCHAR(30) NOT NULL,
        LoaiPhong NVARCHAR(50) NOT NULL,
        SucChua INT NOT NULL,
        GiaThue DECIMAL(18,2) NOT NULL,
        TrangThaiPhong NVARCHAR(30) NOT NULL CONSTRAINT DF_Phong_TrangThai DEFAULT N'Trống',
        CONSTRAINT FK_Phong_ChiNhanh FOREIGN KEY (MaChiNhanh) REFERENCES dbo.ChiNhanh(MaChiNhanh),
        CONSTRAINT CK_Phong_SucChua CHECK (SucChua > 0),
        CONSTRAINT CK_Phong_GiaThue CHECK (GiaThue > 0)
    );
END
GO

IF OBJECT_ID(N'dbo.Giuong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Giuong
    (
        MaGiuong INT IDENTITY(1,1) CONSTRAINT PK_Giuong PRIMARY KEY,
        MaPhong INT NOT NULL,
        TenGiuong NVARCHAR(50) NOT NULL,
        GiaThue DECIMAL(18,2) NOT NULL,
        TrangThaiGiuong NVARCHAR(30) NOT NULL CONSTRAINT DF_Giuong_TrangThai DEFAULT N'Trống',
        CONSTRAINT FK_Giuong_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
        CONSTRAINT CK_Giuong_GiaThue CHECK (GiaThue > 0)
    );
END
GO

IF OBJECT_ID(N'dbo.PhieuDangKyThue', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhieuDangKyThue
    (
        MaDangKy NVARCHAR(20) CONSTRAINT PK_PhieuDangKyThue PRIMARY KEY,
        MaKhachHang INT NOT NULL,
        HinhThucThue NVARCHAR(50) NOT NULL,
        KhuVucMongMuon NVARCHAR(100) NOT NULL,
        GioiTinh NVARCHAR(30) NOT NULL,
        LoaiPhong NVARCHAR(50) NOT NULL,
        SoNguoiDuKien INT NOT NULL,
        GiaToiDa DECIMAL(18,2) NOT NULL,
        NgayVaoDuKien DATE NOT NULL,
        ThoiHanThueThang INT NOT NULL,
        TieuChiUuTien NVARCHAR(500) NULL,
        TrangThai NVARCHAR(50) NOT NULL CONSTRAINT DF_PhieuDangKyThue_TrangThai DEFAULT N'Mới tạo',
        NgayTao DATETIME2(0) NOT NULL CONSTRAINT DF_PhieuDangKyThue_NgayTao DEFAULT SYSDATETIME(),
        CONSTRAINT FK_PhieuDangKyThue_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES dbo.KhachHang(MaKhachHang),
        CONSTRAINT CK_PhieuDangKyThue_SoNguoi CHECK (SoNguoiDuKien > 0),
        CONSTRAINT CK_PhieuDangKyThue_Gia CHECK (GiaToiDa > 0),
        CONSTRAINT CK_PhieuDangKyThue_ThoiHan CHECK (ThoiHanThueThang > 0)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = N'Seq_PhieuDangKyThue' AND SCHEMA_NAME(schema_id) = N'dbo')
BEGIN
    EXEC(N'CREATE SEQUENCE dbo.Seq_PhieuDangKyThue AS INT START WITH 1 INCREMENT BY 1');
END
GO

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NhanVien
    (
        MaNhanVien INT IDENTITY(1,1) CONSTRAINT PK_NhanVien PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        TenDangNhap NVARCHAR(50) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        SoDienThoai NVARCHAR(20) NULL,
        VaiTro NVARCHAR(30) NOT NULL,
        MaChiNhanh INT NULL,
        MatKhauHash NVARCHAR(128) NOT NULL,
        TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_NhanVien_TrangThai DEFAULT N'Đang làm',
        NgayVaoLam DATE NOT NULL CONSTRAINT DF_NhanVien_NgayVaoLam DEFAULT CONVERT(date, GETDATE()),
        CONSTRAINT FK_NhanVien_ChiNhanh FOREIGN KEY (MaChiNhanh) REFERENCES dbo.ChiNhanh(MaChiNhanh)
    );

    CREATE UNIQUE INDEX UX_NhanVien_TenDangNhap ON dbo.NhanVien(TenDangNhap);
    CREATE UNIQUE INDEX UX_NhanVien_Email ON dbo.NhanVien(Email);
END
GO

IF OBJECT_ID(N'dbo.LichXemPhong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LichXemPhong
    (
        MaLich INT IDENTITY(1,1) CONSTRAINT PK_LichXemPhong PRIMARY KEY,
        MaDangKy NVARCHAR(20) NOT NULL,
        LoaiDoiTuong NVARCHAR(20) NOT NULL,
        MaPhong INT NULL,
        MaGiuong INT NULL,
        NgayGioHen DATETIME2(0) NOT NULL,
        GhiChu NVARCHAR(500) NULL,
        TrangThai NVARCHAR(50) NOT NULL CONSTRAINT DF_LichXemPhong_TrangThai DEFAULT N'Đã hẹn',
        NgayTao DATETIME2(0) NOT NULL CONSTRAINT DF_LichXemPhong_NgayTao DEFAULT SYSDATETIME(),
        CONSTRAINT FK_LichXemPhong_DangKy FOREIGN KEY (MaDangKy) REFERENCES dbo.PhieuDangKyThue(MaDangKy),
        CONSTRAINT FK_LichXemPhong_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
        CONSTRAINT FK_LichXemPhong_Giuong FOREIGN KEY (MaGiuong) REFERENCES dbo.Giuong(MaGiuong)
    );
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_TaoPhieuDangKyThue
    @HoTenKhachHang NVARCHAR(100),
    @SoDienThoai NVARCHAR(20),
    @CCCD NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @GioiTinh NVARCHAR(30),
    @HinhThucThue NVARCHAR(50),
    @KhuVucMongMuon NVARCHAR(100),
    @LoaiPhong NVARCHAR(50),
    @SoNguoiDuKien INT,
    @GiaToiDa DECIMAL(18,2),
    @NgayVaoDuKien DATE,
    @ThoiHanThueThang INT,
    @TieuChiUuTien NVARCHAR(500) = NULL,
    @MaDangKy NVARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaKhachHang INT;

    SELECT @MaKhachHang = MaKhachHang
    FROM dbo.KhachHang
    WHERE SoDienThoai = @SoDienThoai;

    IF @MaKhachHang IS NULL
    BEGIN
        INSERT INTO dbo.KhachHang (HoTen, SoDienThoai, CCCD, Email, GioiTinh)
        VALUES (@HoTenKhachHang, @SoDienThoai, @CCCD, @Email, @GioiTinh);

        SET @MaKhachHang = CONVERT(INT, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.KhachHang
        SET HoTen = @HoTenKhachHang,
            CCCD = COALESCE(@CCCD, CCCD),
            Email = COALESCE(@Email, Email),
            GioiTinh = @GioiTinh
        WHERE MaKhachHang = @MaKhachHang;
    END

    DECLARE @NextValue INT = NEXT VALUE FOR dbo.Seq_PhieuDangKyThue;
    SET @MaDangKy = CONCAT(N'DK', CONVERT(CHAR(6), SYSDATETIME(), 12), RIGHT(CONCAT(N'000000', @NextValue), 6));

    INSERT INTO dbo.PhieuDangKyThue
    (
        MaDangKy,
        MaKhachHang,
        HinhThucThue,
        KhuVucMongMuon,
        GioiTinh,
        LoaiPhong,
        SoNguoiDuKien,
        GiaToiDa,
        NgayVaoDuKien,
        ThoiHanThueThang,
        TieuChiUuTien,
        TrangThai
    )
    VALUES
    (
        @MaDangKy,
        @MaKhachHang,
        @HinhThucThue,
        @KhuVucMongMuon,
        @GioiTinh,
        @LoaiPhong,
        @SoNguoiDuKien,
        @GiaToiDa,
        @NgayVaoDuKien,
        @ThoiHanThueThang,
        @TieuChiUuTien,
        N'Mới tạo'
    );
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_ChiNhanh_DanhSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MaChiNhanh, TenChiNhanh, KhuVuc, DiaChi, SoDienThoai, TrangThai
    FROM dbo.ChiNhanh
    ORDER BY MaChiNhanh DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_ChiNhanh_Luu
    @MaChiNhanh INT = NULL OUTPUT,
    @TenChiNhanh NVARCHAR(100),
    @KhuVuc NVARCHAR(100),
    @DiaChi NVARCHAR(255),
    @SoDienThoai NVARCHAR(20) = NULL,
    @TrangThai NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaChiNhanh IS NULL OR @MaChiNhanh = 0
    BEGIN
        INSERT INTO dbo.ChiNhanh (TenChiNhanh, KhuVuc, DiaChi, SoDienThoai, TrangThai)
        VALUES (@TenChiNhanh, @KhuVuc, @DiaChi, @SoDienThoai, @TrangThai);

        SET @MaChiNhanh = CONVERT(INT, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.ChiNhanh
        SET TenChiNhanh = @TenChiNhanh,
            KhuVuc = @KhuVuc,
            DiaChi = @DiaChi,
            SoDienThoai = @SoDienThoai,
            TrangThai = @TrangThai
        WHERE MaChiNhanh = @MaChiNhanh;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_ChiNhanh_Xoa
    @MaChiNhanh INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ChiNhanh
    SET TrangThai = N'Ngừng hoạt động'
    WHERE MaChiNhanh = @MaChiNhanh;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_NhanVien_DocTheoDangNhap
    @TenDangNhapHoacEmail NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        nv.MaNhanVien,
        nv.HoTen,
        nv.TenDangNhap,
        nv.Email,
        nv.SoDienThoai,
        nv.VaiTro,
        nv.MaChiNhanh,
        cn.TenChiNhanh,
        nv.MatKhauHash,
        nv.TrangThai,
        nv.NgayVaoLam
    FROM dbo.NhanVien nv
    LEFT JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = nv.MaChiNhanh
    WHERE nv.TenDangNhap = @TenDangNhapHoacEmail
       OR nv.Email = @TenDangNhapHoacEmail;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_NhanVien_DanhSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        nv.MaNhanVien,
        nv.HoTen,
        nv.TenDangNhap,
        nv.Email,
        nv.SoDienThoai,
        nv.VaiTro,
        nv.MaChiNhanh,
        cn.TenChiNhanh,
        nv.TrangThai,
        nv.NgayVaoLam
    FROM dbo.NhanVien nv
    LEFT JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = nv.MaChiNhanh
    ORDER BY nv.MaNhanVien DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_NhanVien_Luu
    @MaNhanVien INT = NULL OUTPUT,
    @HoTen NVARCHAR(100),
    @TenDangNhap NVARCHAR(50),
    @Email NVARCHAR(100),
    @SoDienThoai NVARCHAR(20) = NULL,
    @VaiTro NVARCHAR(30),
    @MaChiNhanh INT = NULL,
    @MatKhauHash NVARCHAR(128) = NULL,
    @TrangThai NVARCHAR(30),
    @NgayVaoLam DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaNhanVien IS NULL OR @MaNhanVien = 0
    BEGIN
        INSERT INTO dbo.NhanVien
        (
            HoTen,
            TenDangNhap,
            Email,
            SoDienThoai,
            VaiTro,
            MaChiNhanh,
            MatKhauHash,
            TrangThai,
            NgayVaoLam
        )
        VALUES
        (
            @HoTen,
            @TenDangNhap,
            @Email,
            @SoDienThoai,
            @VaiTro,
            @MaChiNhanh,
            @MatKhauHash,
            @TrangThai,
            @NgayVaoLam
        );

        SET @MaNhanVien = CONVERT(INT, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.NhanVien
        SET HoTen = @HoTen,
            TenDangNhap = @TenDangNhap,
            Email = @Email,
            SoDienThoai = @SoDienThoai,
            VaiTro = @VaiTro,
            MaChiNhanh = @MaChiNhanh,
            MatKhauHash = COALESCE(@MatKhauHash, MatKhauHash),
            TrangThai = @TrangThai,
            NgayVaoLam = @NgayVaoLam
        WHERE MaNhanVien = @MaNhanVien;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_NhanVien_KhoaTaiKhoan
    @MaNhanVien INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NhanVien
    SET TrangThai = N'Nghỉ việc'
    WHERE MaNhanVien = @MaNhanVien;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Phong_DanhSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.MaPhong,
        p.MaChiNhanh,
        cn.TenChiNhanh,
        p.TenPhong,
        p.KhuVuc,
        p.GioiTinhQuyDinh,
        p.LoaiPhong,
        p.SucChua,
        p.GiaThue,
        p.TrangThaiPhong
    FROM dbo.Phong p
    INNER JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = p.MaChiNhanh
    ORDER BY p.MaPhong DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Phong_Luu
    @MaPhong INT = NULL OUTPUT,
    @MaChiNhanh INT,
    @TenPhong NVARCHAR(50),
    @KhuVuc NVARCHAR(100),
    @GioiTinhQuyDinh NVARCHAR(30),
    @LoaiPhong NVARCHAR(50),
    @SucChua INT,
    @GiaThue DECIMAL(18,2),
    @TrangThaiPhong NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaPhong IS NULL OR @MaPhong = 0
    BEGIN
        INSERT INTO dbo.Phong (MaChiNhanh, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChua, GiaThue, TrangThaiPhong)
        VALUES (@MaChiNhanh, @TenPhong, @KhuVuc, @GioiTinhQuyDinh, @LoaiPhong, @SucChua, @GiaThue, @TrangThaiPhong);

        SET @MaPhong = CONVERT(INT, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.Phong
        SET MaChiNhanh = @MaChiNhanh,
            TenPhong = @TenPhong,
            KhuVuc = @KhuVuc,
            GioiTinhQuyDinh = @GioiTinhQuyDinh,
            LoaiPhong = @LoaiPhong,
            SucChua = @SucChua,
            GiaThue = @GiaThue,
            TrangThaiPhong = @TrangThaiPhong
        WHERE MaPhong = @MaPhong;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Phong_Xoa
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Phong
    SET TrangThaiPhong = N'Ngừng sử dụng'
    WHERE MaPhong = @MaPhong;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Giuong_DanhSachTheoPhong
    @MaPhong INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MaGiuong, MaPhong, TenGiuong, GiaThue, TrangThaiGiuong
    FROM dbo.Giuong
    WHERE MaPhong = @MaPhong
    ORDER BY TenGiuong;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Giuong_Luu
    @MaGiuong INT = NULL OUTPUT,
    @MaPhong INT,
    @TenGiuong NVARCHAR(50),
    @GiaThue DECIMAL(18,2),
    @TrangThaiGiuong NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaGiuong IS NULL OR @MaGiuong = 0
    BEGIN
        INSERT INTO dbo.Giuong (MaPhong, TenGiuong, GiaThue, TrangThaiGiuong)
        VALUES (@MaPhong, @TenGiuong, @GiaThue, @TrangThaiGiuong);

        SET @MaGiuong = CONVERT(INT, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.Giuong
        SET MaPhong = @MaPhong,
            TenGiuong = @TenGiuong,
            GiaThue = @GiaThue,
            TrangThaiGiuong = @TrangThaiGiuong
        WHERE MaGiuong = @MaGiuong;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Giuong_Xoa
    @MaGiuong INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Giuong
    SET TrangThaiGiuong = N'Ngừng sử dụng'
    WHERE MaGiuong = @MaGiuong;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyThue_DanhSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dk.MaDangKy,
        kh.HoTen,
        kh.SoDienThoai,
        dk.HinhThucThue,
        dk.KhuVucMongMuon,
        dk.GioiTinh,
        dk.LoaiPhong,
        dk.SoNguoiDuKien,
        dk.GiaToiDa,
        dk.NgayVaoDuKien,
        dk.ThoiHanThueThang,
        dk.TrangThai,
        dk.NgayTao
    FROM dbo.PhieuDangKyThue dk
    INNER JOIN dbo.KhachHang kh ON kh.MaKhachHang = dk.MaKhachHang
    ORDER BY dk.NgayTao DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyThue_LayChiTiet
    @MaDangKy NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dk.MaDangKy,
        kh.HoTen,
        kh.SoDienThoai,
        kh.CCCD,
        kh.Email,
        kh.GioiTinh,
        dk.HinhThucThue,
        dk.KhuVucMongMuon,
        dk.LoaiPhong,
        dk.SoNguoiDuKien,
        dk.GiaToiDa,
        dk.NgayVaoDuKien,
        dk.ThoiHanThueThang,
        dk.TieuChiUuTien,
        dk.TrangThai
    FROM dbo.PhieuDangKyThue dk
    INNER JOIN dbo.KhachHang kh ON kh.MaKhachHang = dk.MaKhachHang
    WHERE dk.MaDangKy = @MaDangKy;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyThue_CapNhatTrangThai
    @MaDangKy NVARCHAR(20),
    @TrangThai NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PhieuDangKyThue
    SET TrangThai = @TrangThai
    WHERE MaDangKy = @MaDangKy;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_LichXemPhong_Tao
    @MaLich INT OUTPUT,
    @MaDangKy NVARCHAR(20),
    @LoaiDoiTuong NVARCHAR(20),
    @MaPhong INT = NULL,
    @MaGiuong INT = NULL,
    @NgayGioHen DATETIME2(0),
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.LichXemPhong (MaDangKy, LoaiDoiTuong, MaPhong, MaGiuong, NgayGioHen, GhiChu, TrangThai)
    VALUES (@MaDangKy, @LoaiDoiTuong, @MaPhong, @MaGiuong, @NgayGioHen, @GhiChu, N'Đã hẹn');

    SET @MaLich = CONVERT(INT, SCOPE_IDENTITY());

    UPDATE dbo.PhieuDangKyThue
    SET TrangThai = N'Đã hẹn xem phòng'
    WHERE MaDangKy = @MaDangKy;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_LichXemPhong_DanhSach
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        lx.MaLich,
        lx.MaDangKy,
        kh.HoTen,
        kh.SoDienThoai,
        lx.LoaiDoiTuong,
        p.TenPhong,
        g.TenGiuong,
        lx.NgayGioHen,
        lx.TrangThai,
        lx.GhiChu
    FROM dbo.LichXemPhong lx
    INNER JOIN dbo.PhieuDangKyThue dk ON dk.MaDangKy = lx.MaDangKy
    INNER JOIN dbo.KhachHang kh ON kh.MaKhachHang = dk.MaKhachHang
    LEFT JOIN dbo.Phong p ON p.MaPhong = lx.MaPhong
    LEFT JOIN dbo.Giuong g ON g.MaGiuong = lx.MaGiuong
    ORDER BY lx.NgayGioHen DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Dashboard_ThongKe
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        (SELECT COUNT(*) FROM dbo.Phong) AS TongPhong,
        (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong = N'Trống') AS PhongTrong,
        (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong = N'Đang ở') AS PhongDangO,
        (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong = N'Đã đặt cọc') AS PhongDatCoc,
        (SELECT COUNT(*) FROM dbo.Giuong) AS TongGiuong,
        (SELECT COUNT(*) FROM dbo.Giuong WHERE TrangThaiGiuong = N'Trống') AS GiuongTrong,
        (SELECT COUNT(*) FROM dbo.PhieuDangKyThue WHERE TrangThai = N'Mới tạo') AS PhieuDangKyMoi,
        (SELECT COUNT(*) FROM dbo.LichXemPhong WHERE TrangThai = N'Đã hẹn') AS LichHen,
        (SELECT COUNT(*) FROM dbo.NhanVien WHERE TrangThai = N'Đang làm') AS NhanVienDangLam;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_TraCuuPhongKhaDung
    @KhuVuc NVARCHAR(100),
    @GioiTinh NVARCHAR(30),
    @LoaiPhong NVARCHAR(50),
    @SoNguoiDuKien INT,
    @GiaToiDa DECIMAL(18,2),
    @NgayVaoDuKien DATE,
    @TieuChiUuTien NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        N'Phòng' AS LoaiKetQua,
        p.MaPhong,
        p.TenPhong,
        cn.TenChiNhanh,
        p.KhuVuc,
        p.GioiTinhQuyDinh,
        p.LoaiPhong,
        p.SucChua,
        COUNT(CASE WHEN g.TrangThaiGiuong = N'Trống' THEN 1 END) AS SoGiuongTrong,
        p.GiaThue,
        p.TrangThaiPhong
    FROM dbo.Phong p
    INNER JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = p.MaChiNhanh
    LEFT JOIN dbo.Giuong g ON g.MaPhong = p.MaPhong
    WHERE p.TrangThaiPhong = N'Trống'
      AND p.KhuVuc = @KhuVuc
      AND (p.GioiTinhQuyDinh = @GioiTinh OR p.GioiTinhQuyDinh = N'Không yêu cầu' OR @GioiTinh = N'Không yêu cầu')
      AND p.LoaiPhong = @LoaiPhong
      AND p.SucChua >= @SoNguoiDuKien
      AND p.GiaThue <= @GiaToiDa
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.Giuong gx
          WHERE gx.MaPhong = p.MaPhong
            AND gx.TrangThaiGiuong <> N'Trống'
      )
    GROUP BY p.MaPhong, p.TenPhong, cn.TenChiNhanh, p.KhuVuc, p.GioiTinhQuyDinh,
             p.LoaiPhong, p.SucChua, p.GiaThue, p.TrangThaiPhong
    ORDER BY p.GiaThue, p.TenPhong;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_TraCuuGiuongKhaDung
    @KhuVuc NVARCHAR(100),
    @GioiTinh NVARCHAR(30),
    @LoaiPhong NVARCHAR(50),
    @SoNguoiDuKien INT,
    @GiaToiDa DECIMAL(18,2),
    @NgayVaoDuKien DATE,
    @TieuChiUuTien NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        N'Giường' AS LoaiKetQua,
        g.MaGiuong,
        g.TenGiuong,
        p.MaPhong,
        p.TenPhong,
        cn.TenChiNhanh,
        p.KhuVuc,
        p.GioiTinhQuyDinh,
        p.LoaiPhong,
        p.SucChua,
        g.GiaThue,
        g.TrangThaiGiuong
    FROM dbo.Giuong g
    INNER JOIN dbo.Phong p ON p.MaPhong = g.MaPhong
    INNER JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = p.MaChiNhanh
    WHERE g.TrangThaiGiuong = N'Trống'
      AND p.TrangThaiPhong IN (N'Trống', N'Đang ghép')
      AND p.KhuVuc = @KhuVuc
      AND (p.GioiTinhQuyDinh = @GioiTinh OR p.GioiTinhQuyDinh = N'Không yêu cầu' OR @GioiTinh = N'Không yêu cầu')
      AND p.LoaiPhong = @LoaiPhong
      AND g.GiaThue <= @GiaToiDa
    ORDER BY g.GiaThue, p.TenPhong, g.TenGiuong;
END
GO

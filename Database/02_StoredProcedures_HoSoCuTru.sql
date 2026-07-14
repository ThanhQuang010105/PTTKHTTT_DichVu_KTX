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
    DECLARE @maNhom INT;
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

-- Thêm người mới vào hồ sơ cư trú (Insert vào Khach_hang theo nhóm của phiếu cọc)
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
    DECLARE @maNhom INT;
    SELECT @maNhom = k.MaNhom 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
    BEGIN
        -- Thêm thông tin cư trú TRƯỚC
        INSERT INTO Thong_tin_cu_tru (Tinh, ngayKhaiBao, trangThaiTamTru)
        VALUES (@queQuan, GETDATE(), N'Chờ duyệt');
        
        DECLARE @maCuTru INT = SCOPE_IDENTITY();

        -- Sau đó thêm Khách hàng với maCuTru vừa tạo
        INSERT INTO Khach_hang (hoTen, CCCD, SDT, gioiTinh, QuocTich, KhaNangTC, MaNhom, maCuTru)
        VALUES (@hoTen, @cccd, @sdt, @gioiTinh, @queQuan, N'Tốt', @maNhom, @maCuTru);
    END
END
GO

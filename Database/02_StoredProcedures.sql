USE KTX_HomeStay_Full;
GO

-- =====================================================================
-- SP: Lấy danh sách phiếu đặt cọc theo bộ lọc (dùng cho FrmTraCuuDatCoc)
-- MaPhieu là VARCHAR(20) dạng 'PC000123' (KHÔNG phải INT)
-- =====================================================================
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

-- =====================================================================
-- SP: Lấy trạng thái phiếu đặt cọc theo mã phiếu (VARCHAR)
-- =====================================================================
IF OBJECT_ID('sp_GetStatusByMaPhieu', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetStatusByMaPhieu;
GO

CREATE PROCEDURE sp_GetStatusByMaPhieu
    @maPhieu VARCHAR(20)
AS
BEGIN
    SELECT TrangThai
    FROM Phieu_Dat_Coc
    WHERE MaPhieu = @maPhieu;
END
GO

USE KTX_HomeStay_Full;
GO

-- =====================================================================
-- SP: Lấy thông tin Khách Hàng theo SĐT hoặc CCCD
-- =====================================================================
IF OBJECT_ID('sp_GetKhachHangBySearch', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetKhachHangBySearch;
GO

CREATE PROCEDURE sp_GetKhachHangBySearch
    @keyword VARCHAR(20)
AS
BEGIN
    SELECT *
    FROM Khach_hang
    WHERE SDT = @keyword OR CCCD = @keyword;
END
GO

-- =====================================================================
-- SP: Cập nhật thông tin Khách Hàng
-- (Chỉ cập nhật các thông tin cơ bản: HoTen, CCCD, SDT, Email, GioiTinh, QuocTich, KhaNangTC)
-- =====================================================================
IF OBJECT_ID('sp_UpdateKhachHang', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateKhachHang;
GO

CREATE PROCEDURE sp_UpdateKhachHang
    @maKH VARCHAR(20),
    @hoTen NVARCHAR(100),
    @CCCD VARCHAR(20),
    @SDT VARCHAR(15),
    @email VARCHAR(100),
    @gioiTinh NVARCHAR(10),
    @QuocTich NVARCHAR(50),
    @KhaNangTC NVARCHAR(100)
AS
BEGIN
    UPDATE Khach_hang
    SET
        hoTen = @hoTen,
        CCCD = @CCCD,
        SDT = @SDT,
        email = @email,
        gioiTinh = @gioiTinh,
        QuocTich = @QuocTich,
        KhaNangTC = @KhaNangTC
    WHERE maKH = @maKH;
END
GO

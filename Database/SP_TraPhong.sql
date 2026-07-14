USE KTX_HomeStay_Full;
GO

-- ============================================================
-- SP 1: Tra cứu hợp đồng để tạo yêu cầu trả phòng
-- Tìm theo: SĐT, CCCD hoặc MaHopDong
-- Chỉ lấy hợp đồng đang hiệu lực
-- ============================================================
CREATE OR ALTER PROCEDURE sp_TraCuuHopDongTraPhong
    @SoDienThoai VARCHAR(15) = NULL,
    @CCCD        VARCHAR(20) = NULL,
    @MaHopDong   VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hd.MaHopDong,
        kh.maKH,
        kh.hoTen         AS TenKhachHang,
        kh.CCCD,
        kh.SDT,
        p.TenPhong,
        p.MaPhong,
        hd.NgayBatDau,
        hd.NgayKetThuc,
        hd.GiaThue,
        hd.TrangThai,
        pdc.SoTienCoc    AS TienCocGoc,
        hd.MaPhieu       AS MaPhieuCoc
    FROM Hop_Dong_Thue hd
    INNER JOIN Khach_hang kh  ON hd.maKH    = kh.maKH
    INNER JOIN Phong p         ON hd.MaPhong = p.MaPhong
    INNER JOIN Phieu_Dat_Coc pdc ON hd.MaPhieu = pdc.MaPhieu
    WHERE
        hd.TrangThai = N'Đang hiệu lực'
        AND (
            (@SoDienThoai IS NOT NULL AND kh.SDT       = @SoDienThoai) OR
            (@CCCD        IS NOT NULL AND kh.CCCD      = @CCCD)        OR
            (@MaHopDong   IS NOT NULL AND hd.MaHopDong = @MaHopDong)
        );
END;
GO

-- ============================================================
-- SP 2: Tạo yêu cầu trả phòng
-- Ghi nhận ngày dự kiến trả phòng, lý do, ghi chú
-- Cập nhật trạng thái hợp đồng → "Chờ kiểm tra hiện trạng"
-- ============================================================
CREATE OR ALTER PROCEDURE sp_TaoYeuCauTraPhong
    @MaHopDong         VARCHAR(20),
    @NgayDuKienTra     DATE,
    @LyDoTraPhong      NVARCHAR(100),
    @GhiChu            NVARCHAR(255) = NULL,
    @MaNVSale          VARCHAR(20),
    @KetQua            NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra hợp đồng còn hiệu lực không
        DECLARE @TrangThaiHD NVARCHAR(50);
        DECLARE @NgayBatDau  DATE;

        SELECT @TrangThaiHD = TrangThai, @NgayBatDau = NgayBatDau
        FROM Hop_Dong_Thue
        WHERE MaHopDong = @MaHopDong;

        IF @TrangThaiHD IS NULL
        BEGIN
            SET @KetQua = N'Không tìm thấy hợp đồng!';
            ROLLBACK; RETURN;
        END

        IF @TrangThaiHD <> N'Đang hiệu lực'
        BEGIN
            SET @KetQua = N'Hợp đồng này đã thanh lý hoặc đang chờ xử lý, không thể tạo yêu cầu mới!';
            ROLLBACK; RETURN;
        END

        IF @NgayDuKienTra < @NgayBatDau
        BEGIN
            SET @KetQua = N'Ngày dự kiến trả phòng không được nhỏ hơn ngày bắt đầu ở!';
            ROLLBACK; RETURN;
        END

        -- Cập nhật trạng thái hợp đồng
        UPDATE Hop_Dong_Thue
        SET TrangThai = N'Chờ kiểm tra hiện trạng'
        WHERE MaHopDong = @MaHopDong;

        -- Tạo bản ghi kỳ thanh toán cuối (để ghi nhận yêu cầu trả)
        -- GhiChu lưu: Ngày dự kiến trả phòng, lý do, nhân viên tạo
        DECLARE @GhiChuKy NVARCHAR(500);
        SET @GhiChuKy = N'YÊU CẦU TRẢ PHÒNG | Ngày dự kiến: ' + CONVERT(NVARCHAR, @NgayDuKienTra, 103)
                      + N' | Lý do: ' + @LyDoTraPhong
                      + CASE WHEN @GhiChu IS NOT NULL THEN N' | Ghi chú: ' + @GhiChu ELSE N'' END
                      + N' | NV: ' + @MaNVSale;

        -- Tạo mã kỳ tự động
        DECLARE @MaKyMoi VARCHAR(20);
        SELECT @MaKyMoi = 'KTT' + RIGHT('0000' + CAST(COUNT(*) + 1 AS VARCHAR), 4)
        FROM Ky_Thanh_Toan_Thue
        WHERE MaHopDong = @MaHopDong;

        INSERT INTO Ky_Thanh_Toan_Thue
            (MaKyTT, MaHopDong, NgayBatDauKy, NgayKetThucKy,
             SoTienPhaiTra, SoTienDaTra, SoTienConNo, TrangThai, GhiChu)
        VALUES
            (@MaKyMoi, @MaHopDong, @NgayDuKienTra, @NgayDuKienTra,
             0, 0, 0, N'Yêu cầu trả phòng', @GhiChuKy);

        SET @KetQua = N'SUCCESS';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SET @KetQua = N'Lỗi hệ thống: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

-- ============================================================
-- SP 3: Lấy lịch sử yêu cầu trả phòng (đã tạo)
-- ============================================================
CREATE OR ALTER PROCEDURE sp_GetDanhSachYeuCauTraPhong
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hd.MaHopDong,
        kh.hoTen         AS TenKhachHang,
        kh.SDT,
        p.TenPhong,
        hd.NgayBatDau,
        hd.NgayKetThuc,
        hd.TrangThai,
        kty.GhiChu       AS ThongTinYeuCau,
        kty.NgayBatDauKy AS NgayDuKienTra
    FROM Hop_Dong_Thue hd
    INNER JOIN Khach_hang kh  ON hd.maKH    = kh.maKH
    INNER JOIN Phong p         ON hd.MaPhong = p.MaPhong
    INNER JOIN Ky_Thanh_Toan_Thue kty ON hd.MaHopDong = kty.MaHopDong
    WHERE
        hd.TrangThai = N'Chờ kiểm tra hiện trạng'
        AND kty.TrangThai = N'Yêu cầu trả phòng'
    ORDER BY kty.NgayBatDauKy DESC;
END;
GO

USE KTX_HomeStay_Full;
GO

-- ============================================================
-- SP 1: Lấy danh sách hợp đồng đủ điều kiện lập đối soát cọc
-- (Trạng thái: Chờ kiểm tra hiện trạng - đã tạo yêu cầu trả phòng)
-- ============================================================
CREATE OR ALTER PROCEDURE sp_GetHopDongChoDoiSoat
    @SDT        VARCHAR(20) = NULL,
    @CCCD       VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hd.MaHopDong,
        kh.maKH,
        kh.hoTen         AS TenKhachHang,
        kh.SDT,
        kh.CCCD,
        p.TenPhong,
        hd.NgayBatDau,
        hd.NgayKetThuc,
        hd.GiaThue,
        hd.TrangThai,
        pdc.SoTienCoc    AS TienCocGoc,
        pdc.MaPhieu      AS MaPhieuCoc,
        -- Tính số tháng đã lưu trú đến ngày hôm nay
        DATEDIFF(MONTH, hd.NgayBatDau, GETDATE()) AS SoThangDaO,
        -- Kiểm tra hợp đồng hết hạn chưa
        CASE WHEN GETDATE() >= hd.NgayKetThuc THEN 1 ELSE 0 END AS HetHanHopDong
    FROM Hop_Dong_Thue hd
    INNER JOIN Khach_hang kh     ON hd.maKH    = kh.maKH
    INNER JOIN Phong p            ON hd.MaPhong = p.MaPhong
    INNER JOIN Phieu_Dat_Coc pdc  ON hd.MaPhieu = pdc.MaPhieu
    WHERE
        hd.TrangThai IN (N'Chờ kiểm tra hiện trạng', N'Chờ đối soát')
        -- Chưa có bảng đối soát
        AND NOT EXISTS (
            SELECT 1 FROM Bang_Doi_Soat_Coc ds WHERE ds.MaHopDong = hd.MaHopDong
        )
        AND (@SDT IS NULL OR kh.SDT = @SDT)
        AND (@CCCD IS NULL OR kh.CCCD = @CCCD)
    ORDER BY hd.MaHopDong;
END;
GO

-- ============================================================
-- SP 2: Lấy danh sách phí phát sinh của một hợp đồng
-- (Dùng để hiển thị bảng khấu trừ chi tiết)
-- ============================================================
CREATE OR ALTER PROCEDURE sp_GetPhiPhatSinhTheoHopDong
    @MaHopDong VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pps.MaPhiPS,
        pps.LoaiPhi,
        pps.MoTa,
        pps.SoTien,
        pps.NgayGhiNhan,
        nv.HoTen AS NhanVienGhiNhan,
        ktt.MaKyTT,
        ktt.NgayBatDauKy,
        ktt.NgayKetThucKy
    FROM Phi_Phat_Sinh pps
    INNER JOIN Ky_Thanh_Toan_Thue ktt ON pps.MaKyTT = ktt.MaKyTT
    LEFT  JOIN Nhan_Vien nv            ON pps.NhanVienGhiNhan = nv.MaNV
    WHERE ktt.MaHopDong = @MaHopDong
    ORDER BY pps.NgayGhiNhan;
END;
GO

-- ============================================================
-- SP 3: Lấy tổng tiền thuê còn nợ chưa thanh toán
-- ============================================================
CREATE OR ALTER PROCEDURE sp_GetTongNoTienThue
    @MaHopDong VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        COUNT(*)            AS SoKyConNo,
        ISNULL(SUM(SoTienConNo), 0) AS TongTienConNo
    FROM Ky_Thanh_Toan_Thue
    WHERE MaHopDong = @MaHopDong
      AND TrangThai IN (N'Chưa thanh toán', N'Quá hạn');
END;
GO

-- ============================================================
-- SP 4: Lưu bảng đối soát cọc vào CSDL
-- ============================================================
CREATE OR ALTER PROCEDURE sp_LuuBangDoiSoatCoc
    @MaDS                  VARCHAR(20),
    @MaHopDong             VARCHAR(20),
    @NgayTraPhong          DATE,
    @DaKyHopDong           BIT,
    @HetHanHopDong         BIT,
    @TyLeHoanCoc           DECIMAL(5,2),
    @SoTienCocDuocXetHoan  DECIMAL(18,2),
    @TongTienKhauTru       DECIMAL(18,2),
    @SoTienHoanLai         DECIMAL(18,2),
    @SoTienPhaiDongThem    DECIMAL(18,2),
    @GhiChu                NVARCHAR(500) = NULL,
    @KetQua                NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra chưa tồn tại bảng đối soát cho hợp đồng này
        IF EXISTS (SELECT 1 FROM Bang_Doi_Soat_Coc WHERE MaHopDong = @MaHopDong)
        BEGIN
            SET @KetQua = N'Hợp đồng này đã có bảng đối soát cọc, không thể tạo thêm!';
            ROLLBACK; RETURN;
        END

        INSERT INTO Bang_Doi_Soat_Coc (
            MaDS, MaHopDong, NgayTraPhong,
            DaKyHopDong, HetHangHopDong,
            TyLeHoanCoc, SoTienCocDuocXetHoan,
            TongTienKhauTru, SoTienHoanLai,
            SoTienPhaiDongThem, NgayLapDoiSoat,
            TrangThaiDuyet, GhiChu
        ) VALUES (
            @MaDS, @MaHopDong, @NgayTraPhong,
            @DaKyHopDong, @HetHanHopDong,
            @TyLeHoanCoc, @SoTienCocDuocXetHoan,
            @TongTienKhauTru, @SoTienHoanLai,
            @SoTienPhaiDongThem, GETDATE(),
            N'Chờ kế toán xác nhận', @GhiChu
        );

        -- Cập nhật trạng thái hợp đồng
        UPDATE Hop_Dong_Thue
        SET TrangThai = N'Chờ thanh lý'
        WHERE MaHopDong = @MaHopDong;

        SET @KetQua = N'SUCCESS';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SET @KetQua = N'Lỗi hệ thống: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

USE KTX_HomeStay_Full
GO
-- 1. Hàm lấy danh sách Hồ sơ cư trú (Đã duyệt và chưa có hợp đồng)
CREATE OR ALTER PROCEDURE sp_GetHoSoCuTru_DaDuyet
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ct.maCuTru AS MaHoSo,
        kh.hoTen AS TenKhachHang,
        ct.maCuTru + ' - ' + kh.hoTen AS DisplayText
    FROM Thong_tin_cu_tru ct
    INNER JOIN Khach_hang kh ON ct.maCuTru = kh.maCuTru
    WHERE ct.trangThaiTamTru = N'Đã đăng ký'
      AND kh.maKH NOT IN (SELECT maKH FROM Hop_Dong_Thue)
END
GO

-- 2. Hàm trích xuất chi tiết hồ sơ để lập hợp đồng
CREATE OR ALTER PROCEDURE sp_GetChiTietHoSoLapHopDong
    @MaHoSo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1
        ct.maCuTru AS MaHoSo,
        kh.maKH AS MaKH,
        kh.hoTen AS TenKhachHang,
        kh.CCCD AS CCCD,
        pdc.MaPhieu AS MaPhieuDatCoc,
        pdc.SoTienCoc AS TienCocTuDong,
        p.MaPhong AS MaPhong,
        p.TenPhong AS TenPhong,
        p.GiaThuePhong AS DonGiaThue
    FROM Thong_tin_cu_tru ct
    INNER JOIN Khach_hang kh ON ct.maCuTru = kh.maCuTru
    LEFT JOIN Phieu_Dat_Coc pdc ON kh.maKH = pdc.maKH
    LEFT JOIN Phong p ON pdc.MaPhong = p.MaPhong
    WHERE ct.maCuTru = @MaHoSo
END
GO

-- 3. Hàm lấy danh sách dịch vụ
CREATE OR ALTER PROCEDURE sp_GetDanhSachDichVu
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        MaDV, 
        TenDV, 
        DonGia,
        TenDV + N' (' + FORMAT(DonGia, 'N0') + N'đ)' AS DisplayText
    FROM Dich_Vu
END
GO

-- 4. Hàm Insert Hợp đồng mới (Căn chỉnh theo HopDongThue_DTO)
CREATE OR ALTER PROCEDURE sp_Insert_HopDongThue
    @MaHopDong VARCHAR(20),
    @MaKH VARCHAR(20),
    @MaPhong VARCHAR(20),
    @MaPhieuCoc VARCHAR(20),
    @NgayBatDau DATE,
    @ThoiHanThang INT,
    @KyThanhToan NVARCHAR(50),
    @TienCoc DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    -- Tính ngày kết thúc dựa vào số tháng
    DECLARE @NgayKetThuc DATE = DATEADD(MONTH, @ThoiHanThang, @NgayBatDau);

    INSERT INTO Hop_Dong_Thue (
        MaHopDong, maKH, MaPhong, MaPhieu, 
        NgayLap, NgayBatDau, NgayKetThuc, 
        KyThanhToan, GiaThue, TrangThai
    )
    VALUES (
        @MaHopDong, @MaKH, @MaPhong, @MaPhieuCoc, 
        GETDATE(), @NgayBatDau, @NgayKetThuc, 
        @KyThanhToan, @TienCoc, -- Dựa vào HopDongThue_DTO để truyền
        N'Chờ thanh toán' 
    );
END
GO

-- 5. Hàm Insert Dịch vụ đi kèm hợp đồng
CREATE OR ALTER PROCEDURE sp_Insert_HopDong_DichVu
    @MaHopDong VARCHAR(20),
    @MaDV VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Hop_Dong_Dich_Vu (MaHopDong, MaDV)
    VALUES (@MaHopDong, @MaDV);
END
GO
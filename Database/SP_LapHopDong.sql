USE KTX_HomeStay_Full;
GO
--1. Lấy danh sách hồ sơ cư trú đã được duyệt và chưa có hợp đồng thuê
CREATE PROCEDURE sp_GetHoSoCuTru_DaDuyet
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ct.maCuTru AS MaHoSo,
        kh.hoTen AS TenKhachHang,
        ct.maCuTru + ' - ' + kh.hoTen AS DisplayText
    FROM Thong_tin_cu_tru ct
    INNER JOIN Khach_hang kh ON ct.maCuTru = kh.maCuTru
    WHERE ct.trangThaiTamTru = N'Đã phê duyệt'
      -- Chỉ lấy những khách hàng chưa có hợp đồng thuê
      AND kh.maKH NOT IN (SELECT maKH FROM Hop_Dong_Thue)
END
GO

--2. Lấy chi tiết hồ sơ cư trú theo mã hồ sơ (maCuTru)
CREATE PROCEDURE sp_GetChiTietHoSoLapHopDong
    @MaHoSo VARCHAR(20) -- Tương ứng với maCuTru
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
        
        -- Lấy chính xác Phòng dựa vào Phiếu đặt cọc của khách hàng đó
        p.MaPhong AS MaPhong,
        p.TenPhong AS TenPhong,
        p.GiaThuePhong AS DonGiaThue
        
    FROM Thong_tin_cu_tru ct
    INNER JOIN Khach_hang kh ON ct.maCuTru = kh.maCuTru
    LEFT JOIN Phieu_Dat_Coc pdc ON kh.maKH = pdc.maKH
    LEFT JOIN Phong p ON pdc.MaPhong = p.MaPhong
    WHERE ct.maCuTru = @MaHoSo AND pdc.TrangThai = N'Đã thu'
END
GO

--3. Lấy danh sách dịch vụ
CREATE PROCEDURE sp_GetDanhSachDichVu
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

--4. Nhập hợp đồng thuê mới
CREATE PROCEDURE sp_Insert_HopDongThue
    @MaHopDong VARCHAR(20),
    @MaKH VARCHAR(20),
    @MaPhong VARCHAR(20),
    @MaPhieu VARCHAR(20),
    @NgayBatDau DATE,
    @NgayKetThuc DATE,
    @KyThanhToan NVARCHAR(50),
    @GiaThue DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Hop_Dong_Thue (
        MaHopDong, maKH, MaPhong, MaPhieu, 
        NgayLap, NgayBatDau, NgayKetThuc, 
        KyThanhToan, GiaThue, TrangThai
    )
    VALUES (
        @MaHopDong, @MaKH, @MaPhong, @MaPhieu, 
        GETDATE(), @NgayBatDau, @NgayKetThuc, 
        @KyThanhToan, @GiaThue, 
        N'Chờ thanh toán' -- Trạng thái theo Use Case
    );
END
GO

--5. Nhập dịch vụ vào hợp đồng
CREATE PROCEDURE sp_Insert_HopDong_DichVu
    @MaHopDong VARCHAR(20),
    @MaDV VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Hop_Dong_Dich_Vu (MaHopDong, MaDV)
    VALUES (@MaHopDong, @MaDV);
END
GO
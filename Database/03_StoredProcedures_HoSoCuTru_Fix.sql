USE HomeStayDorm;
GO

IF OBJECT_ID('sp_GetHoSoCuTruByDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetHoSoCuTruByDatCoc;
GO

CREATE PROCEDURE sp_GetHoSoCuTruByDatCoc
    @maDatCoc INT
AS
BEGIN
    DECLARE @maNhom INT;
    -- Lấy mã nhóm từ phiếu cọc
    SELECT @maNhom = k.MaNhom 
    FROM Phieu_Dat_Coc p
    INNER JOIN Khach_hang k ON p.maKH = k.maKH
    WHERE p.MaPhieu = @maDatCoc;

    IF @maNhom IS NOT NULL
    BEGIN
        SELECT 
            k.maKH AS STT, -- Dùng tạm maKH làm ID/STT
            k.hoTen,
            k.CCCD,
            k.SDT,
            k.gioiTinh,
            k.QuocTich AS queQuan
        FROM Khach_hang k
        WHERE k.MaNhom = @maNhom;
    END
END
GO

IF OBJECT_ID('sp_DeleteHoSoCuTru', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteHoSoCuTru;
GO

CREATE PROCEDURE sp_DeleteHoSoCuTru
    @maKH INT
AS
BEGIN
    -- Delete from child table first
    DELETE FROM Thong_tin_cu_tru WHERE maKH = @maKH;
    -- Delete from Khach_hang
    DELETE FROM Khach_hang WHERE maKH = @maKH;
END
GO

IF OBJECT_ID('sp_UpdateTrangThaiPhieuDatCoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateTrangThaiPhieuDatCoc;
GO

CREATE PROCEDURE sp_UpdateTrangThaiPhieuDatCoc
    @maDatCoc INT,
    @trangThai NVARCHAR(50)
AS
BEGIN
    UPDATE Phieu_Dat_Coc
    SET TrangThai = @trangThai
    WHERE MaPhieu = @maDatCoc;
END
GO

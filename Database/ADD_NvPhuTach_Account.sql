USE KTX_HomeStay_Full;
GO

-- 1. Sửa lỗi font chữ do chạy script bị sai encoding trước đó
UPDATE Nhan_Vien 
SET HoTen = N'Nguyễn Phụ Trách', Email = 'nvphutrach', VaiTro = N'Nhân viên phụ trách'
WHERE Email = 'nvphutach' OR HoTen LIKE N'Nguy%';

-- 2. Đảm bảo tài khoản tồn tại đúng chuẩn
IF NOT EXISTS (SELECT 1 FROM Nhan_Vien WHERE Email = 'nvphutrach')
BEGIN
    DECLARE @NewMaNV VARCHAR(20);
    SELECT @NewMaNV = 'NV' + RIGHT('000' + CAST(ISNULL(MAX(CAST(SUBSTRING(MaNV, 3, 10) AS INT)), 0) + 1 AS VARCHAR), 3)
    FROM Nhan_Vien WHERE MaNV LIKE 'NV%';

    IF @NewMaNV IS NULL SET @NewMaNV = 'NV001';

    INSERT INTO Nhan_Vien (MaNV, MaCN, HoTen, Email, VaiTro, MatKhau)
    VALUES (@NewMaNV, 'CN01', N'Nguyễn Phụ Trách', 'nvphutrach', N'Nhân viên phụ trách', '123456');

    PRINT N'Đã thêm tài khoản NvPhuTrach thành công với mã: ' + @NewMaNV;
END
ELSE
BEGIN
    PRINT N'Tài khoản nvphutrach đã sẵn sàng.';
END
GO

SELECT MaNV, HoTen, Email as TenDangNhap, VaiTro FROM Nhan_Vien
WHERE VaiTro IN ('Sale', N'Quản lý', N'Kế toán', N'Nhân viên phụ trách')
ORDER BY VaiTro;
GO

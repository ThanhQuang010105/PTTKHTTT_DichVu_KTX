USE HomeStayDorm;
GO

-- Xóa dữ liệu cũ (nếu có) để không bị lỗi trùng lặp khi chạy nhiều lần
ALTER TABLE Nhom_Thue NOCHECK CONSTRAINT FK_NhomThue_NguoiDaiDien;
DELETE FROM Phieu_Dat_Coc;
DELETE FROM Khach_hang;
DELETE FROM Nhom_Thue;
DELETE FROM Giuong;
DELETE FROM Phong;
DELETE FROM Nhan_Vien;
DELETE FROM Chi_Nhanh;

-- Bật IDENTITY_INSERT cho từng bảng để chèn đúng Khóa chính (tránh lỗi lệch ID)
SET IDENTITY_INSERT Chi_Nhanh ON;
INSERT INTO Chi_Nhanh (MaCN, TenChiNhanh, DiaChi, SDT, Email, KhuVuc, TrangThai, NgayThanhLap)
VALUES 
(1, N'Chi nhánh 1', N'Quận 1, TP.HCM', '0901000000', 'cn1@homestay.com', N'Khu trung tâm', N'Đang hoạt động', '2020-01-01');
SET IDENTITY_INSERT Chi_Nhanh OFF;

SET IDENTITY_INSERT Nhan_Vien ON;
INSERT INTO Nhan_Vien (MaNV, HoTen, SDT, DiaChi, Email, VaiTro, MaCN)
VALUES 
(1, N'Nhân viên Sale 1', '0901000001', N'TP.HCM', 'sale1@homestay.com', 'Sale', 1);
SET IDENTITY_INSERT Nhan_Vien OFF;

SET IDENTITY_INSERT Phong ON;
INSERT INTO Phong (MaPhong, TenPhong, KhuVuc, LoaiPhong, GioiTinhQuyDinh, SucChuaToiDa, GiaThuePhong, TrangThaiPhong, SoLuongGiuong, MaCN)
VALUES 
(1, N'Phòng 1', N'Tầng 1', N'Thường', 'Nam', 4, 3000000, N'Đang trống', 4, 1),
(2, N'Phòng 2', N'Tầng 2', N'VIP', N'Nữ', 4, 4000000, N'Đang trống', 4, 1),
(3, N'Phòng 3', N'Tầng 3', N'Thường', 'Nam', 6, 2500000, N'Đang trống', 6, 1);
SET IDENTITY_INSERT Phong OFF;

SET IDENTITY_INSERT Nhom_Thue ON;
INSERT INTO Nhom_Thue (MaNhom, SoLuong, NguoiDaiDien)
VALUES 
(1, 1, NULL),
(2, 2, NULL),
(3, 1, NULL);
SET IDENTITY_INSERT Nhom_Thue OFF;

SET IDENTITY_INSERT Khach_hang ON;
INSERT INTO Khach_hang (maKH, hoTen, CCCD, SDT, email, gioiTinh, QuocTich, KhachHangTC, MaNhom)
VALUES 
(1, N'Nguyễn Văn A', '079123456789', '0123456789', 'nva@gmail.com', 'Nam', N'Việt Nam', N'Tốt', 1),
(2, N'Nguyễn Văn B', '079987654321', '0123456789', 'nvb@gmail.com', 'Nam', N'Việt Nam', N'Tốt', 2),
(3, N'Nguyễn Văn C', '079111222333', '0123456789', 'nvc@gmail.com', 'Nam', N'Việt Nam', N'Tốt', 3);
SET IDENTITY_INSERT Khach_hang OFF;

-- Bật lại constraint và update
ALTER TABLE Nhom_Thue CHECK CONSTRAINT FK_NhomThue_NguoiDaiDien;
UPDATE Nhom_Thue SET NguoiDaiDien = 1 WHERE MaNhom = 1;
UPDATE Nhom_Thue SET NguoiDaiDien = 2 WHERE MaNhom = 2;
UPDATE Nhom_Thue SET NguoiDaiDien = 3 WHERE MaNhom = 3;

SET IDENTITY_INSERT Phieu_Dat_Coc ON;
INSERT INTO Phieu_Dat_Coc (MaPhieu, NgayDatCoc, SoTienCoc, SoGiuongCoc, HinhThucThanhToan, HinhAnhMinhChung, TrangThai, maKH, MaNV, MaPhong)
VALUES 
(1, '2026-07-01 10:00:00', 1000000, 1, N'Chuyển khoản', 'img1.png', N'Chờ bổ sung', 1, 1, 1),
(2, '2026-07-02 11:30:00', 2000000, 2, N'Tiền mặt', 'img2.png', N'Đã duyệt', 2, 1, 2),
(3, '2026-07-03 15:45:00', 1000000, 1, N'Chuyển khoản', 'img3.png', N'Đã hủy / Quá hạn', 3, 1, 3);
SET IDENTITY_INSERT Phieu_Dat_Coc OFF;
GO

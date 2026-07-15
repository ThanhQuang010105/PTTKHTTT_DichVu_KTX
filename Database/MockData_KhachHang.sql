-- =====================================================================
-- Script tạo dữ liệu mẫu cho bảng Khach_hang (Test chức năng Tra Cứu)
-- =====================================================================

USE KTX_HomeStay_Full;
GO

-- Xóa dữ liệu cũ (chỉ dành cho các record test nếu có)
-- Lưu ý: Không xóa các bản ghi đang bị dính khóa ngoại (nếu có Hop_Dong_Thue, Phieu_Dat_Coc)
DELETE FROM Khach_hang WHERE maKH LIKE 'TEST%';

-- Thêm dữ liệu mẫu
INSERT INTO Khach_hang (maKH, hoTen, CCCD, SDT, email, gioiTinh, QuocTich, KhaNangTC)
VALUES 
    ('TEST001', N'Trần Văn Long', '079098001122', '0987654321', 'longtv@gmail.com', N'Nam', N'Việt Nam', N'Khá'),
    ('TEST002', N'Nguyễn Thị Mai', '079098002233', '0912345678', 'maint@gmail.com', N'Nữ', N'Việt Nam', N'Trung bình'),
    ('TEST003', N'Lê Minh Khang', '079098003344', '0901234567', 'khanglm@yahoo.com', N'Nam', N'Việt Nam', N'Tốt'),
    ('TEST004', N'Phạm Hoàng Oanh', '079098004455', '0934567890', 'oanhph@hotmail.com', N'Nữ', N'Việt Nam', N'Khá'),
    ('TEST005', N'Đỗ Hữu Trí', '079098005566', '0978123456', 'tridh@gmail.com', N'Nam', N'Việt Nam', N'Tốt'),
    ('TEST006', N'John Smith', '079098006677', '0966778899', 'john.smith@gmail.com', N'Nam', N'Mỹ', N'Tốt'),
    ('TEST007', N'Võ Thị Ngọc', '079098007788', '0988776655', 'ngocvt@gmail.com', N'Nữ', N'Việt Nam', N'Trung bình'),
    ('TEST008', N'Lý Tiểu Long', '079098008899', '0944332211', 'longlt@gmail.com', N'Nam', N'Trung Quốc', N'Khá');
GO

PRINT N'Đã thêm dữ liệu mẫu Khách hàng thành công!';

-- ============================================================
-- FILE TEST: Chuẩn bị dữ liệu để test màn hình Đối soát cọc
-- Chạy file này trên SSMS trước khi nhấn F5 trong Visual Studio
-- ============================================================
USE KTX_HomeStay_Full;
GO

-- ── BƯỚC 1: DỌN DẸP DỮ LIỆU CŨ (Để chạy đi chạy lại không bị lỗi) ──

-- 1.1 Xóa bảng đối soát cọc cũ (nếu đã lỡ tạo trong màn hình Đối soát)
DELETE FROM Bang_Doi_Soat_Coc
WHERE MaHopDong = 'HD000123';

-- 1.2 Xóa các kỳ thanh toán sinh ra do màn hình "Tạo yêu cầu trả phòng"
DELETE FROM Ky_Thanh_Toan_Thue
WHERE MaHopDong = 'HD000123' AND TrangThai = N'Yêu cầu trả phòng';

PRINT '✅ Đã dọn dẹp sạch sẽ dữ liệu test cũ (Bảng đối soát & Kỳ thanh toán rác)';
GO

-- ── BƯỚC 2: KHÔI PHỤC HỢP ĐỒNG ──
-- Đưa hợp đồng về trạng thái "Đang hiệu lực" để có thể test TỪ ĐẦU 
-- (Test từ màn hình "Tạo yêu cầu trả phòng" -> Xong mới qua "Đối soát cọc")
UPDATE Hop_Dong_Thue
SET TrangThai = N'Đang hiệu lực'
WHERE MaHopDong = 'HD000123';

PRINT '✅ Đã khôi phục HD000123 → Đang hiệu lực';
GO

-- ── BƯỚC 3: Kiểm tra dữ liệu phí phát sinh đã có chưa
SELECT 
    pps.MaPhiPS, pps.LoaiPhi, pps.MoTa, pps.SoTien,
    ktt.MaKyTT, ktt.MaHopDong
FROM Phi_Phat_Sinh pps
INNER JOIN Ky_Thanh_Toan_Thue ktt ON pps.MaKyTT = ktt.MaKyTT
WHERE ktt.MaHopDong = 'HD000123';

-- Kết quả mong đợi:
-- PS001 | Hư hỏng | Cửa tủ bị gãy bản lề | 200,000
-- PS002 | Mất mát | Làm mất 1 thẻ từ     | 100,000
GO

-- ── BƯỚC 4: Kiểm tra kỳ thanh toán còn nợ
SELECT 
    MaKyTT, MaHopDong, NgayBatDauKy, NgayKetThucKy,
    SoTienPhaiTra, SoTienDaTra, SoTienConNo, TrangThai
FROM Ky_Thanh_Toan_Thue
WHERE MaHopDong = 'HD000123'
ORDER BY NgayBatDauKy;

-- Kết quả mong đợi:
-- KTT01 | Đã đóng | SoTienConNo = 0
-- KTT02 | Đã đóng | SoTienConNo = 0
GO

-- ── BƯỚC 5: Kiểm tra lại hợp đồng xuất hiện trong danh sách chờ đối soát
SELECT 
    hd.MaHopDong,
    kh.hoTen AS TenKhachHang,
    kh.SDT,
    p.TenPhong,
    hd.NgayBatDau,
    hd.NgayKetThuc,
    hd.TrangThai,
    pdc.SoTienCoc AS TienCocGoc,
    DATEDIFF(MONTH, hd.NgayBatDau, GETDATE()) AS SoThangDaO,
    CASE WHEN GETDATE() >= hd.NgayKetThuc THEN 'Hết hạn' ELSE 'Chưa hết hạn' END AS TrangThaiHD
FROM Hop_Dong_Thue hd
INNER JOIN Khach_hang kh     ON hd.maKH    = kh.maKH
INNER JOIN Phong p            ON hd.MaPhong = p.MaPhong
INNER JOIN Phieu_Dat_Coc pdc  ON hd.MaPhieu = pdc.MaPhieu
WHERE hd.TrangThai IN (N'Chờ kiểm tra hiện trạng', N'Chờ đối soát')
  AND NOT EXISTS (SELECT 1 FROM Bang_Doi_Soat_Coc ds WHERE ds.MaHopDong = hd.MaHopDong);

-- Kết quả mong đợi: Phải thấy hàng HD000123 - Nguyễn Văn A
GO

PRINT '=========================================================';
PRINT '✅ DATA ĐÃ RESET SẴN SÀNG! ';
PRINT '---------------------------------------------------------';
PRINT 'Quy trình Test chuẩn để không bị lỗi:';
PRINT '1. Chạy màn hình TẠO YÊU CẦU TRẢ PHÒNG (Tìm 0909123456 -> Tạo yêu cầu)';
PRINT '2. Tắt màn hình, chuyển qua chạy ĐỐI SOÁT CỌC (Tìm 0909123456 -> Tính toán -> Xác nhận)';
PRINT '3. Mỗi lần muốn test lại từ đầu, CHỈ CẦN CHẠY LẠI SCRIPT NÀY LÀ XONG!';
PRINT '=========================================================';
GO

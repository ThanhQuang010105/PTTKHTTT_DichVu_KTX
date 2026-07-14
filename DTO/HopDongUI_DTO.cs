using System;

namespace HomeStayDorm.DTO
{
    // DTO này dùng để load danh sách Hồ sơ vào ComboBox
    public class HoSoCuTru_DTO
    {
        public string MaHoSo { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public string DisplayText { get; set; } = string.Empty;
    }

    // DTO này dùng để hứng dữ liệu trích xuất tự động khi User chọn Combobox
    public class ChiTietHoSo_DTO
    {
        public string MaHoSo { get; set; } = string.Empty;
        public string MaKH { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public string CCCD { get; set; } = string.Empty;
        public string MaPhieuDatCoc { get; set; } = string.Empty;
        public decimal TienCocTuDong { get; set; }
        public string MaPhong { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public decimal DonGiaThue { get; set; }
    }

    // DTO này dùng để load danh sách Dịch vụ vào CheckedListBox
    public class DichVu_DTO
    {
        public string MaDV { get; set; } = string.Empty;
        public string TenDV { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string DisplayText { get; set; } = string.Empty;
    }
}
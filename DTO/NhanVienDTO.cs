using System;

namespace HomeStayDorm.DTO
{
    public class NhanVienDTO
    {
        public string MaNhanVien { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string TenDangNhap { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string VaiTro { get; set; } = "Sale";
        public string? MaChiNhanh { get; set; }
        public string? TenChiNhanh { get; set; }
        public string? MatKhauHash { get; set; }
        public string TrangThai { get; set; } = "Đang làm";
        public DateTime NgayVaoLam { get; set; } = DateTime.Today;
    }
}

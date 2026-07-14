using System;

namespace HomeStayDorm.DTO
{
    public class KhachHangDTO
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? CCCD { get; set; }
        public string? Email { get; set; }
        public string GioiTinh { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}

using System;

namespace HomeStayDorm.DTO
{
    public class LichXemPhongDTO
    {
        public string MaLich { get; set; } = string.Empty;
        public string MaDangKy { get; set; } = string.Empty;
        public string LoaiDoiTuong { get; set; } = string.Empty;
        public string? MaPhong { get; set; }
        public string? MaGiuong { get; set; }
        public DateTime NgayGioHen { get; set; } = DateTime.Now.AddDays(1);
        public string? GhiChu { get; set; }
        public string TrangThai { get; set; } = "Đã hẹn";
    }
}

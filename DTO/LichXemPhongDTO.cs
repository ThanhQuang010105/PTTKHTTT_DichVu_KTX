using System;

namespace HomeStayDorm.DTO
{
    public class LichXemPhongDTO
    {
        public int MaLich { get; set; }
        public string MaDangKy { get; set; } = string.Empty;
        public string LoaiDoiTuong { get; set; } = string.Empty;
        public int? MaPhong { get; set; }
        public int? MaGiuong { get; set; }
        public DateTime NgayGioHen { get; set; } = DateTime.Now.AddDays(1);
        public string? GhiChu { get; set; }
        public string TrangThai { get; set; } = "Đã hẹn";
    }
}

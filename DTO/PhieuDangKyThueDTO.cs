using System;

namespace HomeStayDorm.DTO
{
    public class PhieuDangKyThueDTO
    {
        public string MaDangKy { get; set; } = string.Empty;
        public int? MaKhachHang { get; set; }
        public string HoTenKhachHang { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? CCCD { get; set; }
        public string? Email { get; set; }
        public string GioiTinh { get; set; } = string.Empty;
        public string HinhThucThue { get; set; } = string.Empty;
        public string KhuVucMongMuon { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = string.Empty;
        public int SoNguoiDuKien { get; set; }
        public decimal GiaToiDa { get; set; }
        public DateTime NgayVaoDuKien { get; set; } = DateTime.Today;
        public int ThoiHanThueThang { get; set; }
        public string? TieuChiUuTien { get; set; }
        public string TrangThai { get; set; } = "MoiTao";
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}

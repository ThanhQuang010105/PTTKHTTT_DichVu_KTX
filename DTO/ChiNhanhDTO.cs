namespace HomeStayDorm.DTO
{
    public class ChiNhanhDTO
    {
        public string MaChiNhanh { get; set; } = string.Empty;
        public string TenChiNhanh { get; set; } = string.Empty;
        public string KhuVuc { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string TrangThai { get; set; } = "Hoạt động";
    }
}

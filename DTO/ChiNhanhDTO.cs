namespace HomeStayDorm.DTO
{
    public class ChiNhanhDTO
    {
        public int MaChiNhanh { get; set; }
        public string TenChiNhanh { get; set; } = string.Empty;
        public string KhuVuc { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string TrangThai { get; set; } = "Hoạt động";
    }
}

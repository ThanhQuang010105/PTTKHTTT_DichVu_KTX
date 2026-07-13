namespace HomeStayDorm.DTO
{
    public class GiuongDTO
    {
        public int MaGiuong { get; set; }
        public int MaPhong { get; set; }
        public string TenGiuong { get; set; } = string.Empty;
        public decimal GiaThue { get; set; }
        public string TrangThaiGiuong { get; set; } = string.Empty;
    }
}

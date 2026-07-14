namespace HomeStayDorm.DTO
{
    public class PhongGiuong_DTO
    {
        public string MaPhong { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public string ChiNhanh { get; set; } = string.Empty;
        public decimal GiaThue { get; set; }
        public int SoGiuongThue { get; set; } 
        public bool ThueNguyenPhong { get; set; }
    }
}
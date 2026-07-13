namespace HomeStayDorm.DTO
{
    public class PhongDTO
    {
        public int MaPhong { get; set; }
        public int MaChiNhanh { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public string KhuVuc { get; set; } = string.Empty;
        public string GioiTinhQuyDinh { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = string.Empty;
        public int SucChua { get; set; }
        public decimal GiaThue { get; set; }
        public string TrangThaiPhong { get; set; } = string.Empty;
    }
}

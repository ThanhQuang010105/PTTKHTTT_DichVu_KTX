namespace HomeStayDorm.DTO
{

    public class PhongDTO
    {
        public string MaPhong { get; set; } = string.Empty;
        public string MaChiNhanh { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public string KhuVuc { get; set; } = string.Empty;
        public string GioiTinhQuyDinh { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = string.Empty;
        public int SucChua { get; set; }
        public decimal GiaThue { get; set; }
        public string TrangThaiPhong { get; set; } = string.Empty;
    }


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


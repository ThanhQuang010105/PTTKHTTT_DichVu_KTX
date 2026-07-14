namespace HomeStayDorm.DTO
{
    public class HopDongThue_DTO
    {
        public string MaHopDong { get; set; } = string.Empty;
        public string MaKH { get; set; } = string.Empty;
        public string MaPhong { get; set; } = string.Empty;
        public string MaPhieuCoc { get; set; } = string.Empty;
        public DateTime NgayBatDau { get; set; }
        public int ThoiHanThang { get; set; }
        public string KyThanhToan { get; set; } = string.Empty;
        public decimal TienCoc { get; set; }
    }
}
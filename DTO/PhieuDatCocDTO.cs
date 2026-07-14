using System;

namespace HomeStayDorm.DTO
{
    public class PhieuDatCocDTO
    {
        // Properties mapping to Class Diagram
        public string maPhieuCoc { get; set; }
        public string maKhachHang { get; set; }
        public string soCCCD { get; set; }
        public string soDienThoai { get; set; }
        public decimal soTienCoc { get; set; }
        public string trangThai { get; set; }
        public DateTime ngayLap { get; set; }

        // Additional properties for UI display (DataGridView)
        public int STT { get; set; }
        public string KhachDaiDien { get; set; }
        public string SDT { get; set; }
        public string PhongGiuongCoc { get; set; }
        public int SoGiuongDat { get; set; }
        public string MaPhieuGoc { get; set; }  // varchar 'PC000123'
        public string GioiTinhPhong { get; set; }
    }
}

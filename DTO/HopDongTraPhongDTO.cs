using System;

namespace HomeStayDorm.DTO
{
    /// <summary>
    /// DTO ánh xạ thông tin hợp đồng dùng cho màn hình Tạo yêu cầu trả phòng.
    /// </summary>
    public class HopDongTraPhongDTO
    {
        public string MaHopDong           { get; set; }
        public string MaKhachHang         { get; set; }
        public string TenKhachHang        { get; set; }
        public string CCCD                { get; set; }
        public string SoDienThoai         { get; set; }
        public string Email               { get; set; }
        public string DiaChi              { get; set; }
        public string TenPhong            { get; set; }
        public string MaPhong             { get; set; }
        public string LoaiThue            { get; set; }
        public DateTime NgayBatDau        { get; set; }
        public DateTime NgayKetThuc       { get; set; }
        public decimal GiaThue            { get; set; }
        public string TrangThaiHopDong    { get; set; }
        public decimal TienCocGoc         { get; set; }
        public string MaPhieuCoc          { get; set; }

        /// <summary>Số tháng đã lưu trú tính đến hiện tại.</summary>
        public int SoThangDaO =>
            (int)((DateTime.Now - NgayBatDau).TotalDays / 30);
    }

    /// <summary>
    /// DTO chứa thông tin người dùng gửi lên khi tạo yêu cầu trả phòng.
    /// </summary>
    public class YeuCauTraPhongDTO
    {
        public string   MaHopDong       { get; set; }
        public DateTime NgayDuKienTra   { get; set; }
        public string   LyDoTraPhong    { get; set; }
        public string   GhiChu          { get; set; }
        public string   MaNhanVienSale  { get; set; }
    }
}

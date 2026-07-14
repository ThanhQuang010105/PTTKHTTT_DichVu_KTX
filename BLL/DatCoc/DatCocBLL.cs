using HomeStayDorm.DAO;
using HomeStayDorm.DTO;
using System.Data;

namespace HomeStayDorm.BUS
{
    public class HopDong_BUS
    {
        private readonly HopDong_DAO _dao = new HopDong_DAO();

        public DataTable LayDanhSachPhieuCoc() => _dao.GetDanhSachPhieuCocHopLe();

        public PhongGiuong_DTO LayChiTietPhong(string maPhieu) => _dao.GetThongTinPhongGiuong(maPhieu);

        /// <summary>
        /// Nghiệp vụ cốt lõi: Tính tiền cọc theo quy định.
        /// Công thức: Tiền cọc = (Tiền thuê 2 tháng) x (Số giường thuê)
        /// </summary>
        public decimal TinhTienQuyDinhCoc(decimal giaThuePhong, int soGiuongThue, bool isThueNguyenPhong, int sucChuaToiDa)
        {
            int soGiuongTinhCoc = soGiuongThue;
            
            // Xử lý logic: Nếu thuê nguyên phòng, số giường tính cọc = sức chứa tối đa của phòng
            if (isThueNguyenPhong)
            {
                soGiuongTinhCoc = sucChuaToiDa;
            }

            return (giaThuePhong * 2) * soGiuongTinhCoc;
        }

        public bool XuLyTaoHopDong(HopDongThue_DTO hd)
        {
            // Tự động sinh mã hợp đồng (Ví dụ: HD-2026-06-1234)
            hd.MaHopDong = $"HD-{DateTime.Now:yyyy-MM}-{new Random().Next(1000, 9999)}";
            
            // Có thể bổ sung gọi DAO_PhieuThu để tạo phiếu thu tiền kỳ 1 tại đây
            return _dao.InsertHopDong(hd);
        }
    }
}
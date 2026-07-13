using System.Data;

namespace HomeStayDorm.DAL.TienIch
{
    public class BaoCaoDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayThongKeDashboard()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    (SELECT COUNT(*) FROM dbo.Phong) AS TongPhong,
                    (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong <> N'Ngừng sử dụng') AS PhongTrong,
                    (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong = N'Đang ở') AS PhongDangO,
                    (SELECT COUNT(*) FROM dbo.Phong WHERE TrangThaiPhong = N'Đã đặt cọc') AS PhongDatCoc,
                    (SELECT COUNT(*) FROM dbo.Giuong) AS TongGiuong,
                    (SELECT COUNT(*) FROM dbo.Giuong WHERE TrangThaiGiuong = N'Trống') AS GiuongTrong,
                    (SELECT COUNT(*) FROM dbo.Dang_ky_thue WHERE TrangThai IN (N'Mới tạo', N'Chờ xử lý')) AS PhieuDangKyMoi,
                    (SELECT COUNT(*) FROM dbo.Lich_xem_phong) AS LichHen,
                    (SELECT COUNT(*) FROM dbo.Nhan_Vien WHERE MatKhau <> 'LOCKED') AS NhanVienDangLam");
        }
    }
}

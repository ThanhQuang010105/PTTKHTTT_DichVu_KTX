using System.Data;
using Microsoft.Data.SqlClient;
using HomeStayDorm.DTO;
using System.Configuration;

namespace HomeStayDorm.DAO
{
    public class HopDong_DAO
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["HomeStayDormDB"].ConnectionString;

        // Lấy danh sách khách hàng đã cọc thành công để load lên ComboBox
        public DataTable GetDanhSachPhieuCocHopLe()
        {
            using var conn = new SqlConnection(_connectionString);
            string query = @"SELECT p.MaPhieu, k.hoTen, k.SDT 
                             FROM Phieu_Dat_Coc p 
                             JOIN Khach_hang k ON p.maKH = k.maKH 
                             WHERE p.TrangThai = N'Đã duyệt'";
            using var cmd = new SqlCommand(query, conn);
            var dt = new DataTable();
            try
            {
                conn.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch { /* Xử lý log lỗi hệ thống */ }
            return dt;
        }

        // Lấy thông tin phòng/giường từ mã phiếu cọc
        public PhongGiuong_DTO GetThongTinPhongGiuong(string maPhieu)
        {
            // Trong thực tế sẽ JOIN từ Phieu_Dat_Coc -> Khach_hang -> Dang_ky_thue -> Phong
            // Dưới đây là code mô phỏng trả về DTO
            return new PhongGiuong_DTO 
            { 
                MaPhong = "P302", ChiNhanh = "Chi nhánh Quận 5", GiaThue = 2500000, 
                SoGiuongThue = 1, ThueNguyenPhong = false 
            };
        }

        public bool InsertHopDong(HopDongThue_DTO hd)
        {
            using var conn = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Hop_Dong_Thue (MaHopDong, maKH, MaPhong, MaPhieu, NgayLap, NgayBatDau, TrangThai) 
                             VALUES (@maHD, @maKH, @maPhong, @maPhieu, GETDATE(), @ngayBD, N'Chờ thanh toán')";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@maHD", hd.MaHopDong);
            cmd.Parameters.AddWithValue("@maKH", hd.MaKH);
            cmd.Parameters.AddWithValue("@maPhong", hd.MaPhong);
            cmd.Parameters.AddWithValue("@maPhieu", hd.MaPhieuCoc);
            cmd.Parameters.AddWithValue("@ngayBD", hd.NgayBatDau);
            
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }
    }
}
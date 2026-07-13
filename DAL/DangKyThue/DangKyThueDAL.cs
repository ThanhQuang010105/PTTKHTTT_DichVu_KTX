using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.DangKyThue
{
    public class DangKyThueDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSachDangKy()
        {
            return _db.ExecuteQuery("sp_DangKyThue_DanhSach");
        }

        public DataTable LayChiTietDangKy(string maDangKy)
        {
            return _db.ExecuteQuery("sp_DangKyThue_LayChiTiet", new[]
            {
                new SqlParameter("@MaDangKy", maDangKy)
            });
        }

        public string TaoPhieuDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            using SqlConnection conn = _db.GetConnection();
            using SqlCommand cmd = new SqlCommand("sp_TaoPhieuDangKyThue", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@HoTenKhachHang", phieuDangKy.HoTenKhachHang);
            cmd.Parameters.AddWithValue("@SoDienThoai", phieuDangKy.SoDienThoai);
            cmd.Parameters.AddWithValue("@CCCD", ToDbValue(phieuDangKy.CCCD));
            cmd.Parameters.AddWithValue("@Email", ToDbValue(phieuDangKy.Email));
            cmd.Parameters.AddWithValue("@GioiTinh", phieuDangKy.GioiTinh);
            cmd.Parameters.AddWithValue("@HinhThucThue", phieuDangKy.HinhThucThue);
            cmd.Parameters.AddWithValue("@KhuVucMongMuon", phieuDangKy.KhuVucMongMuon);
            cmd.Parameters.AddWithValue("@LoaiPhong", phieuDangKy.LoaiPhong);
            cmd.Parameters.AddWithValue("@SoNguoiDuKien", phieuDangKy.SoNguoiDuKien);
            cmd.Parameters.AddWithValue("@GiaToiDa", phieuDangKy.GiaToiDa);
            cmd.Parameters.AddWithValue("@NgayVaoDuKien", phieuDangKy.NgayVaoDuKien.Date);
            cmd.Parameters.AddWithValue("@ThoiHanThueThang", phieuDangKy.ThoiHanThueThang);
            cmd.Parameters.AddWithValue("@TieuChiUuTien", ToDbValue(phieuDangKy.TieuChiUuTien));

            SqlParameter maDangKyParam = new SqlParameter("@MaDangKy", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(maDangKyParam);

            conn.Open();
            cmd.ExecuteNonQuery();

            return Convert.ToString(maDangKyParam.Value) ?? string.Empty;
        }

        public void CapNhatTrangThai(string maDangKy, string trangThai)
        {
            _db.ExecuteNonQuery("sp_DangKyThue_CapNhatTrangThai", new[]
            {
                new SqlParameter("@MaDangKy", maDangKy),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        private static object ToDbValue(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();
        }
    }
}

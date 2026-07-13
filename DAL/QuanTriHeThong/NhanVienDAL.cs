using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class NhanVienDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable DocTheoTenDangNhapHoacEmail(string tenDangNhapHoacEmail)
        {
            return _db.ExecuteQuery("sp_NhanVien_DocTheoDangNhap", new[]
            {
                new SqlParameter("@TenDangNhapHoacEmail", tenDangNhapHoacEmail)
            });
        }

        public DataTable LayDanhSach()
        {
            return _db.ExecuteQuery("sp_NhanVien_DanhSach");
        }

        public int Luu(NhanVienDTO nhanVien)
        {
            SqlParameter maNhanVien = new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = nhanVien.MaNhanVien == 0 ? DBNull.Value : nhanVien.MaNhanVien
            };

            SqlParameter[] parameters =
            {
                maNhanVien,
                new SqlParameter("@HoTen", nhanVien.HoTen),
                new SqlParameter("@TenDangNhap", nhanVien.TenDangNhap),
                new SqlParameter("@Email", nhanVien.Email),
                new SqlParameter("@SoDienThoai", string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) ? DBNull.Value : nhanVien.SoDienThoai.Trim()),
                new SqlParameter("@VaiTro", nhanVien.VaiTro),
                new SqlParameter("@MaChiNhanh", nhanVien.MaChiNhanh.HasValue ? nhanVien.MaChiNhanh.Value : DBNull.Value),
                new SqlParameter("@MatKhauHash", string.IsNullOrWhiteSpace(nhanVien.MatKhauHash) ? DBNull.Value : nhanVien.MatKhauHash),
                new SqlParameter("@TrangThai", nhanVien.TrangThai),
                new SqlParameter("@NgayVaoLam", nhanVien.NgayVaoLam.Date)
            };

            _db.ExecuteNonQuery("sp_NhanVien_Luu", parameters);
            return Convert.ToInt32(maNhanVien.Value);
        }

        public void KhoaTaiKhoan(int maNhanVien)
        {
            _db.ExecuteNonQuery("sp_NhanVien_KhoaTaiKhoan", new[] { new SqlParameter("@MaNhanVien", maNhanVien) });
        }
    }
}

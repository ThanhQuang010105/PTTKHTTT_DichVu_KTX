using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class ChiNhanhDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteQuery("sp_ChiNhanh_DanhSach");
        }

        public int Luu(ChiNhanhDTO chiNhanh)
        {
            SqlParameter maChiNhanh = new SqlParameter("@MaChiNhanh", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = chiNhanh.MaChiNhanh == 0 ? DBNull.Value : chiNhanh.MaChiNhanh
            };

            SqlParameter[] parameters =
            {
                maChiNhanh,
                new SqlParameter("@TenChiNhanh", chiNhanh.TenChiNhanh),
                new SqlParameter("@KhuVuc", chiNhanh.KhuVuc),
                new SqlParameter("@DiaChi", chiNhanh.DiaChi),
                new SqlParameter("@SoDienThoai", string.IsNullOrWhiteSpace(chiNhanh.SoDienThoai) ? DBNull.Value : chiNhanh.SoDienThoai.Trim()),
                new SqlParameter("@TrangThai", chiNhanh.TrangThai)
            };

            _db.ExecuteNonQuery("sp_ChiNhanh_Luu", parameters);
            return Convert.ToInt32(maChiNhanh.Value);
        }

        public void Xoa(int maChiNhanh)
        {
            _db.ExecuteNonQuery("sp_ChiNhanh_Xoa", new[] { new SqlParameter("@MaChiNhanh", maChiNhanh) });
        }
    }
}

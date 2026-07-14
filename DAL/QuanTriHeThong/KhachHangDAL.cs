using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class KhachHangDAL
    {
        private DBConnection db;

        public KhachHangDAL()
        {
            db = new DBConnection();
        }

        public KhachHangDTO GetKhachHangBySearch(string keyword)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", keyword)
            };

            DataTable dt = db.ExecuteQuery("sp_GetKhachHangBySearch", parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHangDTO
                {
                    MaKH = row["maKH"].ToString(),
                    MaNhom = row["MaNhom"] != DBNull.Value ? row["MaNhom"].ToString() : null,
                    MaCuTru = row["maCuTru"] != DBNull.Value ? row["maCuTru"].ToString() : null,
                    HoTen = row["hoTen"].ToString(),
                    CCCD = row["CCCD"].ToString(),
                    SDT = row["SDT"] != DBNull.Value ? row["SDT"].ToString() : null,
                    Email = row["email"] != DBNull.Value ? row["email"].ToString() : null,
                    GioiTinh = row["gioiTinh"] != DBNull.Value ? row["gioiTinh"].ToString() : null,
                    QuocTich = row["QuocTich"] != DBNull.Value ? row["QuocTich"].ToString() : null,
                    KhaNangTC = row["KhaNangTC"] != DBNull.Value ? row["KhaNangTC"].ToString() : null
                };
            }
            return null;
        }

        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maKH", kh.MaKH),
                new SqlParameter("@hoTen", kh.HoTen ?? (object)DBNull.Value),
                new SqlParameter("@CCCD", kh.CCCD ?? (object)DBNull.Value),
                new SqlParameter("@SDT", kh.SDT ?? (object)DBNull.Value),
                new SqlParameter("@email", kh.Email ?? (object)DBNull.Value),
                new SqlParameter("@gioiTinh", kh.GioiTinh ?? (object)DBNull.Value),
                new SqlParameter("@QuocTich", kh.QuocTich ?? (object)DBNull.Value),
                new SqlParameter("@KhaNangTC", kh.KhaNangTC ?? (object)DBNull.Value)
            };

            int rowsAffected = db.ExecuteNonQuery("sp_UpdateKhachHang", parameters);
            return rowsAffected > 0;
        }
    }
}

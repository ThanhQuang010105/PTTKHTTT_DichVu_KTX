using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.DangKyThue
{
    public class LichXemPhongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteQuery("sp_LichXemPhong_DanhSach");
        }

        public int TaoLich(LichXemPhongDTO lich)
        {
            SqlParameter maLich = new SqlParameter("@MaLich", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] parameters =
            {
                maLich,
                new SqlParameter("@MaDangKy", lich.MaDangKy),
                new SqlParameter("@LoaiDoiTuong", lich.LoaiDoiTuong),
                new SqlParameter("@MaPhong", lich.MaPhong.HasValue ? lich.MaPhong.Value : DBNull.Value),
                new SqlParameter("@MaGiuong", lich.MaGiuong.HasValue ? lich.MaGiuong.Value : DBNull.Value),
                new SqlParameter("@NgayGioHen", lich.NgayGioHen),
                new SqlParameter("@GhiChu", string.IsNullOrWhiteSpace(lich.GhiChu) ? DBNull.Value : lich.GhiChu.Trim())
            };

            _db.ExecuteNonQuery("sp_LichXemPhong_Tao", parameters);
            return Convert.ToInt32(maLich.Value);
        }
    }
}

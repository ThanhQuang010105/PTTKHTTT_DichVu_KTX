using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class GiuongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSachTheoPhong(int maPhong)
        {
            return _db.ExecuteQuery("sp_Giuong_DanhSachTheoPhong", new[] { new SqlParameter("@MaPhong", maPhong) });
        }

        public DataTable TraCuuGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@KhuVuc", tieuChi.KhuVucMongMuon),
                new SqlParameter("@GioiTinh", tieuChi.GioiTinh),
                new SqlParameter("@LoaiPhong", tieuChi.LoaiPhong),
                new SqlParameter("@SoNguoiDuKien", tieuChi.SoNguoiDuKien),
                new SqlParameter("@GiaToiDa", tieuChi.GiaToiDa),
                new SqlParameter("@NgayVaoDuKien", tieuChi.NgayVaoDuKien.Date),
                new SqlParameter("@TieuChiUuTien", string.IsNullOrWhiteSpace(tieuChi.TieuChiUuTien)
                    ? DBNull.Value
                    : tieuChi.TieuChiUuTien.Trim())
            };

            return _db.ExecuteQuery("sp_TraCuuGiuongKhaDung", parameters);
        }

        public int Luu(GiuongDTO giuong)
        {
            SqlParameter maGiuong = new SqlParameter("@MaGiuong", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = giuong.MaGiuong == 0 ? DBNull.Value : giuong.MaGiuong
            };

            SqlParameter[] parameters =
            {
                maGiuong,
                new SqlParameter("@MaPhong", giuong.MaPhong),
                new SqlParameter("@TenGiuong", giuong.TenGiuong),
                new SqlParameter("@GiaThue", giuong.GiaThue),
                new SqlParameter("@TrangThaiGiuong", giuong.TrangThaiGiuong)
            };

            _db.ExecuteNonQuery("sp_Giuong_Luu", parameters);
            return Convert.ToInt32(maGiuong.Value);
        }

        public void Xoa(int maGiuong)
        {
            _db.ExecuteNonQuery("sp_Giuong_Xoa", new[] { new SqlParameter("@MaGiuong", maGiuong) });
        }
    }
}

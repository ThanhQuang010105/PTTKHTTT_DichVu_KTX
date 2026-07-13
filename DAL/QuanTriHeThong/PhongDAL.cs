using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class PhongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteQuery("sp_Phong_DanhSach");
        }

        public DataTable TraCuuPhongKhaDung(PhieuDangKyThueDTO tieuChi)
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

            return _db.ExecuteQuery("sp_TraCuuPhongKhaDung", parameters);
        }

        public int Luu(PhongDTO phong)
        {
            SqlParameter maPhong = new SqlParameter("@MaPhong", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = phong.MaPhong == 0 ? DBNull.Value : phong.MaPhong
            };

            SqlParameter[] parameters =
            {
                maPhong,
                new SqlParameter("@MaChiNhanh", phong.MaChiNhanh),
                new SqlParameter("@TenPhong", phong.TenPhong),
                new SqlParameter("@KhuVuc", phong.KhuVuc),
                new SqlParameter("@GioiTinhQuyDinh", phong.GioiTinhQuyDinh),
                new SqlParameter("@LoaiPhong", phong.LoaiPhong),
                new SqlParameter("@SucChua", phong.SucChua),
                new SqlParameter("@GiaThue", phong.GiaThue),
                new SqlParameter("@TrangThaiPhong", phong.TrangThaiPhong)
            };

            _db.ExecuteNonQuery("sp_Phong_Luu", parameters);
            return Convert.ToInt32(maPhong.Value);
        }

        public void Xoa(int maPhong)
        {
            _db.ExecuteNonQuery("sp_Phong_Xoa", new[] { new SqlParameter("@MaPhong", maPhong) });
        }
    }
}

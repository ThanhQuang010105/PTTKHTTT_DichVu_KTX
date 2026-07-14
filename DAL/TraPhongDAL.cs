using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL
{
    /// <summary>
    /// DAL cho chức năng Trả phòng.
    /// Chỉ gọi Stored Procedure — tuyệt đối không viết SQL ở đây.
    /// </summary>
    public class TraPhongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        /// <summary>
        /// Tra cứu hợp đồng đang hiệu lực để tạo yêu cầu trả phòng.
        /// </summary>
        public DataTable TraCuuHopDong(string sdt, string cccd, string maHopDong)
        {
            SqlParameter[] prms =
            {
                new SqlParameter("@SoDienThoai", (object)sdt       ?? DBNull.Value),
                new SqlParameter("@CCCD",        (object)cccd      ?? DBNull.Value),
                new SqlParameter("@MaHopDong",   (object)maHopDong ?? DBNull.Value)
            };
            return _db.ExecuteQuery("sp_TraCuuHopDongTraPhong", prms);
        }

        /// <summary>
        /// Gọi SP tạo yêu cầu trả phòng. Trả về chuỗi kết quả từ OUTPUT param.
        /// </summary>
        public string TaoYeuCauTraPhong(YeuCauTraPhongDTO dto)
        {
            SqlParameter pKetQua = new SqlParameter("@KetQua", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] prms =
            {
                new SqlParameter("@MaHopDong",      dto.MaHopDong),
                new SqlParameter("@NgayDuKienTra",  dto.NgayDuKienTra.Date),
                new SqlParameter("@LyDoTraPhong",   dto.LyDoTraPhong),
                new SqlParameter("@GhiChu",         (object)dto.GhiChu ?? DBNull.Value),
                new SqlParameter("@MaNVSale",       dto.MaNhanVienSale),
                pKetQua
            };

            _db.ExecuteNonQuery("sp_TaoYeuCauTraPhong", prms);
            return pKetQua.Value?.ToString();
        }

        /// <summary>
        /// Lấy danh sách các hợp đồng đã tạo yêu cầu trả phòng (Chờ kiểm tra).
        /// </summary>
        public DataTable GetDanhSachYeuCauTraPhong()
        {
            return _db.ExecuteQuery("sp_GetDanhSachYeuCauTraPhong", null);
        }
    }
}

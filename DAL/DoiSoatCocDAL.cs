using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL
{
    /// <summary>
    /// DAL cho chức năng Lập bảng đối soát cọc.
    /// Chỉ gọi Stored Procedure — tuyệt đối không viết SQL ở đây.
    /// </summary>
    public class DoiSoatCocDAL
    {
        private readonly DBConnection _db = new DBConnection();

        /// <summary>
        /// Lấy danh sách hợp đồng đang chờ lập đối soát.
        /// </summary>
        public DataTable GetHopDongChoDoiSoat(string sdt, string cccd)
        {
            SqlParameter[] prms =
            {
                new SqlParameter("@SDT", (object)sdt ?? DBNull.Value),
                new SqlParameter("@CCCD", (object)cccd ?? DBNull.Value)
            };
            return _db.ExecuteQuery("sp_GetHopDongChoDoiSoat", prms);
        }

        /// <summary>
        /// Lấy toàn bộ phí phát sinh của một hợp đồng.
        /// </summary>
        public DataTable GetPhiPhatSinh(string maHopDong)
        {
            SqlParameter[] prms =
            {
                new SqlParameter("@MaHopDong", maHopDong)
            };
            return _db.ExecuteQuery("sp_GetPhiPhatSinhTheoHopDong", prms);
        }

        /// <summary>
        /// Lấy tổng tiền thuê còn nợ của một hợp đồng.
        /// </summary>
        public DataTable GetTongNoTienThue(string maHopDong)
        {
            SqlParameter[] prms =
            {
                new SqlParameter("@MaHopDong", maHopDong)
            };
            return _db.ExecuteQuery("sp_GetTongNoTienThue", prms);
        }

        /// <summary>
        /// Lưu bảng đối soát cọc đã tính toán vào CSDL.
        /// </summary>
        public string LuuBangDoiSoat(LuuDoiSoatDTO dto)
        {
            SqlParameter pKetQua = new SqlParameter("@KetQua", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] prms =
            {
                new SqlParameter("@MaDS",                 dto.MaDS),
                new SqlParameter("@MaHopDong",            dto.MaHopDong),
                new SqlParameter("@NgayTraPhong",         dto.NgayTraPhong.Date),
                new SqlParameter("@DaKyHopDong",          dto.DaKyHopDong),
                new SqlParameter("@HetHanHopDong",        dto.HetHanHopDong),
                new SqlParameter("@TyLeHoanCoc",          dto.TyLeHoanCoc),
                new SqlParameter("@SoTienCocDuocXetHoan", dto.SoTienCocDuocXetHoan),
                new SqlParameter("@TongTienKhauTru",      dto.TongTienKhauTru),
                new SqlParameter("@SoTienHoanLai",        dto.SoTienHoanLai),
                new SqlParameter("@SoTienPhaiDongThem",   dto.SoTienPhaiDongThem),
                new SqlParameter("@GhiChu",               (object)dto.GhiChu ?? DBNull.Value),
                pKetQua
            };

            _db.ExecuteNonQuery("sp_LuuBangDoiSoatCoc", prms);
            return pKetQua.Value?.ToString();
        }
    }
}

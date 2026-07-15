using System;
using System.Data.SqlClient;

namespace HomeStayDorm.DAL.DatCoc
{
    public class HoSoCuTruDB
    {
        private DBConnection dbContext = new DBConnection();

        public int CountNguoiOByDatCoc(string maDatCoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maDatCoc", maDatCoc)
            };

            object result = dbContext.ExecuteScalar("sp_CountNguoiOByDatCoc", parameters);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        public bool InsertHoSoCuTru(DTO.HoSoCuTruDTO thongTinKhachHang)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maDatCoc", thongTinKhachHang.maDatCoc),
                new SqlParameter("@hoTen", thongTinKhachHang.hoTen),
                new SqlParameter("@cccd", thongTinKhachHang.cccd),
                new SqlParameter("@sdt", thongTinKhachHang.sdt),
                new SqlParameter("@gioiTinh", thongTinKhachHang.gioiTinh),
                new SqlParameter("@queQuan", thongTinKhachHang.queQuan)
            };

            int affectedRows = dbContext.ExecuteNonQuery("sp_InsertHoSoCuTru", parameters);
            return affectedRows > 0;
        }

        public System.Collections.Generic.List<DTO.HoSoCuTruDTO> GetHoSoCuTruByDatCoc(string maDatCoc)
        {
            System.Collections.Generic.List<DTO.HoSoCuTruDTO> list = new System.Collections.Generic.List<DTO.HoSoCuTruDTO>();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maDatCoc", maDatCoc)
            };

            System.Data.DataTable dt = dbContext.ExecuteQuery("sp_GetHoSoCuTruByDatCoc", parameters);
            if (dt != null)
            {
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    list.Add(new DTO.HoSoCuTruDTO
                    {
                        STT = Convert.ToInt32(row["STT"]),
                        maKH = row["maKH"].ToString(),
                        hoTen = row["hoTen"].ToString(),
                        cccd = row["CCCD"].ToString(),
                        sdt = row["SDT"].ToString(),
                        gioiTinh = row["gioiTinh"].ToString(),
                        queQuan = row["queQuan"] != DBNull.Value ? row["queQuan"].ToString() : "",
                        maDatCoc = maDatCoc,
                        isNew = false
                    });
                }
            }
            return list;
        }

        public bool DeleteHoSoCuTru(string maKH)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maKH", maKH)
            };
            return dbContext.ExecuteNonQuery("sp_DeleteHoSoCuTru", parameters) > 0;
        }

        public bool UpdateTrangThaiPhieuDatCoc(string maDatCoc, string trangThai)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maDatCoc", maDatCoc),
                new SqlParameter("@trangThai", trangThai)
            };
            return dbContext.ExecuteNonQuery("sp_UpdateTrangThaiPhieuDatCoc", parameters) > 0;
        }
    }
}

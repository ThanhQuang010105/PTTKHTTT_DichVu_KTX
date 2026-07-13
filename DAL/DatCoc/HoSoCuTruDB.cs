using System;
using System.Data.SqlClient;

namespace HomeStayDorm.DAL.DatCoc
{
    public class HoSoCuTruDB
    {
        private DBConnection dbContext = new DBConnection();

        public int CountNguoiOByDatCoc(string maDatCoc)
        {
            // Tách "PDC001" -> 1
            if (maDatCoc.StartsWith("PDC"))
            {
                if (int.TryParse(maDatCoc.Substring(3), out int id))
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@maDatCoc", id)
                    };

                    object result = dbContext.ExecuteScalar("sp_CountNguoiOByDatCoc", parameters);
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }

        public bool InsertHoSoCuTru(DTO.HoSoCuTruDTO thongTinKhachHang)
        {
            if (thongTinKhachHang.maDatCoc.StartsWith("PDC"))
            {
                if (int.TryParse(thongTinKhachHang.maDatCoc.Substring(3), out int id))
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@maDatCoc", id),
                        new SqlParameter("@hoTen", thongTinKhachHang.hoTen),
                        new SqlParameter("@cccd", thongTinKhachHang.cccd),
                        new SqlParameter("@sdt", thongTinKhachHang.sdt),
                        new SqlParameter("@gioiTinh", thongTinKhachHang.gioiTinh),
                        new SqlParameter("@queQuan", thongTinKhachHang.queQuan)
                    };

                    int affectedRows = dbContext.ExecuteNonQuery("sp_InsertHoSoCuTru", parameters);
                    return affectedRows > 0;
                }
            }
            return false;
        }

        public System.Collections.Generic.List<DTO.HoSoCuTruDTO> GetHoSoCuTruByDatCoc(string maDatCoc)
        {
            System.Collections.Generic.List<DTO.HoSoCuTruDTO> list = new System.Collections.Generic.List<DTO.HoSoCuTruDTO>();
            if (maDatCoc.StartsWith("PDC"))
            {
                if (int.TryParse(maDatCoc.Substring(3), out int id))
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@maDatCoc", id)
                    };

                    System.Data.DataTable dt = dbContext.ExecuteQuery("sp_GetHoSoCuTruByDatCoc", parameters);
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        list.Add(new DTO.HoSoCuTruDTO
                        {
                            STT = Convert.ToInt32(row["STT"]),
                            hoTen = row["hoTen"].ToString(),
                            cccd = row["CCCD"].ToString(),
                            sdt = row["SDT"].ToString(),
                            gioiTinh = row["gioiTinh"].ToString(),
                            queQuan = row["queQuan"].ToString(),
                            maDatCoc = maDatCoc,
                            isNew = false
                        });
                    }
                }
            }
            return list;
        }

        public bool DeleteHoSoCuTru(int maKH)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maKH", maKH)
            };
            return dbContext.ExecuteNonQuery("sp_DeleteHoSoCuTru", parameters) > 0;
        }

        public bool UpdateTrangThaiPhieuDatCoc(string maDatCoc, string trangThai)
        {
            if (maDatCoc.StartsWith("PDC"))
            {
                if (int.TryParse(maDatCoc.Substring(3), out int id))
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@maDatCoc", id),
                        new SqlParameter("@trangThai", trangThai)
                    };
                    return dbContext.ExecuteNonQuery("sp_UpdateTrangThaiPhieuDatCoc", parameters) > 0;
                }
            }
            return false;
        }
    }
}

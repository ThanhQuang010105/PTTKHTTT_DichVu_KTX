using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.DatCoc
{
    public class PhieuDatCocDB
    {
        private DBConnection dbContext = new DBConnection();

        public List<PhieuDatCocDTO> GetPhieuDatCocByFilter(string maPhieu, string sdt, string cccd, string trangThai)
        {
            List<PhieuDatCocDTO> list = new List<PhieuDatCocDTO>();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maPhieu", string.IsNullOrEmpty(maPhieu) ? (object)DBNull.Value : maPhieu),
                new SqlParameter("@sdt", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt),
                new SqlParameter("@cccd", string.IsNullOrEmpty(cccd) ? (object)DBNull.Value : cccd),
                new SqlParameter("@trangThai", string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả" ? (object)DBNull.Value : trangThai)
            };

            DataTable dt = dbContext.ExecuteQuery("sp_GetPhieuDatCocByFilter", parameters);

            foreach (DataRow row in dt.Rows)
            {
                PhieuDatCocDTO phieu = new PhieuDatCocDTO
                {
                    STT = Convert.ToInt32(row["STT"]),
                    maPhieuCoc = row["MaDatCoc"].ToString(),
                    KhachDaiDien = row["KhachDaiDien"].ToString(),
                    soDienThoai = row["SoDienThoai"].ToString(),
                    PhongGiuongCoc = row["PhongGiuongCoc"].ToString(),
                    SoGiuongDat = row["SoGiuongDat"] != DBNull.Value ? Convert.ToInt32(row["SoGiuongDat"]) : 0,
                    soTienCoc = Convert.ToDecimal(row["TienCoc"]),
                    trangThai = row["TrangThai"].ToString(),
                    MaPhieuGoc = Convert.ToInt32(row["MaPhieuGoc"]),
                    GioiTinhPhong = row["GioiTinhPhong"] != DBNull.Value ? row["GioiTinhPhong"].ToString() : ""
                };
                list.Add(phieu);
            }

            return list;
        }

        public string GetStatusByMaPhieu(string maPhieu)
        {
            // Tách mã phiếu "PDC001" thành ID số "1"
            if (maPhieu.StartsWith("PDC"))
            {
                int id;
                if (int.TryParse(maPhieu.Substring(3), out id))
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@maPhieu", id)
                    };
                    
                    object result = dbContext.ExecuteScalar("sp_GetStatusByMaPhieu", parameters);
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            return null;
        }
    }
}

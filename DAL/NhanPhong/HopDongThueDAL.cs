using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Đã trả về đúng thư viện theo yêu cầu của team bạn
using System.Configuration;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL
{
    public class HopDongThueDAL
    {
        // Nhớ đảm bảo tên connection string "HomeStayDormDB" khớp với App.config của bạn
        private string connectionString = ConfigurationManager.ConnectionStrings["HomeStayDormDB"].ConnectionString;

        public List<HoSoCuTru_DTO> GetHoSoDaDuyet()
        {
            List<HoSoCuTru_DTO> list = new List<HoSoCuTru_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetHoSoCuTru_DaDuyet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new HoSoCuTru_DTO { 
                            MaHoSo = dr["MaHoSo"].ToString(), 
                            TenKhachHang = dr["TenKhachHang"].ToString(), 
                            DisplayText = dr["DisplayText"].ToString() 
                        });
                    }
                }
            }
            return list;
        }

        public ChiTietHoSo_DTO GetChiTietHoSo(string maHoSo)
        {
            ChiTietHoSo_DTO dto = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetChiTietHoSoLapHopDong", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoSo", maHoSo);
                conn.Open();
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dto = new ChiTietHoSo_DTO
                        {
                            MaHoSo = dr["MaHoSo"].ToString(),
                            MaKH = dr["MaKH"].ToString(),
                            TenKhachHang = dr["TenKhachHang"].ToString(),
                            CCCD = dr["CCCD"].ToString(),
                            
                            // Check DBNull an toàn
                            MaPhieuDatCoc = dr["MaPhieuDatCoc"] != DBNull.Value ? dr["MaPhieuDatCoc"].ToString() : "Chưa cọc",
                            TienCocTuDong = dr["TienCocTuDong"] != DBNull.Value ? Convert.ToDecimal(dr["TienCocTuDong"]) : 0,
                            
                            MaPhong = dr["MaPhong"] != DBNull.Value ? dr["MaPhong"].ToString() : "",
                            TenPhong = dr["TenPhong"] != DBNull.Value ? dr["TenPhong"].ToString() : "",
                            DonGiaThue = dr["DonGiaThue"] != DBNull.Value ? Convert.ToDecimal(dr["DonGiaThue"]) : 0
                        };
                    }
                }
            }
            return dto;
        }

        public List<DichVu_DTO> GetDanhSachDichVu()
        {
            List<DichVu_DTO> list = new List<DichVu_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDanhSachDichVu", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new DichVu_DTO { 
                            MaDV = dr["MaDV"].ToString(), 
                            TenDV = dr["TenDV"].ToString(), 
                            DonGia = Convert.ToDecimal(dr["DonGia"]), 
                            DisplayText = dr["DisplayText"].ToString() 
                        });
                    }
                }
            }
            return list;
        }

        public bool InsertHopDong(HopDongThue_DTO hd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_HopDongThue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@MaHopDong", hd.MaHopDong);
                cmd.Parameters.AddWithValue("@MaKH", hd.MaKH);
                cmd.Parameters.AddWithValue("@MaPhong", hd.MaPhong);
                cmd.Parameters.AddWithValue("@MaPhieuCoc", hd.MaPhieuCoc); 
                cmd.Parameters.AddWithValue("@NgayBatDau", hd.NgayBatDau);
                cmd.Parameters.AddWithValue("@ThoiHanThang", hd.ThoiHanThang); 
                cmd.Parameters.AddWithValue("@KyThanhToan", hd.KyThanhToan);
                cmd.Parameters.AddWithValue("@TienCoc", hd.TienCoc); 
                
                conn.Open();
                
                // --- SỬA TẠI ĐÂY ---
                cmd.ExecuteNonQuery(); 
                return true; 
            }
        }

        public bool InsertHopDongDichVu(string maHopDong, string maDV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_HopDong_DichVu", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHopDong", maHopDong);
                cmd.Parameters.AddWithValue("@MaDV", maDV);
                
                conn.Open();
                
                // --- SỬA TẠI ĐÂY ---
                cmd.ExecuteNonQuery(); 
                return true; 
            }
        }
    }
}
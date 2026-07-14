using System;
using HomeStayDorm.DTO;
using HomeStayDorm.DAL.QuanTriHeThong;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class KhachHangBLL
    {
        private KhachHangDAL khachHangDAL;

        public KhachHangBLL()
        {
            khachHangDAL = new KhachHangDAL();
        }

        public KhachHangDTO GetKhachHangBySearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return null;
            return khachHangDAL.GetKhachHangBySearch(keyword);
        }

        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            if (kh == null || string.IsNullOrWhiteSpace(kh.MaKH)) return false;
            if (string.IsNullOrWhiteSpace(kh.HoTen)) return false; // Tên không được rỗng
            
            return khachHangDAL.UpdateKhachHang(kh);
        }
    }
}

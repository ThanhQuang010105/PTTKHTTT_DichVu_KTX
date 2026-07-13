using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class ChiNhanhBLL
    {
        private readonly ChiNhanhDAL _chiNhanhDAL = new ChiNhanhDAL();

        public DataTable LayDanhSach()
        {
            return _chiNhanhDAL.LayDanhSach();
        }

        public int Luu(ChiNhanhDTO chiNhanh)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(chiNhanh.TenChiNhanh)) loi.Add("Vui lòng nhập tên chi nhánh.");
            if (string.IsNullOrWhiteSpace(chiNhanh.KhuVuc)) loi.Add("Vui lòng nhập khu vực.");
            if (string.IsNullOrWhiteSpace(chiNhanh.DiaChi)) loi.Add("Vui lòng nhập địa chỉ.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            return _chiNhanhDAL.Luu(chiNhanh);
        }

        public void Xoa(int maChiNhanh)
        {
            if (maChiNhanh <= 0) throw new InvalidOperationException("Vui lòng chọn chi nhánh cần xóa.");
            _chiNhanhDAL.Xoa(maChiNhanh);
        }
    }
}

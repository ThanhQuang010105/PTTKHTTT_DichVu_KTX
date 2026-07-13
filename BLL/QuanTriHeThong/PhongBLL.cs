using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class PhongBLL
    {
        private readonly PhongDAL _phongDAL = new PhongDAL();

        public DataTable LayDanhSach()
        {
            return _phongDAL.LayDanhSach();
        }

        public DataTable TraCuuPhongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return _phongDAL.TraCuuPhongKhaDung(tieuChi);
        }

        public string Luu(PhongDTO phong)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(phong.MaChiNhanh)) loi.Add("Vui lòng chọn chi nhánh.");
            if (string.IsNullOrWhiteSpace(phong.TenPhong)) loi.Add("Vui lòng nhập tên phòng.");
            if (string.IsNullOrWhiteSpace(phong.KhuVuc)) loi.Add("Vui lòng nhập khu vực.");
            if (string.IsNullOrWhiteSpace(phong.GioiTinhQuyDinh)) loi.Add("Vui lòng chọn giới tính quy định.");
            if (string.IsNullOrWhiteSpace(phong.LoaiPhong)) loi.Add("Vui lòng chọn loại phòng.");
            if (phong.SucChua <= 0) loi.Add("Sức chứa phải lớn hơn 0.");
            if (phong.GiaThue <= 0) loi.Add("Giá thuê phải lớn hơn 0.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            return _phongDAL.Luu(phong);
        }

        public void Xoa(string maPhong)
        {
            if (string.IsNullOrWhiteSpace(maPhong)) throw new InvalidOperationException("Vui lòng chọn phòng cần xóa.");
            _phongDAL.Xoa(maPhong);
        }
    }
}

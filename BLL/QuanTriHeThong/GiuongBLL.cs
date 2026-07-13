using System.Data;
using System;
using System.Collections.Generic;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class GiuongBLL
    {
        private readonly GiuongDAL _giuongDAL = new GiuongDAL();

        public DataTable LayDanhSachTheoPhong(int maPhong)
        {
            if (maPhong <= 0) return new DataTable();
            return _giuongDAL.LayDanhSachTheoPhong(maPhong);
        }

        public DataTable TraCuuGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return _giuongDAL.TraCuuGiuongKhaDung(tieuChi);
        }

        public int Luu(GiuongDTO giuong)
        {
            List<string> loi = new List<string>();
            if (giuong.MaPhong <= 0) loi.Add("Vui lòng chọn phòng.");
            if (string.IsNullOrWhiteSpace(giuong.TenGiuong)) loi.Add("Vui lòng nhập tên giường.");
            if (giuong.GiaThue <= 0) loi.Add("Giá thuê giường phải lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(giuong.TrangThaiGiuong)) loi.Add("Vui lòng chọn trạng thái giường.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            return _giuongDAL.Luu(giuong);
        }

        public void Xoa(int maGiuong)
        {
            if (maGiuong <= 0) throw new InvalidOperationException("Vui lòng chọn giường cần xóa.");
            _giuongDAL.Xoa(maGiuong);
        }
    }
}

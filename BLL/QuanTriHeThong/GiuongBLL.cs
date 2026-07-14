using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class GiuongBLL
    {
        private readonly GiuongDAL _giuongDAL = new GiuongDAL();

        public DataTable LayDanhSachTheoPhong(string maPhong)
        {
            if (string.IsNullOrWhiteSpace(maPhong)) return new DataTable();
            return _giuongDAL.LayDanhSachTheoPhong(maPhong);
        }

        public DataTable TraCuuGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return TraCuuGiuongPhuHop(tieuChi);
        }

        public DataTable TraCuuGiuongPhuHop(PhieuDangKyThueDTO tieuChi)
        {
            return _giuongDAL.DocDanhSachGiuongTrong(tieuChi);
        }

        public string Luu(GiuongDTO giuong)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(giuong.MaPhong)) loi.Add("Vui lòng chọn phòng.");
            if (string.IsNullOrWhiteSpace(giuong.TenGiuong)) loi.Add("Vui lòng nhập tên giường.");
            if (giuong.GiaThue <= 0) loi.Add("Giá thuê giường phải lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(giuong.TrangThaiGiuong)) loi.Add("Vui lòng chọn trạng thái giường.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            return _giuongDAL.Luu(giuong);
        }

        public void Xoa(string maGiuong)
        {
            if (string.IsNullOrWhiteSpace(maGiuong)) throw new InvalidOperationException("Vui lòng chọn giường cần xóa.");
            _giuongDAL.Xoa(maGiuong);
        }
    }
}

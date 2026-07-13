using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL.DangKyThue;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DangKyThue
{
    public class LichXemPhongBLL
    {
        private readonly LichXemPhongDAL _lichXemPhongDAL = new LichXemPhongDAL();

        public DataTable LayDanhSach()
        {
            return _lichXemPhongDAL.LayDanhSach();
        }

        public string TaoLich(LichXemPhongDTO lich)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(lich.MaDangKy)) loi.Add("Vui lòng tra cứu phiếu đăng ký trước khi lập lịch.");
            if (string.IsNullOrWhiteSpace(lich.LoaiDoiTuong)) loi.Add("Vui lòng chọn phòng/giường cần hẹn xem.");
            if (string.IsNullOrWhiteSpace(lich.MaPhong)) loi.Add("Vui lòng chọn phòng/giường hợp lệ.");
            if (lich.NgayGioHen <= DateTime.Now) loi.Add("Thời gian hẹn phải lớn hơn thời điểm hiện tại.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            return _lichXemPhongDAL.TaoLich(lich);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DangKyThue
{
    public class TraCuuPhongGiuongBLL
    {
        private readonly PhongBLL _phongBLL = new PhongBLL();
        private readonly GiuongBLL _giuongBLL = new GiuongBLL();

        public TraCuuPhongGiuongResult KiemTraBoLoc(PhieuDangKyThueDTO thongTinDK)
        {
            List<string> loi = LayLoiBoLoc(thongTinDK);
            if (loi.Count > 0)
            {
                return TraCuuPhongGiuongResult.TaoThatBai(string.Join(Environment.NewLine, loi));
            }

            return TraCuuPhongGiuongResult.TaoThanhCong(new DataTable(), "Bộ lọc hợp lệ.");
        }

        public TraCuuPhongGiuongResult TraCuuPhongGiuongPhuHop(PhieuDangKyThueDTO thongTinDK)
        {
            TraCuuPhongGiuongResult ketQuaKiemTra = KiemTraBoLoc(thongTinDK);
            if (!ketQuaKiemTra.ThanhCong)
            {
                return ketQuaKiemTra;
            }

            DataTable ketQua = LaThueNguyenPhong(thongTinDK.HinhThucThue)
                ? TraCuuPhongPhuHop(thongTinDK)
                : TraCuuGiuongPhuHop(thongTinDK);

            string thongBao = ketQua.Rows.Count == 0
                ? "Không có phòng/giường phù hợp."
                : $"Tìm thấy {ketQua.Rows.Count} phòng/giường phù hợp.";

            return TraCuuPhongGiuongResult.TaoThanhCong(ketQua, thongBao);
        }

        public TraCuuPhongGiuongResult TraCuuPhongGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return TraCuuPhongGiuongPhuHop(tieuChi);
        }

        public DataTable TraCuuPhongPhuHop(PhieuDangKyThueDTO thongTinDK)
        {
            return _phongBLL.TraCuuPhongPhuHop(thongTinDK);
        }

        public DataTable TraCuuGiuongPhuHop(PhieuDangKyThueDTO thongTinDK)
        {
            return _giuongBLL.TraCuuGiuongPhuHop(thongTinDK);
        }

        private static List<string> LayLoiBoLoc(PhieuDangKyThueDTO tieuChi)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(tieuChi.GioiTinh)) loi.Add("Vui lòng chọn giới tính.");
            if (!LaHinhThucThueHopLe(tieuChi.HinhThucThue)) loi.Add("Vui lòng chọn hình thức thuê hợp lệ.");
            if (string.IsNullOrWhiteSpace(tieuChi.KhuVucMongMuon)) loi.Add("Vui lòng chọn khu vực.");
            if (string.IsNullOrWhiteSpace(tieuChi.LoaiPhong)) loi.Add("Vui lòng chọn loại phòng.");
            if (tieuChi.SoNguoiDuKien <= 0) loi.Add("Số người dự kiến ở phải lớn hơn 0.");
            if (tieuChi.GiaToiDa <= 0) loi.Add("Mức giá tối đa phải lớn hơn 0.");
            return loi;
        }

        private static bool LaHinhThucThueHopLe(string hinhThucThue)
        {
            return string.Equals(hinhThucThue, DangKyThueBLL.ThueNguyenPhong, StringComparison.OrdinalIgnoreCase)
                || string.Equals(hinhThucThue, DangKyThueBLL.ThueGiuongOGhep, StringComparison.OrdinalIgnoreCase);
        }

        private static bool LaThueNguyenPhong(string hinhThucThue)
        {
            return string.Equals(hinhThucThue, DangKyThueBLL.ThueNguyenPhong, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class TraCuuPhongGiuongResult
    {
        private TraCuuPhongGiuongResult(bool thanhCong, DataTable ketQua, string thongBao)
        {
            ThanhCong = thanhCong;
            KetQua = ketQua;
            ThongBao = thongBao;
        }

        public bool ThanhCong { get; }
        public DataTable KetQua { get; }
        public string ThongBao { get; }

        public static TraCuuPhongGiuongResult TaoThanhCong(DataTable ketQua, string thongBao)
        {
            return new TraCuuPhongGiuongResult(true, ketQua, thongBao);
        }

        public static TraCuuPhongGiuongResult TaoThatBai(string thongBao)
        {
            return new TraCuuPhongGiuongResult(false, new DataTable(), thongBao);
        }
    }
}

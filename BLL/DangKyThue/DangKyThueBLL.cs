using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DAL.DangKyThue;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DangKyThue
{
    public class DangKyThueBLL
    {
        public const string ThueNguyenPhong = "Thuê nguyên phòng";
        public const string ThueGiuongOGhep = "Thuê giường ở ghép";

        private readonly DangKyThueDAL _dangKyThueDAL = new DangKyThueDAL();
        private readonly PhongBLL _phongBLL = new PhongBLL();
        private readonly GiuongBLL _giuongBLL = new GiuongBLL();

        public DangKyTraCuuResult TaoDangKyVaTraCuu(PhieuDangKyThueDTO phieuDangKy)
        {
            List<string> loi = KiemTraThongTinHopLe(phieuDangKy);
            if (loi.Count > 0)
            {
                return DangKyTraCuuResult.TaoThatBai(string.Join(Environment.NewLine, loi));
            }

            phieuDangKy.MaDangKy = _dangKyThueDAL.TaoPhieuDangKy(phieuDangKy);

            DataTable ketQua = LaThueNguyenPhong(phieuDangKy.HinhThucThue)
                ? _phongBLL.TraCuuPhongKhaDung(phieuDangKy)
                : _giuongBLL.TraCuuGiuongKhaDung(phieuDangKy);

            string thongBao = ketQua.Rows.Count == 0
                ? "Không có phòng/giường phù hợp. Hãy điều chỉnh tiêu chí hoặc kết thúc xử lý đăng ký."
                : $"Đã tạo phiếu {phieuDangKy.MaDangKy} và tìm thấy {ketQua.Rows.Count} kết quả phù hợp.";

            return DangKyTraCuuResult.TaoThanhCong(phieuDangKy.MaDangKy, ketQua, thongBao);
        }

        public DataTable LayDanhSachDangKy()
        {
            return _dangKyThueDAL.LayDanhSachDangKy();
        }

        public DataTable LayChiTietDangKy(string maDangKy)
        {
            if (string.IsNullOrWhiteSpace(maDangKy)) return new DataTable();
            return _dangKyThueDAL.LayChiTietDangKy(maDangKy.Trim());
        }

        public DangKyTraCuuResult TraCuuPhongGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            List<string> loi = KiemTraTieuChiTraCuu(tieuChi);
            if (loi.Count > 0)
            {
                return DangKyTraCuuResult.TaoThatBai(string.Join(Environment.NewLine, loi));
            }

            DataTable ketQua = LaThueNguyenPhong(tieuChi.HinhThucThue)
                ? _phongBLL.TraCuuPhongKhaDung(tieuChi)
                : _giuongBLL.TraCuuGiuongKhaDung(tieuChi);

            string thongBao = ketQua.Rows.Count == 0
                ? "Không có phòng/giường phù hợp."
                : $"Tìm thấy {ketQua.Rows.Count} phòng/giường phù hợp.";

            return DangKyTraCuuResult.TaoThanhCong(tieuChi.MaDangKy, ketQua, thongBao);
        }

        public PhieuDangKyThueDTO TaoDtoTuDongChiTiet(DataRow row)
        {
            return new PhieuDangKyThueDTO
            {
                MaDangKy = Convert.ToString(row["MaDangKy"]) ?? string.Empty,
                HoTenKhachHang = Convert.ToString(row["HoTen"]) ?? string.Empty,
                SoDienThoai = Convert.ToString(row["SoDienThoai"]) ?? string.Empty,
                CCCD = Convert.ToString(row["CCCD"]),
                Email = Convert.ToString(row["Email"]),
                GioiTinh = Convert.ToString(row["GioiTinh"]) ?? string.Empty,
                HinhThucThue = Convert.ToString(row["HinhThucThue"]) ?? string.Empty,
                KhuVucMongMuon = Convert.ToString(row["KhuVucMongMuon"]) ?? string.Empty,
                LoaiPhong = Convert.ToString(row["LoaiPhong"]) ?? string.Empty,
                SoNguoiDuKien = Convert.ToInt32(row["SoNguoiDuKien"]),
                GiaToiDa = Convert.ToDecimal(row["GiaToiDa"]),
                NgayVaoDuKien = Convert.ToDateTime(row["NgayVaoDuKien"]),
                ThoiHanThueThang = Convert.ToInt32(row["ThoiHanThueThang"]),
                TieuChiUuTien = Convert.ToString(row["TieuChiUuTien"]),
                TrangThai = Convert.ToString(row["TrangThai"]) ?? string.Empty
            };
        }

        public List<string> KiemTraThongTinHopLe(PhieuDangKyThueDTO phieuDangKy)
        {
            List<string> loi = new List<string>();

            if (string.IsNullOrWhiteSpace(phieuDangKy.HoTenKhachHang))
            {
                loi.Add("Vui lòng nhập họ tên khách hàng.");
            }

            if (string.IsNullOrWhiteSpace(phieuDangKy.SoDienThoai))
            {
                loi.Add("Vui lòng nhập số điện thoại khách hàng.");
            }
            else if (phieuDangKy.SoDienThoai.Trim().Length < 9)
            {
                loi.Add("Số điện thoại phải có ít nhất 9 ký tự.");
            }

            if (string.IsNullOrWhiteSpace(phieuDangKy.GioiTinh))
            {
                loi.Add("Vui lòng chọn giới tính/khu vực giới tính.");
            }

            if (!LaHinhThucThueHopLe(phieuDangKy.HinhThucThue))
            {
                loi.Add("Vui lòng chọn hình thức thuê hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(phieuDangKy.KhuVucMongMuon))
            {
                loi.Add("Vui lòng chọn khu vực mong muốn.");
            }

            if (string.IsNullOrWhiteSpace(phieuDangKy.LoaiPhong))
            {
                loi.Add("Vui lòng chọn loại phòng.");
            }

            if (phieuDangKy.SoNguoiDuKien <= 0)
            {
                loi.Add("Số người dự kiến ở phải lớn hơn 0.");
            }

            if (phieuDangKy.GiaToiDa <= 0)
            {
                loi.Add("Mức giá tối đa phải lớn hơn 0.");
            }

            if (phieuDangKy.NgayVaoDuKien.Date < DateTime.Today)
            {
                loi.Add("Ngày vào dự kiến không được nhỏ hơn ngày hiện tại.");
            }

            if (phieuDangKy.ThoiHanThueThang <= 0)
            {
                loi.Add("Thời hạn thuê phải lớn hơn 0 tháng.");
            }

            return loi;
        }

        private List<string> KiemTraTieuChiTraCuu(PhieuDangKyThueDTO tieuChi)
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
            return new[] { ThueNguyenPhong, ThueGiuongOGhep }
                .Contains(hinhThucThue, StringComparer.OrdinalIgnoreCase);
        }

        private static bool LaThueNguyenPhong(string hinhThucThue)
        {
            return string.Equals(hinhThucThue, ThueNguyenPhong, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class DangKyTraCuuResult
    {
        private DangKyTraCuuResult(bool thanhCong, string maDangKy, DataTable ketQua, string thongBao)
        {
            ThanhCong = thanhCong;
            MaDangKy = maDangKy;
            KetQua = ketQua;
            ThongBao = thongBao;
        }

        public bool ThanhCong { get; }
        public string MaDangKy { get; }
        public DataTable KetQua { get; }
        public string ThongBao { get; }

        public static DangKyTraCuuResult TaoThanhCong(string maDangKy, DataTable ketQua, string thongBao)
        {
            return new DangKyTraCuuResult(true, maDangKy, ketQua, thongBao);
        }

        public static DangKyTraCuuResult TaoThatBai(string thongBao)
        {
            return new DangKyTraCuuResult(false, string.Empty, new DataTable(), thongBao);
        }
    }
}

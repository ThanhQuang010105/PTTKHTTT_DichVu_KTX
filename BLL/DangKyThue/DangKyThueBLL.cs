using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HomeStayDorm.DAL.DangKyThue;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DangKyThue
{
    public class DangKyThueBLL
    {
        public const string ThueNguyenPhong = "Thuê nguyên phòng";
        public const string ThueGiuongOGhep = "Thuê giường";

        private readonly DangKyThueDAL _dangKyThueDAL = new DangKyThueDAL();

        public DangKyResult TaoDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            DangKyResult ketQuaKiemTra = KiemTraThongTinHopLe(phieuDangKy);
            return ketQuaKiemTra.ThanhCong
                ? TaoPhieuDangKy(phieuDangKy)
                : ketQuaKiemTra;
        }

        public DangKyResult KiemTraThongTinHopLe(PhieuDangKyThueDTO phieuDangKy)
        {
            List<string> loi = LayLoiThongTinDangKy(phieuDangKy);
            return loi.Count == 0
                ? DangKyResult.TaoThanhCong(string.Empty, "Thông tin đăng ký hợp lệ.")
                : DangKyResult.TaoThatBai(string.Join(Environment.NewLine, loi));
        }

        public DangKyResult TaoPhieuDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            try
            {
                string maDangKy = _dangKyThueDAL.ThemPhieuDangKy(phieuDangKy);
                phieuDangKy.MaDangKy = maDangKy;
                return DangKyResult.TaoThanhCong(
                    maDangKy,
                    $"Đã tạo phiếu đăng ký {maDangKy}.");
            }
            catch (Exception ex)
            {
                return DangKyResult.TaoThatBai($"Không thể lưu phiếu đăng ký. Chi tiết: {ex.Message}");
            }
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

        private List<string> LayLoiThongTinDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(phieuDangKy.HoTenKhachHang)) loi.Add("Vui lòng nhập họ tên khách hàng.");
            if (string.IsNullOrWhiteSpace(phieuDangKy.SoDienThoai)) loi.Add("Vui lòng nhập số điện thoại khách hàng.");
            else if (phieuDangKy.SoDienThoai.Trim().Length < 9) loi.Add("Số điện thoại phải có ít nhất 9 ký tự.");
            if (string.IsNullOrWhiteSpace(phieuDangKy.GioiTinh)) loi.Add("Vui lòng chọn giới tính/khu vực giới tính.");
            if (!LaHinhThucThueHopLe(phieuDangKy.HinhThucThue)) loi.Add("Vui lòng chọn hình thức thuê hợp lệ.");
            if (string.IsNullOrWhiteSpace(phieuDangKy.KhuVucMongMuon)) loi.Add("Vui lòng chọn khu vực mong muốn.");
            if (string.IsNullOrWhiteSpace(phieuDangKy.LoaiPhong)) loi.Add("Vui lòng chọn loại phòng.");
            if (phieuDangKy.SoNguoiDuKien <= 0) loi.Add("Số người dự kiến ở phải lớn hơn 0.");
            if (phieuDangKy.GiaToiDa <= 0) loi.Add("Mức giá tối đa phải lớn hơn 0.");
            if (phieuDangKy.NgayVaoDuKien.Date < DateTime.Today) loi.Add("Ngày vào dự kiến không được nhỏ hơn ngày hiện tại.");
            if (phieuDangKy.ThoiHanThueThang <= 0) loi.Add("Thời hạn thuê phải lớn hơn 0 tháng.");
            return loi;
        }

        private static bool LaHinhThucThueHopLe(string hinhThucThue)
        {
            return new[] { ThueNguyenPhong, ThueGiuongOGhep }
                .Contains(hinhThucThue, StringComparer.OrdinalIgnoreCase);
        }
    }

    public class DangKyResult
    {
        private DangKyResult(bool thanhCong, string maDangKy, string thongBao)
        {
            ThanhCong = thanhCong;
            MaDangKy = maDangKy;
            ThongBao = thongBao;
        }

        public bool ThanhCong { get; }
        public string MaDangKy { get; }
        public string ThongBao { get; }

        public static DangKyResult TaoThanhCong(string maDangKy, string thongBao)
        {
            return new DangKyResult(true, maDangKy, thongBao);
        }

        public static DangKyResult TaoThatBai(string thongBao)
        {
            return new DangKyResult(false, string.Empty, thongBao);
        }
    }
}

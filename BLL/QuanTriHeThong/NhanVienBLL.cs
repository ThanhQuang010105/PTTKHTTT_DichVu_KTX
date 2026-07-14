using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL _nhanVienDAL = new NhanVienDAL();

        public DataTable LayDanhSach()
        {
            return _nhanVienDAL.LayDanhSach();
        }

        public DataTable LayDanhMucVaiTro()
        {
            return _nhanVienDAL.LayDanhMucVaiTro();
        }

        public string Luu(NhanVienDTO nhanVien, string? matKhauMoi)
        {
            List<string> loi = new List<string>();
            if (string.IsNullOrWhiteSpace(nhanVien.HoTen)) loi.Add("Vui lòng nhập họ tên nhân viên.");
            if (string.IsNullOrWhiteSpace(nhanVien.Email)) loi.Add("Vui lòng nhập email.");
            if (string.IsNullOrWhiteSpace(nhanVien.VaiTro)) loi.Add("Vui lòng chọn vai trò.");
            if (string.IsNullOrWhiteSpace(nhanVien.MaChiNhanh)) loi.Add("Vui lòng chọn chi nhánh.");
            if (string.IsNullOrWhiteSpace(nhanVien.MaNhanVien) && string.IsNullOrWhiteSpace(matKhauMoi)) loi.Add("Vui lòng nhập mật khẩu cho nhân viên mới.");
            if (loi.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, loi));

            nhanVien.MatKhauHash = string.IsNullOrWhiteSpace(matKhauMoi) ? null : matKhauMoi;
            return _nhanVienDAL.Luu(nhanVien);
        }

        public void KhoaTaiKhoan(string maNhanVien)
        {
            if (string.IsNullOrWhiteSpace(maNhanVien)) throw new InvalidOperationException("Vui lòng chọn nhân viên cần khóa.");
            _nhanVienDAL.KhoaTaiKhoan(maNhanVien);
        }
    }
}

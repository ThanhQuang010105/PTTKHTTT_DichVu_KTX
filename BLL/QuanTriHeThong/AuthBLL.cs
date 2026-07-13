using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using HomeStayDorm.DAL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.QuanTriHeThong
{
    public class AuthBLL
    {
        private readonly NhanVienDAL _nhanVienDAL = new NhanVienDAL();

        public DangNhapResult DangNhap(string tenDangNhapHoacEmail, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhapHoacEmail) || string.IsNullOrWhiteSpace(matKhau))
            {
                return DangNhapResult.ThatBai("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
            }

            DataTable data = _nhanVienDAL.DocTheoTenDangNhapHoacEmail(tenDangNhapHoacEmail.Trim());
            if (data.Rows.Count == 0)
            {
                return DangNhapResult.ThatBai("Tài khoản hoặc mật khẩu không đúng.");
            }

            NhanVienDTO nhanVien = MapNhanVien(data.Rows[0]);
            if (!string.Equals(nhanVien.TrangThai, "Đang làm", StringComparison.OrdinalIgnoreCase))
            {
                return DangNhapResult.ThatBai("Tài khoản đã bị khóa hoặc nghỉ việc.");
            }

            string hash = HashMatKhau(matKhau);
            if (!string.Equals(nhanVien.MatKhauHash, hash, StringComparison.OrdinalIgnoreCase))
            {
                return DangNhapResult.ThatBai("Tài khoản hoặc mật khẩu không đúng.");
            }

            return DangNhapResult.TaoThanhCong(nhanVien);
        }

        public static string HashMatKhau(string matKhau)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(matKhau));
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        internal static NhanVienDTO MapNhanVien(DataRow row)
        {
            return new NhanVienDTO
            {
                MaNhanVien = Convert.ToInt32(row["MaNhanVien"]),
                HoTen = Convert.ToString(row["HoTen"]) ?? string.Empty,
                TenDangNhap = Convert.ToString(row["TenDangNhap"]) ?? string.Empty,
                Email = Convert.ToString(row["Email"]) ?? string.Empty,
                SoDienThoai = Convert.ToString(row["SoDienThoai"]),
                VaiTro = Convert.ToString(row["VaiTro"]) ?? string.Empty,
                MaChiNhanh = row.Table.Columns.Contains("MaChiNhanh") && row["MaChiNhanh"] != DBNull.Value ? Convert.ToInt32(row["MaChiNhanh"]) : null,
                TenChiNhanh = row.Table.Columns.Contains("TenChiNhanh") ? Convert.ToString(row["TenChiNhanh"]) : null,
                MatKhauHash = row.Table.Columns.Contains("MatKhauHash") ? Convert.ToString(row["MatKhauHash"]) : null,
                TrangThai = Convert.ToString(row["TrangThai"]) ?? string.Empty,
                NgayVaoLam = row.Table.Columns.Contains("NgayVaoLam") && row["NgayVaoLam"] != DBNull.Value ? Convert.ToDateTime(row["NgayVaoLam"]) : DateTime.Today
            };
        }
    }

    public class DangNhapResult
    {
        private DangNhapResult(bool thanhCong, NhanVienDTO? nhanVien, string thongBao)
        {
            ThanhCong = thanhCong;
            NhanVien = nhanVien;
            ThongBao = thongBao;
        }

        public bool ThanhCong { get; }
        public NhanVienDTO? NhanVien { get; }
        public string ThongBao { get; }

        public static DangNhapResult TaoThanhCong(NhanVienDTO nhanVien)
        {
            return new DangNhapResult(true, nhanVien, "Đăng nhập thành công.");
        }

        public static DangNhapResult ThatBai(string thongBao)
        {
            return new DangNhapResult(false, null, thongBao);
        }
    }
}

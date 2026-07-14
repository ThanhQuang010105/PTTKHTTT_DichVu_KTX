using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class NhanVienDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable DocTheoTenDangNhapHoacEmail(string tenDangNhapHoacEmail)
        {
            string login = tenDangNhapHoacEmail.Trim();
            string aliasRole = login.Equals("sale", StringComparison.OrdinalIgnoreCase) ? "Sale"
                : login.Equals("quanly", StringComparison.OrdinalIgnoreCase) ? "Quản lý"
                : login.Equals("ketoan", StringComparison.OrdinalIgnoreCase) ? "Kế toán"
                : string.Empty;

            return _db.ExecuteSqlQuery(@"
                SELECT TOP 1
                    nv.MaNV AS MaNhanVien,
                    nv.MaNV AS TenDangNhap,
                    nv.HoTen,
                    nv.Email,
                    nv.SDT AS SoDienThoai,
                    nv.VaiTro,
                    nv.MaCN AS MaChiNhanh,
                    cn.TenChiNhanh,
                    nv.MatKhau AS MatKhauHash,
                    CASE WHEN nv.MatKhau = 'LOCKED' THEN N'Nghỉ việc' ELSE N'Đang làm' END AS TrangThai,
                    CONVERT(date, GETDATE()) AS NgayVaoLam
                FROM dbo.Nhan_Vien nv
                INNER JOIN dbo.Chi_Nhanh cn ON cn.MaCN = nv.MaCN
                WHERE nv.MaNV = @Login
                   OR nv.Email = @Login
                   OR LEFT(nv.Email, CHARINDEX('@', nv.Email + '@') - 1) = @Login
                   OR (@AliasRole <> '' AND nv.VaiTro = @AliasRole)
                ORDER BY nv.MaNV",
                new[]
                {
                    new SqlParameter("@Login", login),
                    new SqlParameter("@AliasRole", aliasRole)
                });
        }

        public DataTable LayDanhSach()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    nv.MaNV AS MaNhanVien,
                    nv.MaNV AS TenDangNhap,
                    nv.HoTen,
                    nv.Email,
                    nv.SDT AS SoDienThoai,
                    nv.VaiTro,
                    nv.MaCN AS MaChiNhanh,
                    cn.TenChiNhanh,
                    CASE WHEN nv.MatKhau = 'LOCKED' THEN N'Nghỉ việc' ELSE N'Đang làm' END AS TrangThai,
                    CONVERT(date, GETDATE()) AS NgayVaoLam
                FROM dbo.Nhan_Vien nv
                INNER JOIN dbo.Chi_Nhanh cn ON cn.MaCN = nv.MaCN
                ORDER BY nv.MaNV DESC");
        }

        public DataTable LayDanhMucVaiTro()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT DISTINCT LTRIM(RTRIM(VaiTro)) AS VaiTro
                FROM dbo.Nhan_Vien
                WHERE VaiTro IS NOT NULL
                  AND LTRIM(RTRIM(VaiTro)) <> ''
                ORDER BY VaiTro");
        }

        public string Luu(NhanVienDTO nhanVien)
        {
            string maNhanVien = string.IsNullOrWhiteSpace(nhanVien.MaNhanVien)
                ? TaoMaMoi()
                : nhanVien.MaNhanVien.Trim();

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNhanVien),
                new SqlParameter("@MaCN", string.IsNullOrWhiteSpace(nhanVien.MaChiNhanh) ? DBNull.Value : nhanVien.MaChiNhanh),
                new SqlParameter("@HoTen", nhanVien.HoTen),
                new SqlParameter("@SDT", string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) ? DBNull.Value : nhanVien.SoDienThoai.Trim()),
                new SqlParameter("@Email", nhanVien.Email),
                new SqlParameter("@VaiTro", nhanVien.VaiTro),
                new SqlParameter("@MatKhau", string.IsNullOrWhiteSpace(nhanVien.MatKhauHash) ? DBNull.Value : nhanVien.MatKhauHash)
            };

            _db.ExecuteSqlNonQuery(@"
                IF EXISTS (SELECT 1 FROM dbo.Nhan_Vien WHERE MaNV = @MaNV)
                BEGIN
                    UPDATE dbo.Nhan_Vien
                    SET MaCN = @MaCN,
                        HoTen = @HoTen,
                        SDT = @SDT,
                        Email = @Email,
                        VaiTro = @VaiTro,
                        MatKhau = COALESCE(@MatKhau, MatKhau)
                    WHERE MaNV = @MaNV;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.Nhan_Vien (MaNV, MaCN, HoTen, SDT, Email, VaiTro, MatKhau)
                    VALUES (@MaNV, @MaCN, @HoTen, @SDT, @Email, @VaiTro, COALESCE(@MatKhau, '123456'));
                END", parameters);

            return maNhanVien;
        }

        public void KhoaTaiKhoan(string maNhanVien)
        {
            _db.ExecuteSqlNonQuery(
                "UPDATE dbo.Nhan_Vien SET MatKhau = 'LOCKED' WHERE MaNV = @MaNV",
                new[] { new SqlParameter("@MaNV", maNhanVien) });
        }

        private string TaoMaMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaNV, 3, 20))), 0) + 1
                FROM dbo.Nhan_Vien
                WHERE MaNV LIKE 'NV%'");
            return $"NV{Convert.ToInt32(value):000}";
        }
    }
}

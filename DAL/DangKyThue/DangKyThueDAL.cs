using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.DangKyThue
{
    public class DangKyThueDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSachDangKy()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    dk.MaDK AS MaDangKy,
                    kh.hoTen AS HoTen,
                    kh.SDT AS SoDienThoai,
                    kh.CCCD,
                    kh.email AS Email,
                    kh.gioiTinh AS GioiTinh,
                    dk.HinhThucThue,
                    dk.KhuVucMongMuon,
                    dk.LoaiPhongMongMuon AS LoaiPhong,
                    dk.SoNguoiDuKien,
                    TRY_CONVERT(DECIMAL(18,2), dk.MucGiaMongMuon) AS GiaToiDa,
                    dk.ThoiGianDuKienVaoO AS NgayVaoDuKien,
                    dk.ThoiHanThue AS ThoiHanThueThang,
                    dk.TieuChiUuTien,
                    dk.TrangThai,
                    dk.NgayDangKy AS NgayTao
                FROM dbo.Dang_ky_thue dk
                LEFT JOIN dbo.Khach_hang kh ON dk.GhiChu LIKE N'KH=' + kh.maKH + N';%'
                ORDER BY dk.MaDK DESC");
        }

        public DataTable LayChiTietDangKy(string maDangKy)
        {
            return _db.ExecuteSqlQuery(@"
                SELECT TOP 1
                    dk.MaDK AS MaDangKy,
                    kh.hoTen AS HoTen,
                    kh.SDT AS SoDienThoai,
                    kh.CCCD,
                    kh.email AS Email,
                    COALESCE(kh.gioiTinh, N'Không yêu cầu') AS GioiTinh,
                    dk.HinhThucThue,
                    dk.KhuVucMongMuon,
                    dk.LoaiPhongMongMuon AS LoaiPhong,
                    dk.SoNguoiDuKien,
                    COALESCE(TRY_CONVERT(DECIMAL(18,2), dk.MucGiaMongMuon), 0) AS GiaToiDa,
                    dk.ThoiGianDuKienVaoO AS NgayVaoDuKien,
                    COALESCE(TRY_CONVERT(INT, LEFT(dk.ThoiHanThue, PATINDEX('%[^0-9]%', dk.ThoiHanThue + 'x') - 1)), 1) AS ThoiHanThueThang,
                    dk.TieuChiUuTien,
                    dk.TrangThai
                FROM dbo.Dang_ky_thue dk
                LEFT JOIN dbo.Khach_hang kh ON dk.GhiChu LIKE N'KH=' + kh.maKH + N';%'
                WHERE dk.MaDK = @MaDK",
                new[] { new SqlParameter("@MaDK", maDangKy) });
        }

        public string TaoPhieuDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            return ThemPhieuDangKy(phieuDangKy);
        }

        public string ThemPhieuDangKy(PhieuDangKyThueDTO phieuDangKy)
        {
            string maKhachHang = TaoMaKhachHangMoi();
            string maDangKy = TaoMaDangKyMoi();

            SqlParameter[] khachHangParams =
            {
                new SqlParameter("@MaKH", maKhachHang),
                new SqlParameter("@HoTen", phieuDangKy.HoTenKhachHang),
                new SqlParameter("@CCCD", string.IsNullOrWhiteSpace(phieuDangKy.CCCD) ? TaoCccdTam(maKhachHang) : phieuDangKy.CCCD.Trim()),
                new SqlParameter("@SDT", phieuDangKy.SoDienThoai),
                new SqlParameter("@Email", string.IsNullOrWhiteSpace(phieuDangKy.Email) ? DBNull.Value : phieuDangKy.Email.Trim()),
                new SqlParameter("@GioiTinh", phieuDangKy.GioiTinh)
            };

            _db.ExecuteSqlNonQuery(@"
                INSERT INTO dbo.Khach_hang (maKH, hoTen, CCCD, SDT, email, gioiTinh)
                VALUES (@MaKH, @HoTen, @CCCD, @SDT, @Email, @GioiTinh)", khachHangParams);

            SqlParameter[] dangKyParams =
            {
                new SqlParameter("@MaDK", maDangKy),
                new SqlParameter("@SoNguoiDuKien", phieuDangKy.SoNguoiDuKien),
                new SqlParameter("@KhuVucMongMuon", phieuDangKy.KhuVucMongMuon),
                new SqlParameter("@HinhThucThue", phieuDangKy.HinhThucThue),
                new SqlParameter("@LoaiPhongMongMuon", phieuDangKy.LoaiPhong),
                new SqlParameter("@MucGiaMongMuon", phieuDangKy.GiaToiDa.ToString("0")),
                new SqlParameter("@ThoiGianDuKienVaoO", phieuDangKy.NgayVaoDuKien.Date),
                new SqlParameter("@ThoiHanThue", $"{phieuDangKy.ThoiHanThueThang} tháng"),
                new SqlParameter("@TieuChiUuTien", string.IsNullOrWhiteSpace(phieuDangKy.TieuChiUuTien) ? DBNull.Value : phieuDangKy.TieuChiUuTien.Trim()),
                new SqlParameter("@GhiChu", $"KH={maKhachHang};")
            };

            _db.ExecuteSqlNonQuery(@"
                INSERT INTO dbo.Dang_ky_thue
                    (MaDK, SoNguoiDuKien, KhuVucMongMuon, HinhThucThue, LoaiPhongMongMuon, MucGiaMongMuon,
                     ThoiGianDuKienVaoO, ThoiHanThue, TieuChiUuTien, TrangThai, NgayDangKy, GhiChu)
                VALUES
                    (@MaDK, @SoNguoiDuKien, @KhuVucMongMuon, @HinhThucThue, @LoaiPhongMongMuon, @MucGiaMongMuon,
                     @ThoiGianDuKienVaoO, @ThoiHanThue, @TieuChiUuTien, N'Mới tạo', CONVERT(date, GETDATE()), @GhiChu)", dangKyParams);

            return maDangKy;
        }

        public void CapNhatTrangThai(string maDangKy, string trangThai)
        {
            _db.ExecuteSqlNonQuery(
                "UPDATE dbo.Dang_ky_thue SET TrangThai = @TrangThai WHERE MaDK = @MaDK",
                new[]
                {
                    new SqlParameter("@MaDK", maDangKy),
                    new SqlParameter("@TrangThai", trangThai)
                });
        }

        private string TaoMaDangKyMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaDK, 3, 20))), 0) + 1
                FROM dbo.Dang_ky_thue
                WHERE MaDK LIKE 'DK%'");
            return $"DK{Convert.ToInt32(value):000}";
        }

        private string TaoMaKhachHangMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(maKH, 3, 20))), 0) + 1
                FROM dbo.Khach_hang
                WHERE maKH LIKE 'KH%'");
            return $"KH{Convert.ToInt32(value):000}";
        }

        private static string TaoCccdTam(string maKhachHang)
        {
            return $"TMP{maKhachHang}".PadRight(12, '0');
        }
    }
}

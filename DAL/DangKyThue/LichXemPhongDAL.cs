using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.DangKyThue
{
    public class LichXemPhongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    lx.MaLich,
                    lx.MaDK AS MaDangKy,
                    N'Giuong' AS LoaiDoiTuong,
                    p.MaPhong,
                    g.MaGiuong,
                    p.TenPhong,
                    g.TenGiuong,
                    lx.ThoiGianHen AS NgayGioHen,
                    lx.TrangThaiLich AS TrangThai,
                    lx.GhiChu
                FROM dbo.Lich_xem_phong lx
                INNER JOIN dbo.Giuong g ON g.MaGiuong = lx.MaGiuong
                INNER JOIN dbo.Phong p ON p.MaPhong = g.MaPhong
                ORDER BY lx.ThoiGianHen DESC");
        }

        public string TaoLich(LichXemPhongDTO lich)
        {
            string maLich = TaoMaMoi();
            string? maGiuong = lich.MaGiuong;

            if (string.IsNullOrWhiteSpace(maGiuong) && !string.IsNullOrWhiteSpace(lich.MaPhong))
            {
                maGiuong = Convert.ToString(_db.ExecuteSqlScalar(@"
                    SELECT TOP 1 MaGiuong
                    FROM dbo.Giuong
                    WHERE MaPhong = @MaPhong AND TrangThaiGiuong = N'Trống'
                    ORDER BY MaGiuong",
                    new[] { new SqlParameter("@MaPhong", lich.MaPhong) }));
            }

            if (string.IsNullOrWhiteSpace(maGiuong))
            {
                throw new InvalidOperationException("Không tìm được giường trống để lập lịch xem phòng.");
            }

            _db.ExecuteSqlNonQuery(@"
                INSERT INTO dbo.Lich_xem_phong (MaLich, MaDK, MaGiuong, ThoiGianHen, TrangThaiLich, GhiChu)
                VALUES (@MaLich, @MaDK, @MaGiuong, @ThoiGianHen, N'Đã hẹn', @GhiChu);

                UPDATE dbo.Dang_ky_thue
                SET TrangThai = N'Đã hẹn xem phòng'
                WHERE MaDK = @MaDK",
                new[]
                {
                    new SqlParameter("@MaLich", maLich),
                    new SqlParameter("@MaDK", lich.MaDangKy),
                    new SqlParameter("@MaGiuong", maGiuong),
                    new SqlParameter("@ThoiGianHen", lich.NgayGioHen),
                    new SqlParameter("@GhiChu", string.IsNullOrWhiteSpace(lich.GhiChu) ? DBNull.Value : lich.GhiChu.Trim())
                });

            return maLich;
        }

        private string TaoMaMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaLich, 2, 20))), 0) + 1
                FROM dbo.Lich_xem_phong
                WHERE MaLich LIKE 'L%'");
            return $"L{Convert.ToInt32(value):000}";
        }
    }
}

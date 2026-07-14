using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class GiuongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSachTheoPhong(string maPhong)
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    MaGiuong,
                    MaPhong,
                    TenGiuong,
                    GiaThueGiuong AS GiaThue,
                    TrangThaiGiuong
                FROM dbo.Giuong
                WHERE MaPhong = @MaPhong
                ORDER BY TenGiuong",
                new[] { new SqlParameter("@MaPhong", maPhong) });
        }

        public DataTable TraCuuGiuongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return DocDanhSachGiuongTrong(tieuChi);
        }

        public DataTable DocDanhSachGiuongTrong(PhieuDangKyThueDTO tieuChi)
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    N'Giuong' AS LoaiKetQua,
                    g.MaGiuong,
                    g.TenGiuong,
                    p.MaPhong,
                    p.TenPhong,
                    cn.TenChiNhanh,
                    cn.KhuVuc,
                    p.GioiTinhQuyDinh,
                    p.LoaiPhong,
                    g.GiaThueGiuong AS GiaThue,
                    g.TrangThaiGiuong
                FROM dbo.Giuong g
                INNER JOIN dbo.Phong p ON p.MaPhong = g.MaPhong
                INNER JOIN dbo.Chi_Nhanh cn ON cn.MaCN = p.MaCN
                WHERE g.TrangThaiGiuong = N'Trống'
                  AND p.TrangThaiPhong <> N'Ngừng sử dụng'
                  AND (cn.KhuVuc = @KhuVuc OR p.KhuVuc = @KhuVuc)
                  AND (@GioiTinh = N'Không yêu cầu' OR p.GioiTinhQuyDinh = @GioiTinh)
                  AND p.LoaiPhong = @LoaiPhong
                  AND g.GiaThueGiuong <= @GiaToiDa
                ORDER BY g.GiaThueGiuong, p.TenPhong, g.TenGiuong",
                new[]
                {
                    new SqlParameter("@KhuVuc", tieuChi.KhuVucMongMuon),
                    new SqlParameter("@GioiTinh", tieuChi.GioiTinh),
                    new SqlParameter("@LoaiPhong", tieuChi.LoaiPhong),
                    new SqlParameter("@GiaToiDa", tieuChi.GiaToiDa)
                });
        }

        public string Luu(GiuongDTO giuong)
        {
            string maGiuong = string.IsNullOrWhiteSpace(giuong.MaGiuong) ? TaoMaMoi(giuong.MaPhong) : giuong.MaGiuong.Trim();

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaGiuong", maGiuong),
                new SqlParameter("@MaPhong", giuong.MaPhong),
                new SqlParameter("@TenGiuong", giuong.TenGiuong),
                new SqlParameter("@GiaThueGiuong", giuong.GiaThue),
                new SqlParameter("@TrangThaiGiuong", giuong.TrangThaiGiuong)
            };

            _db.ExecuteSqlNonQuery(@"
                IF EXISTS (SELECT 1 FROM dbo.Giuong WHERE MaGiuong = @MaGiuong)
                BEGIN
                    UPDATE dbo.Giuong
                    SET MaPhong = @MaPhong,
                        TenGiuong = @TenGiuong,
                        GiaThueGiuong = @GiaThueGiuong,
                        TrangThaiGiuong = @TrangThaiGiuong
                    WHERE MaGiuong = @MaGiuong;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.Giuong (MaGiuong, MaPhong, TenGiuong, GiaThueGiuong, TrangThaiGiuong)
                    VALUES (@MaGiuong, @MaPhong, @TenGiuong, @GiaThueGiuong, @TrangThaiGiuong);
                END", parameters);

            return maGiuong;
        }

        public void Xoa(string maGiuong)
        {
            _db.ExecuteSqlNonQuery(
                "UPDATE dbo.Giuong SET TrangThaiGiuong = N'Ngừng sử dụng' WHERE MaGiuong = @MaGiuong",
                new[] { new SqlParameter("@MaGiuong", maGiuong) });
        }

        private string TaoMaMoi(string maPhong)
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaGiuong, LEN(@Prefix) + 1, 20))), 0) + 1
                FROM dbo.Giuong
                WHERE MaGiuong LIKE @Prefix + '%'",
                new[] { new SqlParameter("@Prefix", "G" + maPhong.TrimStart('P')) });
            return $"G{maPhong.TrimStart('P')}{Convert.ToInt32(value):00}";
        }
    }
}

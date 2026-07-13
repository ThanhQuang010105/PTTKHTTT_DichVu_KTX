using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class PhongDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    p.MaPhong,
                    p.MaCN AS MaChiNhanh,
                    cn.TenChiNhanh,
                    p.TenPhong,
                    p.KhuVuc,
                    p.GioiTinhQuyDinh,
                    p.LoaiPhong,
                    p.SucChuaToiDa AS SucChua,
                    p.GiaThuePhong AS GiaThue,
                    p.TrangThaiPhong
                FROM dbo.Phong p
                INNER JOIN dbo.Chi_Nhanh cn ON cn.MaCN = p.MaCN
                ORDER BY p.MaPhong DESC");
        }

        public DataTable TraCuuPhongKhaDung(PhieuDangKyThueDTO tieuChi)
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    N'Phong' AS LoaiKetQua,
                    p.MaPhong,
                    p.TenPhong,
                    cn.TenChiNhanh,
                    cn.KhuVuc,
                    p.GioiTinhQuyDinh,
                    p.LoaiPhong,
                    p.SucChuaToiDa AS SucChua,
                    p.GiaThuePhong AS GiaThue,
                    COUNT(CASE WHEN g.TrangThaiGiuong = N'Trống' THEN 1 END) AS SoGiuongTrong,
                    p.TrangThaiPhong
                FROM dbo.Phong p
                INNER JOIN dbo.Chi_Nhanh cn ON cn.MaCN = p.MaCN
                LEFT JOIN dbo.Giuong g ON g.MaPhong = p.MaPhong
                WHERE p.TrangThaiPhong <> N'Ngừng sử dụng'
                  AND (cn.KhuVuc = @KhuVuc OR p.KhuVuc = @KhuVuc)
                  AND (@GioiTinh = N'Không yêu cầu' OR p.GioiTinhQuyDinh = @GioiTinh)
                  AND p.LoaiPhong = @LoaiPhong
                  AND p.GiaThuePhong <= @GiaToiDa
                GROUP BY p.MaPhong, p.TenPhong, cn.TenChiNhanh, cn.KhuVuc, p.GioiTinhQuyDinh,
                         p.LoaiPhong, p.SucChuaToiDa, p.GiaThuePhong, p.TrangThaiPhong
                HAVING COUNT(CASE WHEN g.TrangThaiGiuong = N'Trống' THEN 1 END) >= @SoNguoiDuKien
                ORDER BY p.GiaThuePhong, p.TenPhong",
                new[]
                {
                    new SqlParameter("@KhuVuc", tieuChi.KhuVucMongMuon),
                    new SqlParameter("@GioiTinh", tieuChi.GioiTinh),
                    new SqlParameter("@LoaiPhong", tieuChi.LoaiPhong),
                    new SqlParameter("@SoNguoiDuKien", tieuChi.SoNguoiDuKien),
                    new SqlParameter("@GiaToiDa", tieuChi.GiaToiDa)
                });
        }

        public string Luu(PhongDTO phong)
        {
            string maPhong = string.IsNullOrWhiteSpace(phong.MaPhong) ? TaoMaMoi() : phong.MaPhong.Trim();

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaPhong", maPhong),
                new SqlParameter("@MaCN", phong.MaChiNhanh),
                new SqlParameter("@TenPhong", phong.TenPhong),
                new SqlParameter("@KhuVuc", phong.KhuVuc),
                new SqlParameter("@GioiTinhQuyDinh", phong.GioiTinhQuyDinh),
                new SqlParameter("@LoaiPhong", phong.LoaiPhong),
                new SqlParameter("@SucChuaToiDa", phong.SucChua),
                new SqlParameter("@GiaThuePhong", phong.GiaThue),
                new SqlParameter("@TrangThaiPhong", phong.TrangThaiPhong),
                new SqlParameter("@SoLuongGiuong", phong.SucChua)
            };

            _db.ExecuteSqlNonQuery(@"
                IF EXISTS (SELECT 1 FROM dbo.Phong WHERE MaPhong = @MaPhong)
                BEGIN
                    UPDATE dbo.Phong
                    SET MaCN = @MaCN,
                        TenPhong = @TenPhong,
                        KhuVuc = @KhuVuc,
                        GioiTinhQuyDinh = @GioiTinhQuyDinh,
                        LoaiPhong = @LoaiPhong,
                        SucChuaToiDa = @SucChuaToiDa,
                        GiaThuePhong = @GiaThuePhong,
                        TrangThaiPhong = @TrangThaiPhong,
                        SoLuongGiuong = @SoLuongGiuong
                    WHERE MaPhong = @MaPhong;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.Phong
                        (MaPhong, MaCN, TenPhong, KhuVuc, GioiTinhQuyDinh, LoaiPhong, SucChuaToiDa, GiaThuePhong, TrangThaiPhong, SoLuongGiuong)
                    VALUES
                        (@MaPhong, @MaCN, @TenPhong, @KhuVuc, @GioiTinhQuyDinh, @LoaiPhong, @SucChuaToiDa, @GiaThuePhong, @TrangThaiPhong, @SoLuongGiuong);
                END", parameters);

            return maPhong;
        }

        public void Xoa(string maPhong)
        {
            _db.ExecuteSqlNonQuery(
                "UPDATE dbo.Phong SET TrangThaiPhong = N'Ngừng sử dụng' WHERE MaPhong = @MaPhong",
                new[] { new SqlParameter("@MaPhong", maPhong) });
        }

        private string TaoMaMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaPhong, 2, 20))), 100) + 1
                FROM dbo.Phong
                WHERE MaPhong LIKE 'P%'");
            return $"P{Convert.ToInt32(value)}";
        }
    }
}

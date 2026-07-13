using System;
using System.Data;
using System.Data.SqlClient;
using HomeStayDorm.DTO;

namespace HomeStayDorm.DAL.QuanTriHeThong
{
    public class ChiNhanhDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayDanhSach()
        {
            return _db.ExecuteSqlQuery(@"
                SELECT
                    MaCN AS MaChiNhanh,
                    TenChiNhanh,
                    KhuVuc,
                    DiaChi,
                    SDT AS SoDienThoai,
                    TrangThai
                FROM dbo.Chi_Nhanh
                ORDER BY MaCN DESC");
        }

        public string Luu(ChiNhanhDTO chiNhanh)
        {
            string maChiNhanh = string.IsNullOrWhiteSpace(chiNhanh.MaChiNhanh)
                ? TaoMaMoi()
                : chiNhanh.MaChiNhanh.Trim();

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaCN", maChiNhanh),
                new SqlParameter("@TenChiNhanh", chiNhanh.TenChiNhanh),
                new SqlParameter("@KhuVuc", chiNhanh.KhuVuc),
                new SqlParameter("@DiaChi", chiNhanh.DiaChi),
                new SqlParameter("@SDT", string.IsNullOrWhiteSpace(chiNhanh.SoDienThoai) ? DBNull.Value : chiNhanh.SoDienThoai.Trim()),
                new SqlParameter("@TrangThai", chiNhanh.TrangThai)
            };

            _db.ExecuteSqlNonQuery(@"
                IF EXISTS (SELECT 1 FROM dbo.Chi_Nhanh WHERE MaCN = @MaCN)
                BEGIN
                    UPDATE dbo.Chi_Nhanh
                    SET TenChiNhanh = @TenChiNhanh,
                        KhuVuc = @KhuVuc,
                        DiaChi = @DiaChi,
                        SDT = @SDT,
                        TrangThai = @TrangThai
                    WHERE MaCN = @MaCN;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.Chi_Nhanh (MaCN, TenChiNhanh, KhuVuc, DiaChi, SDT, TrangThai, NgayThanhLap)
                    VALUES (@MaCN, @TenChiNhanh, @KhuVuc, @DiaChi, @SDT, @TrangThai, CONVERT(date, GETDATE()));
                END", parameters);

            return maChiNhanh;
        }

        public void Xoa(string maChiNhanh)
        {
            _db.ExecuteSqlNonQuery(
                "UPDATE dbo.Chi_Nhanh SET TrangThai = N'Ngừng hoạt động' WHERE MaCN = @MaCN",
                new[] { new SqlParameter("@MaCN", maChiNhanh) });
        }

        private string TaoMaMoi()
        {
            object? value = _db.ExecuteSqlScalar(@"
                SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaCN, 3, 20))), 0) + 1
                FROM dbo.Chi_Nhanh
                WHERE MaCN LIKE 'CN%'");
            return $"CN{Convert.ToInt32(value):00}";
        }
    }
}

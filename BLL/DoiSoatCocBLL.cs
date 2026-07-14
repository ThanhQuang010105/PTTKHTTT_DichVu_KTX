using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL
{
    /// <summary>
    /// BLL cho chức năng Lập bảng đối soát cọc.
    /// Đây là trái tim nghiệp vụ — nơi duy nhất tính tỷ lệ hoàn cọc.
    /// Tuyệt đối không viết SQL ở đây.
    /// </summary>
    public class DoiSoatCocBLL
    {
        private readonly DoiSoatCocDAL _dal = new DoiSoatCocDAL();

        // ═══════════════════════════════════════════════════════════
        // 1. TRA CỨU HỢP ĐỒNG CHỜ ĐỐI SOÁT
        // ═══════════════════════════════════════════════════════════

        public DataTable GetHopDongChoDoiSoat(string sdt, string cccd, out string errorMsg)
        {
            errorMsg = string.Empty;

            string sdtVal = string.IsNullOrWhiteSpace(sdt) ? null : sdt.Trim();
            string cccdVal = string.IsNullOrWhiteSpace(cccd) ? null : cccd.Trim();

            DataTable dt = _dal.GetHopDongChoDoiSoat(sdtVal, cccdVal);

            if (dt == null || dt.Rows.Count == 0)
            {
                errorMsg = "Không tìm thấy hợp đồng nào đang chờ lập đối soát cọc!";
                return null;
            }
            return dt;
        }

        /// <summary>Map DataRow → DTO để truyền xuống màn hình.</summary>
        public HopDongDoiSoatDTO MapRow(DataRow row)
        {
            return new HopDongDoiSoatDTO
            {
                MaHopDong     = row["MaHopDong"].ToString(),
                MaKhachHang   = row["maKH"].ToString(),
                TenKhachHang  = row["TenKhachHang"].ToString(),
                SoDienThoai   = row["SDT"].ToString(),
                CCCD          = row["CCCD"].ToString(),
                TenPhong      = row["TenPhong"].ToString(),
                NgayBatDau    = Convert.ToDateTime(row["NgayBatDau"]),
                NgayKetThuc   = Convert.ToDateTime(row["NgayKetThuc"]),
                GiaThue       = Convert.ToDecimal(row["GiaThue"]),
                TrangThai     = row["TrangThai"].ToString(),
                TienCocGoc    = Convert.ToDecimal(row["TienCocGoc"]),
                MaPhieuCoc    = row["MaPhieuCoc"].ToString(),
                SoThangDaO    = Convert.ToInt32(row["SoThangDaO"]),
                HetHanHopDong = Convert.ToBoolean(row["HetHanHopDong"])
            };
        }

        // ═══════════════════════════════════════════════════════════
        // 2. TÍNH TỶ LỆ HOÀN CỌC — LOGIC NGHIỆP VỤ CỐT LÕI
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Xác định tỷ lệ hoàn cọc cơ bản theo quy định của đồ án:
        ///   • Chưa ký HĐ (hoặc hủy trước ký):              hoàn 80%
        ///   • Đã ký HĐ, chưa hết hạn, ở dưới 6 tháng:     hoàn 50%
        ///   • Đã ký HĐ, chưa hết hạn, ở trên 6 tháng:     hoàn 70%
        ///   • Hết hạn hợp đồng:                            hoàn 100%
        /// </summary>
        public (decimal TyLe, string LyDo) TinhTyLeHoanCoc(
            bool daKyHopDong, bool hetHanHopDong, int soThangDaO)
        {
            if (!daKyHopDong)
                return (80m, "Đã cọc nhưng chưa ký hợp đồng — hoàn 80% tiền cọc");

            if (hetHanHopDong)
                return (100m, "Hết hạn thuê theo hợp đồng — hoàn 100% tiền cọc");

            if (soThangDaO < 6)
                return (50m, "Đã ký hợp đồng, lưu trú dưới 6 tháng — hoàn 50% tiền cọc");

            return (70m, "Đã ký hợp đồng, lưu trú trên 6 tháng — hoàn 70% tiền cọc");
        }

        // ═══════════════════════════════════════════════════════════
        // 3. TÍNH TOÁN TỔNG HỢP ĐỐI SOÁT
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Tính toán toàn bộ bảng đối soát cọc dựa trên thông tin hợp đồng.
        /// BLL lấy phí phát sinh và nợ tiền thuê từ DAL, rồi tính toán.
        /// </summary>
        public KetQuaDoiSoatDTO TinhDoiSoat(
            HopDongDoiSoatDTO hopDong,
            bool daKyHopDong,
            DateTime ngayTraPhong,
            string ghiChu = "")
        {
            // --- Bước 1: Xác định tỷ lệ hoàn cọc ---
            var (tyLe, lyDo) = TinhTyLeHoanCoc(daKyHopDong, hopDong.HetHanHopDong, hopDong.SoThangDaO);
            decimal soTienCocDuocXetHoan = Math.Round(hopDong.TienCocGoc * tyLe / 100m, 0);

            // --- Bước 2: Lấy danh sách phí phát sinh từ CSDL ---
            DataTable dtPhi = _dal.GetPhiPhatSinh(hopDong.MaHopDong);
            var danhSachPhi = new List<PhiPhatSinhDTO>();
            decimal tongPhiPhatSinh = 0;

            if (dtPhi != null)
            {
                foreach (DataRow row in dtPhi.Rows)
                {
                    decimal soTien = Convert.ToDecimal(row["SoTien"]);
                    tongPhiPhatSinh += soTien;
                    danhSachPhi.Add(new PhiPhatSinhDTO
                    {
                        MaPhiPS         = row["MaPhiPS"].ToString(),
                        LoaiPhi         = row["LoaiPhi"].ToString(),
                        MoTa            = row["MoTa"].ToString(),
                        SoTien          = soTien,
                        NgayGhiNhan     = Convert.ToDateTime(row["NgayGhiNhan"]),
                        NhanVienGhiNhan = row["NhanVienGhiNhan"].ToString()
                    });
                }
            }

            // --- Bước 3: Lấy tổng tiền thuê còn nợ ---
            decimal tongNoTienThue = 0;
            DataTable dtNo = _dal.GetTongNoTienThue(hopDong.MaHopDong);
            if (dtNo != null && dtNo.Rows.Count > 0)
                tongNoTienThue = Convert.ToDecimal(dtNo.Rows[0]["TongTienConNo"]);

            // --- Bước 4: Tính tổng khấu trừ và kết quả cuối ---
            decimal tongKhauTru = tongPhiPhatSinh + tongNoTienThue;
            decimal soTienHoanLai     = 0;
            decimal soTienPhaiDongThem = 0;

            decimal chenh = soTienCocDuocXetHoan - tongKhauTru;
            if (chenh >= 0)
                soTienHoanLai = chenh;        // Hoàn lại cho khách
            else
                soTienPhaiDongThem = -chenh;  // Khách phải đóng thêm

            return new KetQuaDoiSoatDTO
            {
                MaHopDong            = hopDong.MaHopDong,
                TienCocGoc           = hopDong.TienCocGoc,
                DaKyHopDong          = daKyHopDong,
                HetHanHopDong        = hopDong.HetHanHopDong,
                SoThangDaO           = hopDong.SoThangDaO,
                TyLeHoanCoc          = tyLe,
                LyDoTyLe             = lyDo,
                SoTienCocDuocXetHoan = soTienCocDuocXetHoan,
                TongPhiPhatSinh      = tongPhiPhatSinh,
                TongNoTienThue       = tongNoTienThue,
                TongKhauTru          = tongKhauTru,
                SoTienHoanLai        = soTienHoanLai,
                SoTienPhaiDongThem   = soTienPhaiDongThem,
                NgayTraPhong         = ngayTraPhong,
                GhiChu               = ghiChu,
                DanhSachPhiPhatSinh  = danhSachPhi
            };
        }

        // ═══════════════════════════════════════════════════════════
        // 4. LƯU BẢNG ĐỐI SOÁT
        // ═══════════════════════════════════════════════════════════

        public (bool success, string message) LuuBangDoiSoat(KetQuaDoiSoatDTO ketQua)
        {
            // Sinh mã tự động
            string maMoi = "DS" + DateTime.Now.ToString("yyMMddHHmm");

            var dto = new LuuDoiSoatDTO
            {
                MaDS                 = maMoi,
                MaHopDong            = ketQua.MaHopDong,
                NgayTraPhong         = ketQua.NgayTraPhong,
                DaKyHopDong          = ketQua.DaKyHopDong,
                HetHanHopDong        = ketQua.HetHanHopDong,
                TyLeHoanCoc          = ketQua.TyLeHoanCoc,
                SoTienCocDuocXetHoan = ketQua.SoTienCocDuocXetHoan,
                TongTienKhauTru      = ketQua.TongKhauTru,
                SoTienHoanLai        = ketQua.SoTienHoanLai,
                SoTienPhaiDongThem   = ketQua.SoTienPhaiDongThem,
                GhiChu               = ketQua.GhiChu
            };

            string ketQuaDB = _dal.LuuBangDoiSoat(dto);

            if (ketQuaDB == "SUCCESS")
                return (true, "Lập bảng đối soát cọc thành công! Hồ sơ đang chờ kế toán xác nhận.");

            return (false, ketQuaDB);
        }
    }
}

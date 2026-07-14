using System;
using System.Collections.Generic;
using System.Data;
using HomeStayDorm.DAL;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL
{
    /// <summary>
    /// BLL cho chức năng Trả phòng.
    /// Nơi duy nhất chứa logic nghiệp vụ — tuyệt đối không viết SQL ở đây.
    /// </summary>
    public class TraPhongBLL
    {
        private readonly TraPhongDAL _dal = new TraPhongDAL();

        // ─────────────────────────────────────────────────────────────
        // 1. TRA CỨU HỢP ĐỒNG
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Tra cứu hợp đồng đang hiệu lực.
        /// Validate: phải nhập ít nhất 1 điều kiện.
        /// </summary>
        public DataTable TraCuuHopDong(string sdt, string cccd, string maHopDong, out string errorMsg)
        {
            errorMsg = string.Empty;

            bool isEmpty = string.IsNullOrWhiteSpace(sdt)
                        && string.IsNullOrWhiteSpace(cccd)
                        && string.IsNullOrWhiteSpace(maHopDong);

            if (isEmpty)
            {
                errorMsg = "Vui lòng nhập ít nhất một điều kiện tìm kiếm!";
                return null;
            }

            string sdtVal  = string.IsNullOrWhiteSpace(sdt)       ? null : sdt.Trim();
            string cccdVal = string.IsNullOrWhiteSpace(cccd)      ? null : cccd.Trim();
            string hdVal   = string.IsNullOrWhiteSpace(maHopDong) ? null : maHopDong.Trim();

            DataTable dt = _dal.TraCuuHopDong(sdtVal, cccdVal, hdVal);

            if (dt == null || dt.Rows.Count == 0)
            {
                errorMsg = "Không tìm thấy hợp đồng đang hiệu lực!";
                return null;
            }

            return dt;
        }

        /// <summary>
        /// Map một dòng DataRow sang DTO để hiển thị thông tin lên Form.
        /// </summary>
        public HopDongTraPhongDTO MapRowToDTO(DataRow row)
        {
            T(row, "Email", out string email);
            T(row, "DiaChi", out string diaChi);
            T(row, "LoaiThue", out string loaiThue);
            T(row, "TrangThai", out string trangThai);
            T(row, "MaPhieuCoc", out string maPhieuCoc);

            return new HopDongTraPhongDTO
            {
                MaHopDong         = row["MaHopDong"].ToString(),
                MaKhachHang       = row["maKH"].ToString(),
                TenKhachHang      = row["TenKhachHang"].ToString(),
                CCCD              = row["CCCD"].ToString(),
                SoDienThoai       = row["SDT"].ToString(),
                Email             = email,
                DiaChi            = diaChi,
                TenPhong          = row["TenPhong"].ToString(),
                MaPhong           = row["MaPhong"].ToString(),
                LoaiThue          = loaiThue,
                NgayBatDau        = Convert.ToDateTime(row["NgayBatDau"]),
                NgayKetThuc       = Convert.ToDateTime(row["NgayKetThuc"]),
                GiaThue           = Convert.ToDecimal(row["GiaThue"]),
                TrangThaiHopDong  = trangThai,
                TienCocGoc        = Convert.ToDecimal(row["TienCocGoc"]),
                MaPhieuCoc        = maPhieuCoc
            };
        }

        // ─────────────────────────────────────────────────────────────
        // 2. VALIDATE & TẠO YÊU CẦU TRẢ PHÒNG
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Các lý do trả phòng hợp lệ (đồng bộ với ComboBox trên Form).
        /// </summary>
        public List<string> GetDanhSachLyDo()
        {
            return new List<string>
            {
                "Hết nhu cầu thuê",
                "Hết hạn hợp đồng",
                "Chuyển nơi ở khác",
                "Lý do khác"
            };
        }

        /// <summary>
        /// Validate nghiệp vụ trước khi gọi DAL tạo yêu cầu.
        /// Trả về true nếu hợp lệ, false kèm errorMsg nếu vi phạm.
        /// </summary>
        public bool ValidateYeuCau(YeuCauTraPhongDTO dto, HopDongTraPhongDTO hopDong, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (hopDong == null)
            {
                errorMsg = "Vui lòng tra cứu hợp đồng trước khi tạo yêu cầu trả phòng!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.LyDoTraPhong))
            {
                errorMsg = "Lý do trả phòng là trường bắt buộc!";
                return false;
            }

            if (dto.NgayDuKienTra == default)
            {
                errorMsg = "Ngày dự kiến trả phòng không được để trống!";
                return false;
            }

            if (dto.NgayDuKienTra.Date < hopDong.NgayBatDau.Date)
            {
                errorMsg = "Ngày dự kiến trả phòng không được nhỏ hơn ngày bắt đầu ở!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gọi DAL tạo yêu cầu trả phòng sau khi validate xong.
        /// </summary>
        public (bool success, string message) TaoYeuCauTraPhong(YeuCauTraPhongDTO dto, HopDongTraPhongDTO hopDong)
        {
            if (!ValidateYeuCau(dto, hopDong, out string err))
                return (false, err);

            string ketQua = _dal.TaoYeuCauTraPhong(dto);

            if (ketQua == "SUCCESS")
                return (true, "Tạo yêu cầu trả phòng thành công! Hồ sơ đã chuyển sang bước Kiểm tra hiện trạng.");

            return (false, ketQua);
        }

        // ─────────────────────────────────────────────────────────────
        // 3. DANH SÁCH YÊU CẦU ĐÃ TẠO
        // ─────────────────────────────────────────────────────────────

        public DataTable GetDanhSachYeuCauTraPhong()
        {
            return _dal.GetDanhSachYeuCauTraPhong();
        }

        // ─────────────────────────────────────────────────────────────
        // HELPER: đọc cột an toàn (không crash nếu cột không tồn tại)
        // ─────────────────────────────────────────────────────────────
        private static void T(DataRow row, string col, out string val)
        {
            val = row.Table.Columns.Contains(col) && row[col] != DBNull.Value
                ? row[col].ToString()
                : null;
        }
    }
}

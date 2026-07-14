using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL;
using HomeStayDorm.DTO;

namespace HomeStayDorm.UI
{
    public partial class frmTaoYeuCauTraPhong : Form
    {
        private readonly TraPhongBLL _bll = new TraPhongBLL();
        private HopDongTraPhongDTO _hopDangChon = null;

        public string MaNhanVienSale { get; set; } = "NV003";

        public frmTaoYeuCauTraPhong()
        {
            InitializeComponent();
        }

        // ── Load form ────────────────────────────────────────────────
        private void frmTaoYeuCauTraPhong_Load(object sender, EventArgs e)
        {
            cboLyDo.DataSource    = _bll.GetDanhSachLyDo();
            dtpNgayDuKien.Value   = DateTime.Today;
            dtpNgayDuKien.MinDate = DateTime.Today;
            lblNgayHT.Text        = "📅  " + DateTime.Today.ToString("dd/MM/yyyy");
            XoaThongTin();
        }

        // ── Tìm kiếm ─────────────────────────────────────────────────
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdt    = txtSDT.Text.Trim();
            string cccd   = txtCCCD.Text.Trim();
            string maHD   = txtMaHopDong.Text.Trim();
            string hoTen  = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(sdt) && string.IsNullOrEmpty(cccd)
                && string.IsNullOrEmpty(maHD) && string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập ít nhất một thông tin để tra cứu.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = _bll.TraCuuHopDong(sdt, cccd, maHD, out string err);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(err)
                    ? "Không tìm thấy hợp đồng phù hợp."
                    : err,
                    "Không có kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaThongTin();
                return;
            }

            // Lấy dòng đầu tiên (hoặc có thể mở dialog chọn nếu nhiều kết quả)
            _hopDangChon = _bll.MapRowToDTO(dt.Rows[0]);
            HienThiThongTin(_hopDangChon);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSDT.Clear();
            txtCCCD.Clear();
            txtMaHopDong.Clear();
            txtHoTen.Clear();
            txtGhiChu.Clear();
            dtpNgayDuKien.Value = DateTime.Today;
            if (cboLyDo.Items.Count > 0) cboLyDo.SelectedIndex = 0;
            XoaThongTin();
            txtSDT.Focus();
        }

        // ── Hiển thị thông tin sau khi tìm ───────────────────────────
        private void HienThiThongTin(HopDongTraPhongDTO hd)
        {
            // Thông tin khách thuê
            valHoTen.Text  = hd.TenKhachHang;
            valSDT.Text    = hd.SoDienThoai;
            valEmail.Text  = hd.Email ?? "—";
            valCCCD.Text   = hd.CCCD;
            valDiaChi.Text = hd.DiaChi ?? "—";

            // Thông tin hợp đồng
            valMaHD.Text      = hd.MaHopDong;
            valMaCoc.Text     = hd.MaPhieuCoc ?? "—";
            valPhong.Text     = hd.TenPhong;
            valLoai.Text      = hd.LoaiThue ?? "—";
            valNgayBD.Text    = hd.NgayBatDau.ToString("dd/MM/yyyy");
            valNgayHH.Text    = hd.NgayKetThuc.ToString("dd/MM/yyyy");
            valTrangThai.Text = hd.TrangThaiHopDong ?? "—";
            valCoc.Text       = hd.TienCocGoc.ToString("N0") + " đ";

            dtpNgayDuKien.MinDate = hd.NgayBatDau.Date;
            grpYeuCau.Enabled     = true;
        }

        private void XoaThongTin()
        {
            _hopDangChon = null;
            string dot   = "—";

            valHoTen.Text  = dot; valSDT.Text  = dot; valEmail.Text = dot;
            valCCCD.Text   = dot; valDiaChi.Text = dot;
            valMaHD.Text   = dot; valMaCoc.Text  = dot; valPhong.Text = dot;
            valLoai.Text   = dot; valNgayBD.Text = dot; valNgayHH.Text = dot;
            valTrangThai.Text = dot; valCoc.Text = dot;

            grpYeuCau.Enabled = false;
        }

        // ── Tạo yêu cầu ──────────────────────────────────────────────
        private void btnTaoYeuCau_Click(object sender, EventArgs e)
        {
            if (_hopDangChon == null)
            {
                MessageBox.Show("Vui lòng tra cứu và chọn hợp đồng trước.",
                    "Chưa chọn hợp đồng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new YeuCauTraPhongDTO
            {
                MaHopDong      = _hopDangChon.MaHopDong,
                NgayDuKienTra  = dtpNgayDuKien.Value.Date,
                LyDoTraPhong   = cboLyDo.SelectedItem?.ToString(),
                GhiChu         = txtGhiChu.Text.Trim(),
                MaNhanVienSale = MaNhanVienSale
            };

            var (success, message) = _bll.TaoYeuCauTraPhong(dto, _hopDangChon);

            if (success)
            {
                MessageBox.Show(message, "Thành công ✔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null);
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            if (_hopDangChon != null)
            {
                var cf = MessageBox.Show("Bạn có chắc muốn hủy thao tác?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cf != DialogResult.Yes) return;
            }
            btnLamMoi_Click(null, null);
        }

        // ── Đếm ký tự ghi chú ────────────────────────────────────────
        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {
            lblCounter.Text = txtGhiChu.Text.Length + " / 500";
        }
    }
}

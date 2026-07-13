using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL;
using HomeStayDorm.DTO;

namespace HomeStayDorm.UI
{
    public partial class frmDoiSoatCoc : Form
    {
        private readonly DoiSoatCocBLL _bll = new DoiSoatCocBLL();
        private HopDongDoiSoatDTO _hopDongDaChon = null;
        private KetQuaDoiSoatDTO _ketQuaDoiSoat = null;

        public frmDoiSoatCoc()
        {
            InitializeComponent();
        }

        private void frmDoiSoatCoc_Load(object sender, EventArgs e)
        {
            lblDate.Text = "📅 " + DateTime.Today.ToString("dd/MM/yyyy");
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnHuyBo.Click += btnHuyBo_Click;
            btnTinhLai.Click += btnTinhLai_Click;
            btnXacNhan.Click += btnXacNhan_Click;
            
            ConfigureDgvTinhToan();
            ConfigureDgvHuHong();
            
            grpInfo.Enabled = false;
            grpFinance.Enabled = false;
            btnXacNhan.Enabled = false;
            btnTinhLai.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdt = radSDT.Checked ? txtPhone.Text.Trim() : null;
            string cccd = radCCCD.Checked ? txtCCCD.Text.Trim() : null;

            DataTable dt = _bll.GetHopDongChoDoiSoat(sdt, cccd, out string errMsg);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(errMsg) ? "Không tìm thấy dữ liệu!" : errMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Map the first result
            _hopDongDaChon = _bll.MapRow(dt.Rows[0]);
            HienThiThongTinKhachVaHopDong();
            
            // Automatically calculate
            btnTinhLai_Click(null, null);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtPhone.Clear();
            txtCCCD.Clear();
            _hopDongDaChon = null;
            _ketQuaDoiSoat = null;
            
            valMaKH.Text = "---"; valHoTen.Text = "---"; valSDT.Text = "---"; valCCCD.Text = "---"; valEmail.Text = "---";
            valMaHD.Text = "---"; valPhong.Text = "---"; valNgayBD.Text = "---"; valNgayDuKien.Text = "---"; valNgayTao.Text = "---";
            valMaPhieu.Text = "---"; valNgayCoc.Text = "---"; valTienCoc.Text = "---"; valHinhThuc.Text = "---"; valBoPhan.Text = "---";
            
            dgvTinhToan.Rows.Clear();
            dgvChiTietHuHong.Rows.Clear();
            txtGhiChu.Clear();
            lblTongHuHong.Text = "Tổng chi phí hư hỏng: 0 VNĐ";

            grpInfo.Enabled = false;
            grpFinance.Enabled = false;
            btnXacNhan.Enabled = false;
            btnTinhLai.Enabled = false;
        }

        private void HienThiThongTinKhachVaHopDong()
        {
            if (_hopDongDaChon == null) return;
            
            valMaKH.Text = _hopDongDaChon.MaKhachHang;
            valHoTen.Text = _hopDongDaChon.TenKhachHang;
            valSDT.Text = _hopDongDaChon.SoDienThoai;
            valCCCD.Text = _hopDongDaChon.CCCD;
            valEmail.Text = "---"; // Not in DTO
            
            valMaHD.Text = _hopDongDaChon.MaHopDong;
            valPhong.Text = _hopDongDaChon.TenPhong;
            valNgayBD.Text = _hopDongDaChon.NgayBatDau.ToString("dd/MM/yyyy");
            valNgayDuKien.Text = _hopDongDaChon.NgayKetThuc.ToString("dd/MM/yyyy");
            valNgayTao.Text = DateTime.Today.ToString("dd/MM/yyyy");

            valMaPhieu.Text = string.IsNullOrEmpty(_hopDongDaChon.MaPhieuCoc) ? "---" : _hopDongDaChon.MaPhieuCoc;
            valNgayCoc.Text = _hopDongDaChon.NgayBatDau.ToString("dd/MM/yyyy");
            valTienCoc.Text = _hopDongDaChon.TienCocGoc.ToString("N0");
            valHinhThuc.Text = "Tiền mặt"; // Mock
            valBoPhan.Text = "Sale"; // Mock
            
            grpInfo.Enabled = true;
            grpFinance.Enabled = true;
            btnTinhLai.Enabled = true;
        }

        private void btnTinhLai_Click(object sender, EventArgs e)
        {
            if (_hopDongDaChon == null) return;

            // Default logic: assumed DaKyHopDong = true, NgayTra = today
            _ketQuaDoiSoat = _bll.TinhDoiSoat(_hopDongDaChon, true, DateTime.Today, txtGhiChu.Text.Trim());

            HienThiBangTinhToan();
            btnXacNhan.Enabled = true;
        }

        private void HienThiBangTinhToan()
        {
            if (_ketQuaDoiSoat == null) return;

            dgvTinhToan.Rows.Clear();
            
            // Nhóm 1: Tiền cọc
            dgvTinhToan.Rows.Add("1", "Tiền cọc gốc", "-", _ketQuaDoiSoat.TienCocGoc.ToString("N0"));
            dgvTinhToan.Rows.Add("2", "Tiền cọc được xét hoàn", $"Tỷ lệ hoàn: {_ketQuaDoiSoat.TyLeHoanCoc}%", _ketQuaDoiSoat.SoTienCocDuocXetHoan.ToString("N0"));
            
            // Nhóm 2: Tiền thuê / Tiện ích (A)
            dgvTinhToan.Rows.Add("3", "Tiền thuê phòng còn nợ", "Theo kỳ thanh toán", _ketQuaDoiSoat.TongNoTienThue.ToString("N0"));
            dgvTinhToan.Rows.Add("4", "Tiền điện", "(Chỉ số mới - Chỉ số cũ) x Đơn giá", "0");
            dgvTinhToan.Rows.Add("5", "Tiền nước", "(Chỉ số mới - Chỉ số cũ) x Đơn giá", "0");
            dgvTinhToan.Rows.Add("6", "Phí dịch vụ", "(Số ngày ở x Phí dịch vụ/ngày)", "0");
            AddHighlightRow(dgvTinhToan, "A", "Tổng nợ thuê / tiện ích", "(3 + 4 + 5 + 6)", _ketQuaDoiSoat.TongNoTienThue.ToString("N0"));

            // Nhóm 3: Khấu trừ / Hư hỏng (B)
            dgvTinhToan.Rows.Add("7", "Chi phí hư hỏng tài sản", "Theo biên bản kiểm tra", _ketQuaDoiSoat.TongPhiPhatSinh.ToString("N0"));
            dgvTinhToan.Rows.Add("8", "Chi phí vệ sinh", "Theo biên bản kiểm tra", "0");
            dgvTinhToan.Rows.Add("9", "Tiền phạt vi phạm", "Theo chính sách", "0");
            dgvTinhToan.Rows.Add("10", "Khoản khác", "Nhập ghi chú", "0");
            AddHighlightRow(dgvTinhToan, "B", "Tổng chi phí bồi thường", "(7 + 8 + 9 + 10)", _ketQuaDoiSoat.TongPhiPhatSinh.ToString("N0"));
            
            // Kết quả cuối cùng (C)
            if (_ketQuaDoiSoat.SoTienHoanLai > 0)
                AddHighlightRow(dgvTinhToan, "C", "SỐ TIỀN HOÀN LẠI KHÁCH", "(2) - (A) - (B)", _ketQuaDoiSoat.SoTienHoanLai.ToString("N0"), true);
            else if (_ketQuaDoiSoat.SoTienPhaiDongThem > 0)
                AddHighlightRow(dgvTinhToan, "C", "SỐ TIỀN KHÁCH ĐÓNG THÊM", "(A) + (B) - (2)", _ketQuaDoiSoat.SoTienPhaiDongThem.ToString("N0"), true);
            else
                AddHighlightRow(dgvTinhToan, "C", "KHÔNG PHÁT SINH TIỀN", "(2) = (A) + (B)", "0", true);

            // Right side grid (Damages detail)
            dgvChiTietHuHong.Rows.Clear();
            foreach (var p in _ketQuaDoiSoat.DanhSachPhiPhatSinh)
            {
                dgvChiTietHuHong.Rows.Add(p.LoaiPhi, p.MoTa, p.SoTien.ToString("N0"));
            }
            lblTongHuHong.Text = $"Tổng chi phí hư hỏng: {_ketQuaDoiSoat.TongPhiPhatSinh:N0} VNĐ";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (_ketQuaDoiSoat == null) return;

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xác nhận đối soát?\n\n" +
                $"Hợp đồng: {_ketQuaDoiSoat.MaHopDong}\n" +
                $"Tỷ lệ hoàn: {_ketQuaDoiSoat.TyLeHoanCoc}%\n",
                "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (success, message) = _bll.LuuBangDoiSoat(_ketQuaDoiSoat);
            if (success)
            {
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null);
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            if (_ketQuaDoiSoat != null)
            {
                if (MessageBox.Show("Bạn có muốn hủy bỏ thao tác?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            btnLamMoi_Click(null, null);
        }

        private void AddHighlightRow(DataGridView dgv, string stt, string noiDung, string cachTinh, string soTien, bool isTotal = false)
        {
            int idx = dgv.Rows.Add(stt, noiDung, cachTinh, soTien);
            dgv.Rows[idx].DefaultCellStyle.BackColor = isTotal ? Color.FromArgb(230, 240, 255) : Color.FromArgb(243, 244, 246);
            dgv.Rows[idx].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
        }

        private void ConfigureDgvTinhToan()
        {
            dgvTinhToan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "STT", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTinhToan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nội dung", Width = 200 });
            dgvTinhToan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cách tính", Width = 250 });
            dgvTinhToan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số tiền (VNĐ)", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void ConfigureDgvHuHong()
        {
            dgvChiTietHuHong.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hạng mục", Width = 120 });
            dgvChiTietHuHong.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mô tả", Width = 180 });
            dgvChiTietHuHong.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chi phí (VNĐ)", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        }
    }
}

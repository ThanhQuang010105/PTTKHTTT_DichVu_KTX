using System;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.DangKyThue;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.DangKyThue
{
    public class frmTraCuuPhong : Form
    {
        private readonly DangKyThueBLL _dangKyThueBLL = new DangKyThueBLL();
        private readonly ComboBox cboHinhThucThue = new ComboBox();
        private readonly ComboBox cboKhuVuc = new ComboBox();
        private readonly ComboBox cboGioiTinh = new ComboBox();
        private readonly ComboBox cboLoaiPhong = new ComboBox();
        private readonly NumericUpDown nudSoNguoi = new NumericUpDown();
        private readonly NumericUpDown nudGiaToiDa = new NumericUpDown();
        private readonly DateTimePicker dtpNgayVao = new DateTimePicker();
        private readonly TextBox txtTieuChi = new TextBox();
        private readonly DataGridView dgvKetQua = UiHelper.Grid();
        private readonly Label lblTrangThai = new Label();

        public frmTraCuuPhong()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Tra cứu phòng/giường trống";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1040, 720);
            MinimumSize = new Size(900, 520);
            BackColor = UiHelper.Surface;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = false;

            Panel viewport = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiHelper.Surface
            };

            Panel content = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1020, 790),
                BackColor = UiHelper.Surface
            };

            Label title = new Label
            {
                Text = "Tra cứu phòng/giường khả dụng",
                Location = new Point(24, 22),
                Size = new Size(960, 38),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text
            };

            GroupBox filters = new GroupBox
            {
                Text = "Bộ lọc",
                Location = new Point(22, 80),
                Size = new Size(978, 275),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            AddFilter(filters, "Hình thức", cboHinhThucThue, 18, 36, 220);
            AddFilter(filters, "Khu vực", cboKhuVuc, 260, 36, 220);
            AddFilter(filters, "Giới tính", cboGioiTinh, 502, 36, 220);
            AddFilter(filters, "Loại phòng", cboLoaiPhong, 744, 36, 220);
            AddFilter(filters, "Số người", nudSoNguoi, 18, 116, 220);
            AddFilter(filters, "Giá tối đa", nudGiaToiDa, 260, 116, 220);
            AddFilter(filters, "Ngày vào", dtpNgayVao, 502, 116, 220);
            AddFilter(filters, "Ưu tiên", txtTieuChi, 744, 116, 220);

            Button btnTraCuu = UiHelper.Button("Tra cứu", UiHelper.Primary);
            btnTraCuu.AutoSize = false;
            btnTraCuu.Location = new Point(818, 216);
            btnTraCuu.Size = new Size(145, 42);
            btnTraCuu.TextAlign = ContentAlignment.MiddleCenter;
            btnTraCuu.Click += BtnTraCuu_Click;

            Button btnLamMoi = UiHelper.Button("Làm mới");
            btnLamMoi.AutoSize = false;
            btnLamMoi.Location = new Point(695, 216);
            btnLamMoi.Size = new Size(112, 42);
            btnLamMoi.TextAlign = ContentAlignment.MiddleCenter;
            btnLamMoi.Click += (_, _) => ResetForm();

            filters.Controls.Add(btnTraCuu);
            filters.Controls.Add(btnLamMoi);

            GroupBox resultGroup = new GroupBox
            {
                Text = "Kết quả tra cứu",
                Location = new Point(22, 375),
                Size = new Size(978, 310),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            resultGroup.Controls.Add(dgvKetQua);

            lblTrangThai.Location = new Point(24, 700);
            lblTrangThai.Size = new Size(960, 46);
            lblTrangThai.ForeColor = UiHelper.Muted;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            content.Controls.Add(title);
            content.Controls.Add(filters);
            content.Controls.Add(resultGroup);
            content.Controls.Add(lblTrangThai);
            viewport.Controls.Add(content);
            Controls.Add(viewport);

            LoadDefaults();
        }

        private static void AddFilter(Control parent, string labelText, Control control, int x, int y, int width)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(x, y),
                Size = new Size(width, 26),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = UiHelper.Muted
            };

            control.Location = new Point(x, y + 28);
            control.Size = new Size(width, 36);
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            parent.Controls.Add(label);
            parent.Controls.Add(control);
        }

        private void LoadDefaults()
        {
            UiHelper.ConfigureCombo(cboHinhThucThue, DangKyThueBLL.ThueNguyenPhong, DangKyThueBLL.ThueGiuongOGhep);
            UiHelper.ConfigureCombo(cboKhuVuc, "Quận 1", "Quận 3", "Bình Thạnh", "Thủ Đức");
            UiHelper.ConfigureCombo(cboGioiTinh, "Nam", "Nữ", "Không yêu cầu");
            UiHelper.ConfigureCombo(cboLoaiPhong, "Tiêu chuẩn", "Cao cấp", "Studio");
            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = 20;
            nudGiaToiDa.Minimum = 500000;
            nudGiaToiDa.Maximum = 100000000;
            nudGiaToiDa.Increment = 500000;
            nudGiaToiDa.ThousandsSeparator = true;
            dtpNgayVao.Format = DateTimePickerFormat.Custom;
            dtpNgayVao.CustomFormat = "dd/MM/yyyy";
            dtpNgayVao.MinDate = DateTime.Today;
            ResetForm();
        }

        private void ResetForm()
        {
            cboHinhThucThue.SelectedIndex = 0;
            cboKhuVuc.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            nudSoNguoi.Value = 1;
            nudGiaToiDa.Value = 4000000;
            dtpNgayVao.Value = DateTime.Today;
            txtTieuChi.Clear();
            dgvKetQua.DataSource = null;
            lblTrangThai.Text = "Nhập bộ lọc và bấm Tra cứu.";
            lblTrangThai.ForeColor = UiHelper.Muted;
        }

        private void BtnTraCuu_Click(object? sender, EventArgs e)
        {
            try
            {
                DangKyTraCuuResult result = _dangKyThueBLL.TraCuuPhongGiuongKhaDung(BuildDto());
                dgvKetQua.DataSource = result.KetQua;
                lblTrangThai.Text = result.ThongBao;
                lblTrangThai.ForeColor = result.ThanhCong ? UiHelper.Success : UiHelper.Danger;
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private PhieuDangKyThueDTO BuildDto()
        {
            return new PhieuDangKyThueDTO
            {
                HinhThucThue = Convert.ToString(cboHinhThucThue.SelectedItem) ?? string.Empty,
                KhuVucMongMuon = Convert.ToString(cboKhuVuc.SelectedItem) ?? string.Empty,
                GioiTinh = Convert.ToString(cboGioiTinh.SelectedItem) ?? string.Empty,
                LoaiPhong = Convert.ToString(cboLoaiPhong.SelectedItem) ?? string.Empty,
                SoNguoiDuKien = Convert.ToInt32(nudSoNguoi.Value),
                GiaToiDa = nudGiaToiDa.Value,
                NgayVaoDuKien = dtpNgayVao.Value.Date,
                TieuChiUuTien = txtTieuChi.Text.Trim()
            };
        }
    }
}

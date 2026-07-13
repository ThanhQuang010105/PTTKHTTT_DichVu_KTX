using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.QuanTriHeThong
{
    public class frmQuanLyNhanVien : Form
    {
        private readonly NhanVienBLL _nhanVienBLL = new NhanVienBLL();
        private readonly ChiNhanhBLL _chiNhanhBLL = new ChiNhanhBLL();
        private readonly DataGridView dgvNhanVien = UiHelper.Grid();
        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtTenDangNhap = new TextBox();
        private readonly TextBox txtEmail = new TextBox();
        private readonly TextBox txtSoDienThoai = new TextBox();
        private readonly TextBox txtMatKhau = new TextBox();
        private readonly ComboBox cboVaiTro = new ComboBox();
        private readonly ComboBox cboChiNhanh = new ComboBox();
        private readonly ComboBox cboTrangThai = new ComboBox();
        private readonly DateTimePicker dtpNgayVaoLam = new DateTimePicker();
        private readonly Label lblTrangThai = new Label();
        private int _maNhanVien;

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            LoadCombos();
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Quản lý nhân viên";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 760);
            MinimumSize = new Size(900, 560);
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
                Size = new Size(970, 780),
                BackColor = UiHelper.Surface
            };

            Label title = new Label
            {
                Text = "Quản lý nhân viên",
                Location = new Point(24, 22),
                Size = new Size(900, 40),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text
            };

            GroupBox form = new GroupBox
            {
                Text = "Thông tin nhân viên",
                Location = new Point(22, 82),
                Size = new Size(916, 315),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            AddField(form, "Họ tên *", txtHoTen, 18, 34, 135, 260);
            AddField(form, "Tên đăng nhập *", txtTenDangNhap, 18, 84, 135, 260);
            AddField(form, "Email *", txtEmail, 18, 134, 135, 260);
            AddField(form, "SĐT", txtSoDienThoai, 18, 184, 135, 260);
            AddField(form, "Mật khẩu", txtMatKhau, 18, 234, 135, 260);

            AddField(form, "Vai trò *", cboVaiTro, 485, 34, 120, 275);
            AddField(form, "Chi nhánh", cboChiNhanh, 485, 84, 120, 275);
            AddField(form, "Trạng thái", cboTrangThai, 485, 134, 120, 275);
            AddField(form, "Ngày vào làm", dtpNgayVaoLam, 485, 184, 120, 275);

            Button btnMoi = FixedButton("Thêm mới", null, 120);
            btnMoi.Location = new Point(495, 254);
            btnMoi.Click += (_, _) => ClearForm();

            Button btnKhoa = FixedButton("Khóa tài khoản", UiHelper.Danger, 150);
            btnKhoa.Location = new Point(623, 254);
            btnKhoa.Click += BtnKhoa_Click;

            Button btnLuu = FixedButton("Lưu", UiHelper.Primary, 100);
            btnLuu.Location = new Point(781, 254);
            btnLuu.Click += BtnLuu_Click;

            form.Controls.Add(btnMoi);
            form.Controls.Add(btnKhoa);
            form.Controls.Add(btnLuu);

            GroupBox list = new GroupBox
            {
                Text = "Danh sách nhân viên",
                Location = new Point(22, 417),
                Size = new Size(916, 275),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            list.Controls.Add(dgvNhanVien);
            dgvNhanVien.SelectionChanged += (_, _) => FillSelectedRow();

            lblTrangThai.Location = new Point(24, 705);
            lblTrangThai.Size = new Size(900, 46);
            lblTrangThai.ForeColor = UiHelper.Muted;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            txtMatKhau.PasswordChar = '*';
            txtMatKhau.PlaceholderText = "Bỏ trống khi không đổi mật khẩu";
            dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
            dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";

            content.Controls.Add(title);
            content.Controls.Add(form);
            content.Controls.Add(list);
            content.Controls.Add(lblTrangThai);
            viewport.Controls.Add(content);
            Controls.Add(viewport);

            UiHelper.ConfigureCombo(cboVaiTro, "Sale", "QuanLy", "KeToan");
            UiHelper.ConfigureCombo(cboTrangThai, "Đang làm", "Nghỉ việc");
        }

        private static void AddField(Control parent, string labelText, Control control, int x, int y, int labelWidth, int controlWidth)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(x, y),
                Size = new Size(labelWidth, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = UiHelper.Muted
            };

            control.Location = new Point(x + labelWidth + 12, y);
            control.Size = new Size(controlWidth, 36);
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            parent.Controls.Add(label);
            parent.Controls.Add(control);
        }

        private static Button FixedButton(string text, Color? backColor, int width)
        {
            Button button = UiHelper.Button(text, backColor);
            button.AutoSize = false;
            button.Size = new Size(width, 42);
            button.TextAlign = ContentAlignment.MiddleCenter;
            return button;
        }

        private void LoadCombos()
        {
            DataTable chiNhanh = _chiNhanhBLL.LayDanhSach();
            cboChiNhanh.DataSource = chiNhanh;
            cboChiNhanh.DisplayMember = "TenChiNhanh";
            cboChiNhanh.ValueMember = "MaChiNhanh";
        }

        private void LoadData()
        {
            try
            {
                dgvNhanVien.DataSource = _nhanVienBLL.LayDanhSach();
                lblTrangThai.Text = "Đã tải danh sách nhân viên.";
                lblTrangThai.ForeColor = UiHelper.Success;
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void FillSelectedRow()
        {
            if (dgvNhanVien.CurrentRow == null) return;
            _maNhanVien = Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value);
            txtHoTen.Text = Convert.ToString(dgvNhanVien.CurrentRow.Cells["HoTen"].Value);
            txtTenDangNhap.Text = Convert.ToString(dgvNhanVien.CurrentRow.Cells["TenDangNhap"].Value);
            txtEmail.Text = Convert.ToString(dgvNhanVien.CurrentRow.Cells["Email"].Value);
            txtSoDienThoai.Text = Convert.ToString(dgvNhanVien.CurrentRow.Cells["SoDienThoai"].Value);
            cboVaiTro.SelectedItem = Convert.ToString(dgvNhanVien.CurrentRow.Cells["VaiTro"].Value);
            cboTrangThai.SelectedItem = Convert.ToString(dgvNhanVien.CurrentRow.Cells["TrangThai"].Value);
            if (dgvNhanVien.CurrentRow.Cells["MaChiNhanh"].Value != DBNull.Value)
            {
                cboChiNhanh.SelectedValue = Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaChiNhanh"].Value);
            }
            dtpNgayVaoLam.Value = Convert.ToDateTime(dgvNhanVien.CurrentRow.Cells["NgayVaoLam"].Value);
            txtMatKhau.Clear();
        }

        private void ClearForm()
        {
            _maNhanVien = 0;
            txtHoTen.Clear();
            txtTenDangNhap.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtMatKhau.Clear();
            cboVaiTro.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;
            dtpNgayVaoLam.Value = DateTime.Today;
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            try
            {
                NhanVienDTO dto = new NhanVienDTO
                {
                    MaNhanVien = _maNhanVien,
                    HoTen = txtHoTen.Text.Trim(),
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    VaiTro = Convert.ToString(cboVaiTro.SelectedItem) ?? "Sale",
                    MaChiNhanh = cboChiNhanh.SelectedValue == null ? null : Convert.ToInt32(cboChiNhanh.SelectedValue),
                    TrangThai = Convert.ToString(cboTrangThai.SelectedItem) ?? "Đang làm",
                    NgayVaoLam = dtpNgayVaoLam.Value.Date
                };
                _maNhanVien = _nhanVienBLL.Luu(dto, txtMatKhau.Text);
                LoadData();
                lblTrangThai.Text = $"Đã lưu nhân viên #{_maNhanVien}.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnKhoa_Click(object? sender, EventArgs e)
        {
            try
            {
                _nhanVienBLL.KhoaTaiKhoan(_maNhanVien);
                LoadData();
                lblTrangThai.Text = "Đã khóa tài khoản nhân viên.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }
    }
}

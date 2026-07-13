using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.DangKyThue;
using HomeStayDorm.DTO;

namespace HomeStayDorm.UI.DangKyThue
{
    public class frmDangKyThue : Form
    {
        private readonly DangKyThueBLL _dangKyThueBLL = new DangKyThueBLL();

        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtSoDienThoai = new TextBox();
        private readonly TextBox txtCCCD = new TextBox();
        private readonly TextBox txtEmail = new TextBox();
        private readonly ComboBox cboGioiTinh = new ComboBox();
        private readonly ComboBox cboHinhThucThue = new ComboBox();
        private readonly ComboBox cboKhuVuc = new ComboBox();
        private readonly ComboBox cboLoaiPhong = new ComboBox();
        private readonly NumericUpDown nudSoNguoi = new NumericUpDown();
        private readonly NumericUpDown nudGiaToiDa = new NumericUpDown();
        private readonly DateTimePicker dtpNgayVao = new DateTimePicker();
        private readonly NumericUpDown nudThoiHan = new NumericUpDown();
        private readonly TextBox txtTieuChiUuTien = new TextBox();
        private readonly DataGridView dgvKetQua = new DataGridView();
        private readonly Label lblMaDangKy = new Label();
        private readonly Label lblTrangThai = new Label();
        private readonly Button btnLuuVaTraCuu = new Button();
        private readonly Button btnXoaTrang = new Button();

        private static readonly Color Surface = Color.FromArgb(246, 248, 251);
        private static readonly Color TextColor = Color.FromArgb(30, 41, 59);
        private static readonly Color MutedColor = Color.FromArgb(71, 85, 105);
        private static readonly Color PrimaryColor = Color.FromArgb(37, 99, 235);

        public frmDangKyThue()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Tạo đăng ký thuê / Tra cứu phòng-giường khả dụng";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1120, 760);
            MinimumSize = new Size(900, 560);
            BackColor = Surface;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = false;

            Panel viewport = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Surface
            };

            Panel content = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1100, 850),
                BackColor = Surface
            };

            Label lblTitle = new Label
            {
                Text = "Tạo đăng ký thuê",
                Location = new Point(24, 22),
                Size = new Size(1040, 38),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = TextColor
            };

            lblTrangThai.Location = new Point(24, 66);
            lblTrangThai.Size = new Size(1040, 44);
            lblTrangThai.ForeColor = MutedColor;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            GroupBox gbKhachHang = new GroupBox
            {
                Text = "Khối 1 - Thông tin khách hàng",
                Location = new Point(22, 120),
                Size = new Size(510, 260),
                Padding = new Padding(12),
                ForeColor = TextColor
            };

            AddField(gbKhachHang, "Họ tên *", txtHoTen, 20, 34, 135, 315);
            AddField(gbKhachHang, "Số điện thoại *", txtSoDienThoai, 20, 74, 135, 315);
            AddField(gbKhachHang, "CCCD/CMND", txtCCCD, 20, 114, 135, 315);
            AddField(gbKhachHang, "Email", txtEmail, 20, 154, 135, 315);
            AddField(gbKhachHang, "Giới tính *", cboGioiTinh, 20, 194, 135, 315);

            GroupBox gbNhuCau = new GroupBox
            {
                Text = "Khối 2 - Nhu cầu thuê",
                Location = new Point(545, 120),
                Size = new Size(535, 350),
                Padding = new Padding(12),
                ForeColor = TextColor
            };

            AddField(gbNhuCau, "Hình thức thuê *", cboHinhThucThue, 18, 34, 135, 345);
            AddField(gbNhuCau, "Khu vực *", cboKhuVuc, 18, 70, 135, 345);
            AddField(gbNhuCau, "Loại phòng *", cboLoaiPhong, 18, 106, 135, 345);
            AddField(gbNhuCau, "Số người *", nudSoNguoi, 18, 142, 135, 345);
            AddField(gbNhuCau, "Giá tối đa *", nudGiaToiDa, 18, 178, 135, 345);
            AddField(gbNhuCau, "Ngày vào *", dtpNgayVao, 18, 214, 135, 345);
            AddField(gbNhuCau, "Thời hạn thuê *", nudThoiHan, 18, 250, 135, 345);
            txtTieuChiUuTien.Multiline = true;
            txtTieuChiUuTien.ScrollBars = ScrollBars.Vertical;
            AddField(gbNhuCau, "Tiêu chí ưu tiên", txtTieuChiUuTien, 18, 286, 135, 345, 46);

            GroupBox resultGroup = new GroupBox
            {
                Text = "Khối 3 - Kết quả tra cứu",
                Location = new Point(22, 490),
                Size = new Size(1058, 270),
                Padding = new Padding(12),
                ForeColor = TextColor
            };
            ConfigureDataGrid();
            resultGroup.Controls.Add(dgvKetQua);

            ConfigureButton(btnLuuVaTraCuu, "Lưu phiếu && tìm phòng trống", PrimaryColor, Color.White);
            btnLuuVaTraCuu.Location = new Point(810, 785);
            btnLuuVaTraCuu.Size = new Size(270, 42);
            btnLuuVaTraCuu.Click += BtnLuuVaTraCuu_Click;

            ConfigureButton(btnXoaTrang, "Xóa trắng form", Color.White, TextColor);
            btnXoaTrang.Location = new Point(650, 785);
            btnXoaTrang.Size = new Size(150, 42);
            btnXoaTrang.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnXoaTrang.Click += BtnXoaTrang_Click;

            content.Controls.Add(lblTitle);
            content.Controls.Add(lblTrangThai);
            content.Controls.Add(gbKhachHang);
            content.Controls.Add(gbNhuCau);
            content.Controls.Add(resultGroup);
            content.Controls.Add(btnLuuVaTraCuu);
            content.Controls.Add(btnXoaTrang);
            viewport.Controls.Add(content);
            Controls.Add(viewport);

            LoadDefaultValues();
        }

        private static void AddField(Control parent, string labelText, Control control, int x, int y, int labelWidth, int controlWidth, int controlHeight = 30)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(x, y),
                Size = new Size(labelWidth, Math.Max(34, controlHeight)),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = MutedColor
            };

            control.Location = new Point(x + labelWidth + 12, y);
            control.Size = new Size(controlWidth, Math.Max(36, controlHeight));
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            parent.Controls.Add(label);
            parent.Controls.Add(control);
        }

        private static void ConfigureButton(Button button, string text, Color backColor, Color foreColor)
        {
            button.Text = text;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.UseCompatibleTextRendering = true;
            button.UseVisualStyleBackColor = false;
        }

        private void LoadDefaultValues()
        {
            txtHoTen.PlaceholderText = "Nguyễn Văn A";
            txtSoDienThoai.PlaceholderText = "0900000000";
            txtCCCD.PlaceholderText = "012345678901";
            txtEmail.PlaceholderText = "khach@example.com";
            txtTieuChiUuTien.PlaceholderText = "Yên tĩnh, có gửi xe, có điều hòa...";

            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Không yêu cầu" });

            cboHinhThucThue.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThucThue.Items.AddRange(new object[] { DangKyThueBLL.ThueNguyenPhong, DangKyThueBLL.ThueGiuongOGhep });

            cboKhuVuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhuVuc.Items.AddRange(new object[] { "Quận 1", "Quận 3", "Bình Thạnh", "Thủ Đức" });

            cboLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiPhong.Items.AddRange(new object[] { "Tiêu chuẩn", "Cao cấp", "Studio" });

            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = 20;
            nudSoNguoi.Value = 1;

            nudGiaToiDa.Minimum = 500000;
            nudGiaToiDa.Maximum = 100000000;
            nudGiaToiDa.Increment = 500000;
            nudGiaToiDa.ThousandsSeparator = true;
            nudGiaToiDa.Value = 4000000;

            dtpNgayVao.Format = DateTimePickerFormat.Custom;
            dtpNgayVao.CustomFormat = "dd/MM/yyyy";
            dtpNgayVao.MinDate = DateTime.Today;
            dtpNgayVao.Value = DateTime.Today;

            nudThoiHan.Minimum = 1;
            nudThoiHan.Maximum = 60;
            nudThoiHan.Value = 6;

            cboGioiTinh.SelectedIndex = 0;
            cboHinhThucThue.SelectedIndex = 0;
            cboKhuVuc.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;

            lblMaDangKy.Text = string.Empty;
            SetStatus("Nhập thông tin khách hàng và nhu cầu thuê, sau đó lưu phiếu để tra cứu phòng/giường khả dụng.", MutedColor);
        }

        private void ConfigureDataGrid()
        {
            dgvKetQua.Dock = DockStyle.Fill;
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.ReadOnly = true;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.BackgroundColor = Color.White;
            dgvKetQua.BorderStyle = BorderStyle.FixedSingle;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.RowHeadersVisible = false;
            dgvKetQua.EnableHeadersVisualStyles = false;
            dgvKetQua.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            dgvKetQua.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
            dgvKetQua.DefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254);
            dgvKetQua.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        }

        private void BtnLuuVaTraCuu_Click(object? sender, EventArgs e)
        {
            try
            {
                DangKyTraCuuResult result = _dangKyThueBLL.TaoDangKyVaTraCuu(BuildDto());
                dgvKetQua.DataSource = result.KetQua;

                if (!result.ThanhCong)
                {
                    lblMaDangKy.Text = string.Empty;
                    SetStatus(result.ThongBao, Color.FromArgb(220, 38, 38));
                    return;
                }

                lblMaDangKy.Text = $"Mã đăng ký vừa tạo: {result.MaDangKy}";
                SetStatus($"{lblMaDangKy.Text}. {result.ThongBao}", result.KetQua.Rows.Count == 0
                    ? Color.FromArgb(180, 83, 9)
                    : Color.FromArgb(22, 101, 52));
            }
            catch (SqlException ex)
            {
                SetStatus($"Không thể kết nối hoặc thao tác CSDL. Chi tiết: {ex.Message}", Color.FromArgb(220, 38, 38));
            }
            catch (Exception ex)
            {
                SetStatus($"Không thể xử lý đăng ký thuê. Chi tiết: {ex.Message}", Color.FromArgb(220, 38, 38));
            }
        }

        private void BtnXoaTrang_Click(object? sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtCCCD.Clear();
            txtEmail.Clear();
            txtTieuChiUuTien.Clear();
            dgvKetQua.DataSource = null;
            lblMaDangKy.Text = string.Empty;
            cboGioiTinh.SelectedIndex = 0;
            cboHinhThucThue.SelectedIndex = 0;
            cboKhuVuc.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            nudSoNguoi.Value = 1;
            nudGiaToiDa.Value = 4000000;
            dtpNgayVao.Value = DateTime.Today;
            nudThoiHan.Value = 6;
            SetStatus("Form đã được làm mới.", MutedColor);
        }

        private PhieuDangKyThueDTO BuildDto()
        {
            return new PhieuDangKyThueDTO
            {
                HoTenKhachHang = txtHoTen.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                GioiTinh = Convert.ToString(cboGioiTinh.SelectedItem) ?? string.Empty,
                HinhThucThue = Convert.ToString(cboHinhThucThue.SelectedItem) ?? string.Empty,
                KhuVucMongMuon = Convert.ToString(cboKhuVuc.SelectedItem) ?? string.Empty,
                LoaiPhong = Convert.ToString(cboLoaiPhong.SelectedItem) ?? string.Empty,
                SoNguoiDuKien = Convert.ToInt32(nudSoNguoi.Value),
                GiaToiDa = nudGiaToiDa.Value,
                NgayVaoDuKien = dtpNgayVao.Value.Date,
                ThoiHanThueThang = Convert.ToInt32(nudThoiHan.Value),
                TieuChiUuTien = txtTieuChiUuTien.Text.Trim()
            };
        }

        private void SetStatus(string message, Color color)
        {
            lblTrangThai.Text = message;
            lblTrangThai.ForeColor = color;
        }
    }
}

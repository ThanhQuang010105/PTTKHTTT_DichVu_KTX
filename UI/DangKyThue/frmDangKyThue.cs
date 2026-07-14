using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.DangKyThue;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.DangKyThue
{
    public class frmDangKyThue : Form
    {
        private readonly DangKyThueBLL _dangKyThueBLL = new DangKyThueBLL();
        private readonly ChiNhanhBLL _chiNhanhBLL = new ChiNhanhBLL();
        private readonly PhongBLL _phongBLL = new PhongBLL();

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
        private readonly Label lblTrangThai = new Label();
        private readonly Button btnLuu = new Button();
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
            Text = "Tạo đăng ký thuê";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1120, 610);
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
                Size = new Size(1100, 600),
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

            ConfigureButton(btnLuu, "Lưu phiếu đăng ký", PrimaryColor, Color.White);
            btnLuu.Location = new Point(842, 505);
            btnLuu.Size = new Size(238, 42);
            btnLuu.Click += BtnLuu_Click;

            ConfigureButton(btnXoaTrang, "Xóa trắng form", Color.White, TextColor);
            btnXoaTrang.Location = new Point(680, 505);
            btnXoaTrang.Size = new Size(150, 42);
            btnXoaTrang.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnXoaTrang.Click += BtnXoaTrang_Click;

            content.Controls.Add(lblTitle);
            content.Controls.Add(lblTrangThai);
            content.Controls.Add(gbKhachHang);
            content.Controls.Add(gbNhuCau);
            content.Controls.Add(btnLuu);
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

            UiHelper.ConfigureComboFromData(cboGioiTinh, _phongBLL.LayDanhMucGioiTinhQuyDinh(), "GioiTinh", "Nam", "Nữ", "Không yêu cầu");
            UiHelper.ConfigureCombo(cboHinhThucThue, DangKyThueBLL.ThueNguyenPhong, DangKyThueBLL.ThueGiuongOGhep);
            UiHelper.ConfigureComboFromData(cboKhuVuc, _chiNhanhBLL.LayDanhMucKhuVuc(), "KhuVuc", "Quận 1", "Bình Thạnh");
            UiHelper.ConfigureComboFromData(cboLoaiPhong, _phongBLL.LayDanhMucLoaiPhong(), "LoaiPhong", "Phòng 4 Người", "Phòng Đơn");

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

            SetStatus("Nhập thông tin khách hàng và nhu cầu thuê, sau đó lưu phiếu đăng ký.", MutedColor);
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            try
            {
                PhieuDangKyThueDTO thongTinDangKy = TaoThongTinDangKyTuDuLieuNhap();
                DangKyResult ketQuaKiemTra = _dangKyThueBLL.KiemTraThongTinHopLe(thongTinDangKy);

                if (!ketQuaKiemTra.ThanhCong)
                {
                    SetStatus(ketQuaKiemTra.ThongBao, Color.FromArgb(220, 38, 38));
                    return;
                }

                DangKyResult ketQuaLuu = _dangKyThueBLL.TaoPhieuDangKy(thongTinDangKy);
                if (!ketQuaLuu.ThanhCong)
                {
                    SetStatus(ketQuaLuu.ThongBao, Color.FromArgb(220, 38, 38));
                    return;
                }

                SetStatus(ketQuaLuu.ThongBao, Color.FromArgb(22, 101, 52));
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
            ResetCacTruongNhapLieu();
            SetStatus("Form đã được làm mới.", MutedColor);
        }

        private void ResetCacTruongNhapLieu()
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtCCCD.Clear();
            txtEmail.Clear();
            txtTieuChiUuTien.Clear();
            cboGioiTinh.SelectedIndex = 0;
            cboHinhThucThue.SelectedIndex = 0;
            cboKhuVuc.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            nudSoNguoi.Value = 1;
            nudGiaToiDa.Value = 4000000;
            dtpNgayVao.Value = DateTime.Today;
            nudThoiHan.Value = 6;
        }

        private PhieuDangKyThueDTO TaoThongTinDangKyTuDuLieuNhap()
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

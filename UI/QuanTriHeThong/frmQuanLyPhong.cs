using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.QuanTriHeThong
{
    public class frmQuanLyPhong : Form
    {
        private readonly PhongBLL _phongBLL = new PhongBLL();
        private readonly GiuongBLL _giuongBLL = new GiuongBLL();
        private readonly ChiNhanhBLL _chiNhanhBLL = new ChiNhanhBLL();

        private readonly DataGridView dgvPhong = UiHelper.Grid();
        private readonly DataGridView dgvGiuong = UiHelper.Grid();
        private readonly ComboBox cboChiNhanh = new ComboBox();
        private readonly TextBox txtTenPhong = new TextBox();
        private readonly TextBox txtKhuVuc = new TextBox();
        private readonly ComboBox cboGioiTinh = new ComboBox();
        private readonly ComboBox cboLoaiPhong = new ComboBox();
        private readonly NumericUpDown nudSucChua = new NumericUpDown();
        private readonly NumericUpDown nudGiaPhong = new NumericUpDown();
        private readonly ComboBox cboTrangThaiPhong = new ComboBox();

        private readonly ComboBox cboPhongGiuong = new ComboBox();
        private readonly TextBox txtTenGiuong = new TextBox();
        private readonly NumericUpDown nudGiaGiuong = new NumericUpDown();
        private readonly ComboBox cboTrangThaiGiuong = new ComboBox();
        private readonly Label lblTrangThai = new Label();

        private string _maPhong = string.Empty;
        private string _maGiuong = string.Empty;

        public frmQuanLyPhong()
        {
            InitializeComponent();
            LoadCombos();
            LoadPhong();
        }

        private void InitializeComponent()
        {
            Text = "Quản lý phòng/giường";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 720);
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
                Size = new Size(960, 790),
                BackColor = UiHelper.Surface
            };

            Label title = new Label
            {
                Text = "Quản lý phòng/giường",
                Location = new Point(24, 22),
                Size = new Size(900, 40),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text
            };

            TabControl tabs = new TabControl
            {
                Location = new Point(22, 82),
                Size = new Size(916, 620),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            TabPage tabPhong = new TabPage("Phòng") { BackColor = UiHelper.Surface };
            TabPage tabGiuong = new TabPage("Giường trong phòng") { BackColor = UiHelper.Surface };
            tabPhong.Controls.Add(CreatePhongLayout());
            tabGiuong.Controls.Add(CreateGiuongLayout());
            tabs.TabPages.Add(tabPhong);
            tabs.TabPages.Add(tabGiuong);

            lblTrangThai.Location = new Point(24, 712);
            lblTrangThai.Size = new Size(900, 46);
            lblTrangThai.ForeColor = UiHelper.Muted;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            content.Controls.Add(title);
            content.Controls.Add(tabs);
            content.Controls.Add(lblTrangThai);
            viewport.Controls.Add(content);
            Controls.Add(viewport);
        }

        private Control CreatePhongLayout()
        {
            Panel page = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiHelper.Surface
            };

            GroupBox form = new GroupBox
            {
                Text = "Thông tin phòng",
                Location = new Point(12, 12),
                Size = new Size(875, 260),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            AddField(form, "Chi nhánh *", cboChiNhanh, 18, 34, 110, 250);
            AddField(form, "Tên phòng *", txtTenPhong, 18, 80, 110, 250);
            AddField(form, "Khu vực *", txtKhuVuc, 18, 126, 110, 250);
            AddField(form, "Sức chứa", nudSucChua, 18, 172, 110, 250);
            AddField(form, "Giới tính", cboGioiTinh, 455, 34, 110, 280);
            AddField(form, "Loại phòng", cboLoaiPhong, 455, 80, 110, 280);
            AddField(form, "Giá thuê", nudGiaPhong, 455, 126, 110, 280);
            AddField(form, "Trạng thái", cboTrangThaiPhong, 455, 172, 110, 280);

            Button btnLuu = FixedButton("Lưu phòng", UiHelper.Primary, 128);
            btnLuu.Location = new Point(472, 214);
            btnLuu.Click += BtnLuuPhong_Click;

            Button btnXoa = FixedButton("Xóa phòng", UiHelper.Danger, 128);
            btnXoa.Location = new Point(608, 214);
            btnXoa.Click += BtnXoaPhong_Click;

            Button btnMoi = FixedButton("Thêm mới", null, 128);
            btnMoi.Location = new Point(744, 214);
            btnMoi.Click += (_, _) => ClearPhong();

            form.Controls.Add(btnLuu);
            form.Controls.Add(btnXoa);
            form.Controls.Add(btnMoi);

            GroupBox list = new GroupBox
            {
                Text = "Danh sách phòng",
                Location = new Point(12, 290),
                Size = new Size(875, 275),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            list.Controls.Add(dgvPhong);
            dgvPhong.SelectionChanged += (_, _) => FillSelectedPhong();

            page.Controls.Add(form);
            page.Controls.Add(list);
            return page;
        }

        private Control CreateGiuongLayout()
        {
            Panel page = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiHelper.Surface
            };

            GroupBox form = new GroupBox
            {
                Text = "Thông tin giường",
                Location = new Point(12, 12),
                Size = new Size(875, 205),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            AddField(form, "Phòng *", cboPhongGiuong, 18, 34, 110, 250);
            AddField(form, "Tên giường *", txtTenGiuong, 18, 82, 110, 250);
            AddField(form, "Giá thuê", nudGiaGiuong, 455, 34, 110, 280);
            AddField(form, "Trạng thái", cboTrangThaiGiuong, 455, 82, 110, 280);

            Button btnLuu = FixedButton("Lưu giường", UiHelper.Primary, 128);
            btnLuu.Location = new Point(472, 145);
            btnLuu.Click += BtnLuuGiuong_Click;

            Button btnXoa = FixedButton("Xóa giường", UiHelper.Danger, 128);
            btnXoa.Location = new Point(608, 145);
            btnXoa.Click += BtnXoaGiuong_Click;

            Button btnMoi = FixedButton("Thêm mới", null, 128);
            btnMoi.Location = new Point(744, 145);
            btnMoi.Click += (_, _) => ClearGiuong();

            form.Controls.Add(btnLuu);
            form.Controls.Add(btnXoa);
            form.Controls.Add(btnMoi);

            GroupBox list = new GroupBox
            {
                Text = "Danh sách giường",
                Location = new Point(12, 225),
                Size = new Size(875, 340),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            list.Controls.Add(dgvGiuong);
            cboPhongGiuong.SelectedIndexChanged += (_, _) => LoadGiuong();
            dgvGiuong.SelectionChanged += (_, _) => FillSelectedGiuong();

            page.Controls.Add(form);
            page.Controls.Add(list);
            return page;
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
            cboChiNhanh.DataSource = chiNhanh.Copy();
            cboChiNhanh.DisplayMember = "TenChiNhanh";
            cboChiNhanh.ValueMember = "MaChiNhanh";

            UiHelper.ConfigureCombo(cboGioiTinh, "Nam", "Nữ", "Không yêu cầu");
            UiHelper.ConfigureCombo(cboLoaiPhong, "Phòng 4 Người", "Phòng Đơn");
            UiHelper.ConfigureCombo(cboTrangThaiPhong, "Hoạt động", "Ngừng sử dụng");
            UiHelper.ConfigureCombo(cboTrangThaiGiuong, "Trống", "Đã thuê", "Ngừng sử dụng");
            nudSucChua.Minimum = 1;
            nudSucChua.Maximum = 30;
            ConfigureMoney(nudGiaPhong);
            ConfigureMoney(nudGiaGiuong);
        }

        private static void ConfigureMoney(NumericUpDown input)
        {
            input.Minimum = 100000;
            input.Maximum = 100000000;
            input.Increment = 100000;
            input.ThousandsSeparator = true;
            input.Value = 1000000;
        }

        private void LoadPhong()
        {
            try
            {
                DataTable phong = _phongBLL.LayDanhSach();
                dgvPhong.DataSource = phong;
                cboPhongGiuong.DataSource = phong.Copy();
                cboPhongGiuong.DisplayMember = "TenPhong";
                cboPhongGiuong.ValueMember = "MaPhong";
                lblTrangThai.Text = "Đã tải danh sách phòng.";
                lblTrangThai.ForeColor = UiHelper.Success;
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void LoadGiuong()
        {
            try
            {
                if (cboPhongGiuong.SelectedValue == null || cboPhongGiuong.SelectedValue is DataRowView)
                {
                    return;
                }

                string maPhong = Convert.ToString(cboPhongGiuong.SelectedValue) ?? string.Empty;
                dgvGiuong.DataSource = _giuongBLL.LayDanhSachTheoPhong(maPhong);
            }
            catch
            {
                dgvGiuong.DataSource = null;
            }
        }

        private void FillSelectedPhong()
        {
            if (dgvPhong.CurrentRow == null) return;
            _maPhong = Convert.ToString(dgvPhong.CurrentRow.Cells["MaPhong"].Value) ?? string.Empty;
            cboChiNhanh.SelectedValue = Convert.ToString(dgvPhong.CurrentRow.Cells["MaChiNhanh"].Value);
            txtTenPhong.Text = Convert.ToString(dgvPhong.CurrentRow.Cells["TenPhong"].Value);
            txtKhuVuc.Text = Convert.ToString(dgvPhong.CurrentRow.Cells["KhuVuc"].Value);
            cboGioiTinh.SelectedItem = Convert.ToString(dgvPhong.CurrentRow.Cells["GioiTinhQuyDinh"].Value);
            cboLoaiPhong.SelectedItem = Convert.ToString(dgvPhong.CurrentRow.Cells["LoaiPhong"].Value);
            nudSucChua.Value = Convert.ToDecimal(dgvPhong.CurrentRow.Cells["SucChua"].Value);
            nudGiaPhong.Value = Convert.ToDecimal(dgvPhong.CurrentRow.Cells["GiaThue"].Value);
            cboTrangThaiPhong.SelectedItem = Convert.ToString(dgvPhong.CurrentRow.Cells["TrangThaiPhong"].Value);
            cboPhongGiuong.SelectedValue = _maPhong;
        }

        private void FillSelectedGiuong()
        {
            if (dgvGiuong.CurrentRow == null) return;
            _maGiuong = Convert.ToString(dgvGiuong.CurrentRow.Cells["MaGiuong"].Value) ?? string.Empty;
            txtTenGiuong.Text = Convert.ToString(dgvGiuong.CurrentRow.Cells["TenGiuong"].Value);
            nudGiaGiuong.Value = Convert.ToDecimal(dgvGiuong.CurrentRow.Cells["GiaThue"].Value);
            cboTrangThaiGiuong.SelectedItem = Convert.ToString(dgvGiuong.CurrentRow.Cells["TrangThaiGiuong"].Value);
        }

        private void ClearPhong()
        {
            _maPhong = string.Empty;
            txtTenPhong.Clear();
            txtKhuVuc.Clear();
            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            cboTrangThaiPhong.SelectedIndex = 0;
            nudSucChua.Value = 1;
            nudGiaPhong.Value = 1000000;
        }

        private void ClearGiuong()
        {
            _maGiuong = string.Empty;
            txtTenGiuong.Clear();
            nudGiaGiuong.Value = 1000000;
            cboTrangThaiGiuong.SelectedIndex = 0;
        }

        private void BtnLuuPhong_Click(object? sender, EventArgs e)
        {
            try
            {
                PhongDTO dto = new PhongDTO
                {
                    MaPhong = _maPhong,
                    MaChiNhanh = Convert.ToString(cboChiNhanh.SelectedValue) ?? string.Empty,
                    TenPhong = txtTenPhong.Text.Trim(),
                    KhuVuc = txtKhuVuc.Text.Trim(),
                    GioiTinhQuyDinh = Convert.ToString(cboGioiTinh.SelectedItem) ?? "Không yêu cầu",
                    LoaiPhong = Convert.ToString(cboLoaiPhong.SelectedItem) ?? "Phòng 4 Người",
                    SucChua = Convert.ToInt32(nudSucChua.Value),
                    GiaThue = nudGiaPhong.Value,
                    TrangThaiPhong = Convert.ToString(cboTrangThaiPhong.SelectedItem) ?? "Trống"
                };
                _maPhong = _phongBLL.Luu(dto);
                LoadPhong();
                lblTrangThai.Text = $"Đã lưu phòng #{_maPhong}.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnXoaPhong_Click(object? sender, EventArgs e)
        {
            try
            {
                _phongBLL.Xoa(_maPhong);
                LoadPhong();
                ClearPhong();
                lblTrangThai.Text = "Đã cập nhật phòng sang Ngừng sử dụng.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnLuuGiuong_Click(object? sender, EventArgs e)
        {
            try
            {
                GiuongDTO dto = new GiuongDTO
                {
                    MaGiuong = _maGiuong,
                    MaPhong = Convert.ToString(cboPhongGiuong.SelectedValue) ?? string.Empty,
                    TenGiuong = txtTenGiuong.Text.Trim(),
                    GiaThue = nudGiaGiuong.Value,
                    TrangThaiGiuong = Convert.ToString(cboTrangThaiGiuong.SelectedItem) ?? "Trống"
                };
                _maGiuong = _giuongBLL.Luu(dto);
                LoadGiuong();
                lblTrangThai.Text = $"Đã lưu giường #{_maGiuong}.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnXoaGiuong_Click(object? sender, EventArgs e)
        {
            try
            {
                _giuongBLL.Xoa(_maGiuong);
                LoadGiuong();
                ClearGiuong();
                lblTrangThai.Text = "Đã cập nhật giường sang Ngừng sử dụng.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }
    }
}

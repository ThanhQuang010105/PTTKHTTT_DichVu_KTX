using System;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.QuanTriHeThong
{
    public class frmQuanLyChiNhanh : Form
    {
        private readonly ChiNhanhBLL _chiNhanhBLL = new ChiNhanhBLL();
        private readonly DataGridView dgvChiNhanh = UiHelper.Grid();
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtKhuVuc = new TextBox();
        private readonly TextBox txtDiaChi = new TextBox();
        private readonly TextBox txtSoDienThoai = new TextBox();
        private readonly ComboBox cboTrangThai = new ComboBox();
        private readonly Label lblTrangThai = new Label();
        private int _maChiNhanh;

        public frmQuanLyChiNhanh()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Quản lý chi nhánh";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(930, 690);
            MinimumSize = new Size(850, 520);
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
                Size = new Size(910, 700),
                BackColor = UiHelper.Surface
            };

            Label title = new Label
            {
                Text = "Quản lý chi nhánh",
                Location = new Point(24, 22),
                Size = new Size(850, 40),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text
            };

            GroupBox form = new GroupBox
            {
                Text = "Thông tin chi nhánh",
                Location = new Point(22, 82),
                Size = new Size(866, 235),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            AddField(form, "Tên chi nhánh *", txtTen, 18, 34, 125, 260);
            AddField(form, "Khu vực *", txtKhuVuc, 455, 34, 105, 280);
            AddField(form, "Địa chỉ *", txtDiaChi, 18, 84, 125, 697);
            AddField(form, "SĐT", txtSoDienThoai, 18, 134, 125, 260);
            AddField(form, "Trạng thái", cboTrangThai, 455, 134, 105, 280);

            Button btnMoi = FixedButton("Thêm mới", null, 120);
            btnMoi.Location = new Point(454, 183);
            btnMoi.Click += (_, _) => ClearForm();

            Button btnXoa = FixedButton("Xóa/Ngừng hoạt động", UiHelper.Danger, 185);
            btnXoa.Location = new Point(582, 183);
            btnXoa.Click += BtnXoa_Click;

            Button btnLuu = FixedButton("Lưu", UiHelper.Primary, 100);
            btnLuu.Location = new Point(775, 183);
            btnLuu.Click += BtnLuu_Click;

            form.Controls.Add(btnMoi);
            form.Controls.Add(btnXoa);
            form.Controls.Add(btnLuu);

            GroupBox list = new GroupBox
            {
                Text = "Danh sách chi nhánh",
                Location = new Point(22, 337),
                Size = new Size(866, 270),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            list.Controls.Add(dgvChiNhanh);
            dgvChiNhanh.SelectionChanged += (_, _) => FillSelectedRow();

            lblTrangThai.Location = new Point(24, 620);
            lblTrangThai.Size = new Size(850, 46);
            lblTrangThai.ForeColor = UiHelper.Muted;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            content.Controls.Add(title);
            content.Controls.Add(form);
            content.Controls.Add(list);
            content.Controls.Add(lblTrangThai);
            viewport.Controls.Add(content);
            Controls.Add(viewport);

            UiHelper.ConfigureCombo(cboTrangThai, "Hoạt động", "Ngừng hoạt động");
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

        private void LoadData()
        {
            try
            {
                dgvChiNhanh.DataSource = _chiNhanhBLL.LayDanhSach();
                lblTrangThai.Text = "Đã tải danh sách chi nhánh.";
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
            if (dgvChiNhanh.CurrentRow == null) return;
            _maChiNhanh = Convert.ToInt32(dgvChiNhanh.CurrentRow.Cells["MaChiNhanh"].Value);
            txtTen.Text = Convert.ToString(dgvChiNhanh.CurrentRow.Cells["TenChiNhanh"].Value);
            txtKhuVuc.Text = Convert.ToString(dgvChiNhanh.CurrentRow.Cells["KhuVuc"].Value);
            txtDiaChi.Text = Convert.ToString(dgvChiNhanh.CurrentRow.Cells["DiaChi"].Value);
            txtSoDienThoai.Text = Convert.ToString(dgvChiNhanh.CurrentRow.Cells["SoDienThoai"].Value);
            cboTrangThai.SelectedItem = Convert.ToString(dgvChiNhanh.CurrentRow.Cells["TrangThai"].Value);
        }

        private void ClearForm()
        {
            _maChiNhanh = 0;
            txtTen.Clear();
            txtKhuVuc.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            cboTrangThai.SelectedIndex = 0;
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            try
            {
                ChiNhanhDTO dto = new ChiNhanhDTO
                {
                    MaChiNhanh = _maChiNhanh,
                    TenChiNhanh = txtTen.Text.Trim(),
                    KhuVuc = txtKhuVuc.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    TrangThai = Convert.ToString(cboTrangThai.SelectedItem) ?? "Hoạt động"
                };
                _maChiNhanh = _chiNhanhBLL.Luu(dto);
                LoadData();
                lblTrangThai.Text = $"Đã lưu chi nhánh #{_maChiNhanh}.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                _chiNhanhBLL.Xoa(_maChiNhanh);
                LoadData();
                ClearForm();
                lblTrangThai.Text = "Đã cập nhật chi nhánh sang Ngừng hoạt động.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }
    }
}

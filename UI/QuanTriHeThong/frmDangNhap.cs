using System;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.QuanTriHeThong
{
    public class frmDangNhap : Form
    {
        private readonly AuthBLL _authBLL = new AuthBLL();
        private readonly TextBox txtTenDangNhap = new TextBox();
        private readonly TextBox txtMatKhau = new TextBox();
        private readonly CheckBox chkHienMatKhau = new CheckBox();
        private readonly Label lblThongBao = new Label();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Đăng nhập HomeStay Dorm";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(460, 360);
            MinimumSize = new Size(460, 360);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = UiHelper.Surface;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = false;

            Label title = new Label
            {
                Text = "HomeStay Dorm",
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = UiHelper.Text,
                Location = new Point(40, 34)
            };

            txtTenDangNhap.PlaceholderText = "Tên đăng nhập hoặc email";
            txtTenDangNhap.Location = new Point(40, 100);
            txtTenDangNhap.Size = new Size(380, 32);
            txtMatKhau.PlaceholderText = "Mật khẩu";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Location = new Point(40, 152);
            txtMatKhau.Size = new Size(380, 32);
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(40, 202);
            chkHienMatKhau.CheckedChanged += (_, _) => txtMatKhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '*';

            Button btnDangNhap = UiHelper.Button("Đăng nhập", UiHelper.Primary);
            btnDangNhap.AutoSize = false;
            btnDangNhap.Location = new Point(40, 248);
            btnDangNhap.Size = new Size(380, 44);
            btnDangNhap.Click += BtnDangNhap_Click;
            AcceptButton = btnDangNhap;

            lblThongBao.AutoSize = false;
            lblThongBao.ForeColor = UiHelper.Muted;
            lblThongBao.Text = "Demo: sale / quanly / ketoan, mật khẩu 123456";
            lblThongBao.Location = new Point(40, 306);
            lblThongBao.Size = new Size(380, 34);
            lblThongBao.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Add(title);
            Controls.Add(txtTenDangNhap);
            Controls.Add(txtMatKhau);
            Controls.Add(chkHienMatKhau);
            Controls.Add(btnDangNhap);
            Controls.Add(lblThongBao);
        }

        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            DangNhapResult result = _authBLL.DangNhap(txtTenDangNhap.Text, txtMatKhau.Text);
            if (!result.ThanhCong || result.NhanVien == null)
            {
                lblThongBao.Text = result.ThongBao;
                lblThongBao.ForeColor = UiHelper.Danger;
                return;
            }

            SessionContext.CurrentUser = result.NhanVien;
            Hide();
            using frmDashboard dashboard = new frmDashboard(result.NhanVien);
            dashboard.ShowDialog(this);
            Show();
            txtMatKhau.Clear();
        }
    }
}

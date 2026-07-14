using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using HomeStayDorm.BLL.TienIch;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.DangKyThue;
using HomeStayDorm.UI.QuanTriHeThong;

namespace HomeStayDorm.UI.Common
{
    public class frmDashboard : Form
    {
        private readonly NhanVienDTO _nhanVien;
        private readonly BaoCaoBLL _baoCaoBLL = new BaoCaoBLL();
        private readonly FlowLayoutPanel pnlStats = new FlowLayoutPanel();
        private readonly Label lblUser = new Label();

        public frmDashboard(NhanVienDTO nhanVien)
        {
            _nhanVien = nhanVien;
            InitializeComponent();
            LoadStats();
        }

        private void InitializeComponent()
        {
            Text = "Dashboard HomeStay Dorm";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(820, 520);
            BackColor = UiHelper.Surface;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = true;

            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(18)
            };
            UiHelper.UseNaturalRows(root);

            Label title = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = UiHelper.Text
            };
            lblUser.Text = $"{_nhanVien.HoTen} - {_nhanVien.VaiTro}";
            lblUser.AutoSize = true;
            lblUser.ForeColor = UiHelper.Muted;
            lblUser.Margin = new Padding(0, 4, 0, 14);

            FlowLayoutPanel menu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                Margin = new Padding(0, 0, 0, 14)
            };

            AddMenuButton(menu, "Đăng ký thuê", () => new frmDangKyThue().ShowDialog(this), "Sale", "QuanLy");
            AddMenuButton(menu, "Tra cứu phòng", () => new frmTraCuuPhong().ShowDialog(this), "Sale", "QuanLy");
            AddMenuButton(menu, "Lập lịch xem phòng", () => new frmLichXemPhong().ShowDialog(this), "Sale", "QuanLy");
            AddMenuButton(menu, "Quản lý phòng/giường", () => new frmQuanLyPhong().ShowDialog(this), "QuanLy");
            AddMenuButton(menu, "Quản lý chi nhánh", () => new frmQuanLyChiNhanh().ShowDialog(this), "QuanLy");
            AddMenuButton(menu, "Quản lý nhân viên", () => new frmQuanLyNhanVien().ShowDialog(this), "QuanLy");

            Button btnRefresh = UiHelper.Button("Làm mới thống kê");
            btnRefresh.Click += (_, _) => LoadStats();
            menu.Controls.Add(btnRefresh);

            pnlStats.Dock = DockStyle.Fill;
            pnlStats.AutoScroll = true;
            pnlStats.WrapContents = true;
            UiHelper.ReserveHeight(pnlStats, 430);

            TableLayoutPanel header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                RowCount = 2,
                ColumnCount = 1
            };
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            header.Controls.Add(title, 0, 0);
            header.Controls.Add(lblUser, 0, 1);

            root.Controls.Add(header, 0, 0);
            root.Controls.Add(menu, 0, 1);
            root.Controls.Add(pnlStats, 0, 2);
            Controls.Add(UiHelper.ScrollHost(root));
        }

        private void AddMenuButton(FlowLayoutPanel menu, string text, Action open, params string[] roles)
        {
            if (!IsInRole(roles))
            {
                return;
            }

            Button button = UiHelper.Button(text, UiHelper.Primary);
            button.Click += (_, _) => open();
            menu.Controls.Add(button);
        }

        private bool IsInRole(params string[] roles)
        {
            string currentRole = NormalizeRole(_nhanVien.VaiTro);
            foreach (string role in roles)
            {
                if (currentRole == NormalizeRole(role))
                {
                    return true;
                }
            }

            return false;
        }

        private static string NormalizeRole(string? role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return string.Empty;
            }

            string normalized = role.Trim().Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder(normalized.Length);
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                if (char.IsLetterOrDigit(c))
                {
                    builder.Append(char.ToLowerInvariant(c));
                }
            }

            return builder.ToString();
        }

        private void LoadStats()
        {
            pnlStats.Controls.Clear();
            try
            {
                DataTable stats = _baoCaoBLL.LayThongKeDashboard();
                if (stats.Rows.Count == 0) return;

                DataRow row = stats.Rows[0];
                AddStat("Tổng phòng", row["TongPhong"]);
                AddStat("Phòng trống", row["PhongTrong"]);
                AddStat("Phòng đang ở", row["PhongDangO"]);
                AddStat("Phòng đặt cọc", row["PhongDatCoc"]);
                AddStat("Tổng giường", row["TongGiuong"]);
                AddStat("Giường trống", row["GiuongTrong"]);
                AddStat("Phiếu đăng ký mới", row["PhieuDangKyMoi"]);
                AddStat("Lịch hẹn xem phòng", row["LichHen"]);
                AddStat("Nhân viên đang làm", row["NhanVienDangLam"]);
            }
            catch (Exception ex)
            {
                AddStat("Không tải được thống kê", ex.Message);
            }
        }

        private void AddStat(string title, object value)
        {
            Panel card = new Panel
            {
                Width = 285,
                Height = 96,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 12, 12),
                Padding = new Padding(14)
            };
            Label lblTitle = new Label
            {
                Text = title,
                AutoSize = true,
                ForeColor = UiHelper.Muted
            };
            Label lblValue = new Label
            {
                Text = Convert.ToString(value),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text,
                Top = 34
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            pnlStats.Controls.Add(card);
        }
    }
}

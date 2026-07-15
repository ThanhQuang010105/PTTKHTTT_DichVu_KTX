using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using HomeStayDorm.BLL.TienIch;
using HomeStayDorm.DTO;
using HomeStayDorm.GUI.DatCoc;
using HomeStayDorm.UI;
using HomeStayDorm.UI.DangKyThue;
using HomeStayDorm.UI.QuanTriHeThong;

namespace HomeStayDorm.UI.Common
{
    public class frmDashboard : Form
    {
        private readonly NhanVienDTO _nhanVien;
        private readonly BaoCaoBLL _baoCaoBLL = new BaoCaoBLL();

        // Layout chính: Sidebar trái + Content phải
        private Panel pnlSidebar = new Panel();
        private Panel pnlContent = new Panel();
        private Panel pnlContentArea = new Panel();
        private Label lblPageTitle = new Label();

        // Màu sidebar
        private static readonly Color SidebarBg     = Color.FromArgb(15, 23, 42);
        private static readonly Color SidebarText    = Color.FromArgb(203, 213, 225);
        private static readonly Color SidebarHover   = Color.FromArgb(30, 41, 59);
        private static readonly Color SidebarActive  = Color.FromArgb(37, 99, 235);
        private static readonly Color SidebarSection = Color.FromArgb(71, 85, 105);

        private Button? _activeBtn = null;

        public frmDashboard(NhanVienDTO nhanVien)
        {
            _nhanVien = nhanVien;
            InitializeComponent();
            ShowHomeDashboard();
        }

        private void InitializeComponent()
        {
            Text = "HomeStay Dorm – Hệ thống quản lý";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1100, 680);
            BackColor = UiHelper.Surface;
            Font = new Font("Segoe UI", 10F);

            BuildSidebar();
            BuildContentArea();

            // Root layout
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(pnlSidebar, 0, 0);
            root.Controls.Add(pnlContent, 1, 0);

            Controls.Add(root);
        }

        // ──────────────────────────────────────────────
        //  SIDEBAR
        // ──────────────────────────────────────────────
        private void BuildSidebar()
        {
            pnlSidebar.Dock = DockStyle.Fill;
            pnlSidebar.BackColor = SidebarBg;
            pnlSidebar.Padding = new Padding(0, 0, 0, 0);

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = SidebarBg,
                Padding = new Padding(0, 0, 0, 0)
            };

            // Logo / App name
            Panel logo = new Panel { Width = 230, Height = 72, BackColor = Color.FromArgb(9, 14, 28) };
            Label lblApp = new Label
            {
                Text = "🏠 HomeStay Dorm",
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 230,
                Height = 72,
                TextAlign = ContentAlignment.MiddleCenter
            };
            logo.Controls.Add(lblApp);
            flow.Controls.Add(logo);

            // User info
            Panel userInfo = new Panel { Width = 230, Height = 68, BackColor = Color.FromArgb(20, 30, 55), Padding = new Padding(14, 10, 14, 10) };
            Label lblUserName = new Label
            {
                Text = _nhanVien.HoTen,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 202, Height = 24,
                Location = new Point(14, 10)
            };
            string roleDisplay = GetRoleDisplay(_nhanVien.VaiTro);
            Label lblRole = new Label
            {
                Text = "👤 " + roleDisplay,
                ForeColor = SidebarSection,
                AutoSize = false,
                Width = 202, Height = 22,
                Location = new Point(14, 36)
            };
            userInfo.Controls.Add(lblUserName);
            userInfo.Controls.Add(lblRole);
            flow.Controls.Add(userInfo);

            // Separator
            flow.Controls.Add(MakeSeparator());

            string role = NormalizeRole(_nhanVien.VaiTro);
            bool isNvPhuTach = role == "nvphutrach" || role == "nhanvienphutrach";

            // ── SALE ──
            if (role == "sale")
            {
                flow.Controls.Add(MakeSectionLabel("ĐĂNG KÝ THUÊ"));
                flow.Controls.Add(MakeMenuBtn("📋  Đăng ký thuê phòng",       () => OpenForm(new frmDangKyThue(), "Đăng ký thuê phòng")));
                flow.Controls.Add(MakeMenuBtn("🔍  Kiểm tra tình trạng phòng",() => OpenForm(new frmTraCuuPhong(), "Tra cứu phòng trống")));
                flow.Controls.Add(MakeMenuBtn("📅  Lập lịch xem phòng",        () => OpenForm(new frmLichXemPhong(), "Lịch xem phòng")));
                flow.Controls.Add(MakeSeparator());

                flow.Controls.Add(MakeSectionLabel("KHÁCH HÀNG & ĐẶT CỌC"));
                flow.Controls.Add(MakeMenuBtn("👤  Thông tin khách hàng",      () => OpenForm(new frmThongTinKhachHang(), "Thông tin khách hàng")));
                flow.Controls.Add(MakeMenuBtn("💰  Tra cứu đặt cọc",           () => OpenForm(new FrmTraCuuDatCoc(), "Tra cứu phiếu đặt cọc")));
                flow.Controls.Add(MakeSeparator());

                flow.Controls.Add(MakeSectionLabel("TRẢ PHÒNG"));
                flow.Controls.Add(MakeMenuBtn("🚪  Tạo yêu cầu trả phòng",    () => OpenForm(new frmTaoYeuCauTraPhong(), "Tạo yêu cầu trả phòng")));
                flow.Controls.Add(MakeSeparator());
            }

            // ── QUẢN LÝ ──
            if (role == "quanly")
            {
                flow.Controls.Add(MakeSectionLabel("NHẬN PHÒNG & BÀN GIAO"));
                flow.Controls.Add(MakeMenuBtn("✅  Phê duyệt điều kiện lưu trú", () => ShowComingSoon("Phê duyệt điều kiện lưu trú (UC17)")));
                flow.Controls.Add(MakeMenuBtn("🤝  Lập biên bản bàn giao",      () => ShowComingSoon("Lập biên bản bàn giao (UC18)")));
                flow.Controls.Add(MakeMenuBtn("📑  Đối chiếu & xác nhận chứng từ", () => ShowComingSoon("Đối chiếu & xác nhận chứng từ (UC16)")));
                flow.Controls.Add(MakeSeparator());

                flow.Controls.Add(MakeSectionLabel("QUẢN TRỊ HỆ THỐNG"));
                flow.Controls.Add(MakeMenuBtn("🔍  Kiểm tra tình trạng phòng/giường", () => OpenForm(new frmTraCuuPhong(), "Kiểm tra tình trạng phòng/giường")));
                flow.Controls.Add(MakeMenuBtn("🏢  Quản lý phòng/giường",       () => OpenForm(new frmQuanLyPhong(), "Quản lý phòng/giường")));
                flow.Controls.Add(MakeMenuBtn("🏪  Quản lý chi nhánh",           () => OpenForm(new frmQuanLyChiNhanh(), "Quản lý chi nhánh")));
                flow.Controls.Add(MakeMenuBtn("👥  Quản lý nhân viên",           () => OpenForm(new frmQuanLyNhanVien(), "Quản lý nhân viên")));
                flow.Controls.Add(MakeMenuBtn("📊  Báo cáo thống kê",            () => ShowComingSoon("Báo cáo thống kê")));
                flow.Controls.Add(MakeSeparator());
            }

            // ── KẾ TOÁN ──
            if (role == "ketoan")
            {
                flow.Controls.Add(MakeSectionLabel("THANH TOÁN CỌC"));
                flow.Controls.Add(MakeMenuBtn("💵  Lập yêu cầu thanh toán cọc", () => ShowComingSoon("Lập yêu cầu thanh toán cọc (UC10)")));
                flow.Controls.Add(MakeMenuBtn("🧾  Lập phiếu thu phí nhận phòng",() => ShowComingSoon("Lập phiếu thu phí nhận phòng (UC11)")));
                flow.Controls.Add(MakeMenuBtn("✔️  Xác nhận thanh toán",         () => ShowComingSoon("Xác nhận thanh toán (UC12)")));
                flow.Controls.Add(MakeSeparator());

                flow.Controls.Add(MakeSectionLabel("ĐỐI SOÁT CỌC"));
                flow.Controls.Add(MakeMenuBtn("⚖️  Lập bảng đối soát cọc",      () => OpenForm(new frmDoiSoatCoc(), "Đối soát cọc")));
                flow.Controls.Add(MakeSeparator());
            }

            // ── NHÂN VIÊN PHỤ TRÁCH ──
            if (isNvPhuTach)
            {
                flow.Controls.Add(MakeSectionLabel("HỢP ĐỒNG THUÊ"));
                flow.Controls.Add(MakeMenuBtn("📄  Lập hợp đồng thuê",           () => OpenForm(new HomeStayDorm.GUI.frmHopDongThue(), "Lập hợp đồng thuê (UC19)")));
                flow.Controls.Add(MakeMenuBtn("📃  Thanh lý hợp đồng",           () => ShowComingSoon("Thanh lý hợp đồng (UC20)")));
                flow.Controls.Add(MakeSeparator());
            }

            // Dashboard – ẩn với kế toán
            if (role != "ketoan")
            {
                flow.Controls.Add(MakeSectionLabel("TỔNG QUAN"));
                Button btnDashboard = MakeMenuBtn("🏠  Dashboard", () =>
                {
                    lblPageTitle.Text = "Dashboard – Tổng quan hệ thống";
                    ShowHomeDashboard();
                });
                flow.Controls.Add(btnDashboard);
                flow.Controls.Add(MakeSeparator());
            }

            // Đăng xuất
            Button btnLogout = MakeMenuBtn("🚪  Đăng xuất", null!);
            btnLogout.ForeColor = Color.FromArgb(252, 165, 165);
            btnLogout.Click += (_, _) =>
            {
                if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SessionContext.CurrentUser = null;
                    Close();
                }
            };
            flow.Controls.Add(btnLogout);

            pnlSidebar.Controls.Add(flow);
        }

        // ──────────────────────────────────────────────
        //  CONTENT AREA (phía phải)
        // ──────────────────────────────────────────────
        private void BuildContentArea()
        {
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = UiHelper.Surface;

            // Top bar
            Panel topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 54,
                BackColor = Color.White,
                Padding = new Padding(20, 0, 20, 0)
            };
            topBar.Paint += (_, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(226, 232, 240), 1),
                    0, topBar.Height - 1, topBar.Width, topBar.Height - 1);
            };

            lblPageTitle.AutoSize = false;
            lblPageTitle.Dock = DockStyle.Fill;
            lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblPageTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblPageTitle.ForeColor = UiHelper.Text;
            lblPageTitle.Text = "Dashboard – Tổng quan hệ thống";

            // Ngày giờ bên phải topbar
            Label lblDateTime = new Label
            {
                Dock = DockStyle.Right,
                AutoSize = false,
                Width = 200,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = UiHelper.Muted
            };
            System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
            clock.Tick += (_, _) => lblDateTime.Text = DateTime.Now.ToString("HH:mm  dd/MM/yyyy");
            lblDateTime.Text = DateTime.Now.ToString("HH:mm  dd/MM/yyyy");

            topBar.Controls.Add(lblPageTitle);
            topBar.Controls.Add(lblDateTime);

            // Content panel (nơi nhúng form con hoặc dashboard)
            pnlContentArea.Dock = DockStyle.Fill;
            pnlContentArea.BackColor = UiHelper.Surface;
            pnlContentArea.Padding = new Padding(20);

            pnlContent.Controls.Add(pnlContentArea);
            pnlContent.Controls.Add(topBar);
        }

        // ──────────────────────────────────────────────
        //  HIỂN THỊ DASHBOARD HOME
        // ──────────────────────────────────────────────
        private void ShowHomeDashboard()
        {
            lblPageTitle.Text = "Dashboard – Tổng quan hệ thống";
            pnlContentArea.Controls.Clear();

            FlowLayoutPanel cards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                BackColor = UiHelper.Surface
            };

            try
            {
                var stats = _baoCaoBLL.LayThongKeDashboard();
                if (stats.Rows.Count > 0)
                {
                    var row = stats.Rows[0];
                    AddStatCard(cards, "Tổng phòng",          "🏢", row["TongPhong"],        Color.FromArgb(37,99,235));
                    AddStatCard(cards, "Phòng trống",          "✅", row["PhongTrong"],        Color.FromArgb(22,163,74));
                    AddStatCard(cards, "Phòng đang ở",         "🏠", row["PhongDangO"],        Color.FromArgb(234,88,12));
                    AddStatCard(cards, "Phòng đặt cọc",        "💰", row["PhongDatCoc"],       Color.FromArgb(124,58,237));
                    AddStatCard(cards, "Tổng giường",          "🛏️", row["TongGiuong"],        Color.FromArgb(37,99,235));
                    AddStatCard(cards, "Giường trống",         "🆓", row["GiuongTrong"],       Color.FromArgb(22,163,74));
                    AddStatCard(cards, "Đăng ký mới",          "📋", row["PhieuDangKyMoi"],    Color.FromArgb(234,88,12));
                    AddStatCard(cards, "Lịch hẹn xem phòng",  "📅", row["LichHen"],           Color.FromArgb(220,38,38));
                    AddStatCard(cards, "Nhân viên đang làm",  "👥", row["NhanVienDangLam"],   Color.FromArgb(71,85,105));
                }
            }
            catch
            {
                Label err = new Label
                {
                    Text = "⚠️ Không thể tải thống kê. Kiểm tra kết nối database.",
                    ForeColor = UiHelper.Danger,
                    AutoSize = true,
                    Margin = new Padding(8)
                };
                cards.Controls.Add(err);
            }

            // Nút làm mới
            Button btnRefresh = UiHelper.Button("↻  Làm mới thống kê");
            btnRefresh.Width = 180;
            btnRefresh.Margin = new Padding(0, 14, 0, 0);
            btnRefresh.Click += (_, _) => ShowHomeDashboard();
            cards.Controls.Add(btnRefresh);

            pnlContentArea.Controls.Add(cards);
        }

        private void AddStatCard(FlowLayoutPanel parent, string title, string icon, object value, Color accent)
        {
            Panel card = new Panel
            {
                Width = 200,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 16, 16),
                Padding = new Padding(16)
            };
            card.Paint += (_, e) =>
            {
                using Pen p = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(p, 0, 0, card.Width - 1, card.Height - 1);
                using SolidBrush bar = new SolidBrush(accent);
                e.Graphics.FillRectangle(bar, 0, 0, 4, card.Height);
            };

            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 18F),
                AutoSize = false,
                Width = 170, Height = 36,
                Location = new Point(16, 10),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Label lblVal = new Label
            {
                Text = Convert.ToString(value) ?? "—",
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = UiHelper.Text,
                AutoSize = false,
                Width = 170, Height = 34,
                Location = new Point(16, 48),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = UiHelper.Muted,
                AutoSize = false,
                Width = 170, Height = 20,
                Location = new Point(16, 86),
                Font = new Font("Segoe UI", 8.5F)
            };
            card.Controls.Add(lblIcon);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblTitle);
            parent.Controls.Add(card);
        }

        // ──────────────────────────────────────────────
        //  MỞ FORM CON NHÚNG VÀO CONTENT AREA
        // ──────────────────────────────────────────────
        private void OpenForm(Form childForm, string title)
        {
            lblPageTitle.Text = title;
            pnlContentArea.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Visible = true;
            pnlContentArea.Controls.Add(childForm);
            childForm.BringToFront();
        }

        private void ShowComingSoon(string feature)
        {
            lblPageTitle.Text = feature + "  (Sẽ hoàn thiện sau)";
            pnlContentArea.Controls.Clear();

            Panel ph = new Panel { Dock = DockStyle.Fill, BackColor = UiHelper.Surface };
            Label lbl = new Label
            {
                Text = $"🚧  Chức năng đang phát triển\n\n\"{feature}\"\n\nChức năng này chưa được cài đặt trong phiên bản hiện tại.",
                Font = new Font("Segoe UI", 14F),
                ForeColor = UiHelper.Muted,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            ph.Controls.Add(lbl);
            pnlContentArea.Controls.Add(ph);
        }

        // ──────────────────────────────────────────────
        //  HELPER: Tạo nút sidebar
        // ──────────────────────────────────────────────
        private Button MakeMenuBtn(string text, Action action)
        {
            Button btn = new Button
            {
                Text = text,
                Width = 230,
                Height = 44,
                FlatStyle = FlatStyle.Flat,
                BackColor = SidebarBg,
                ForeColor = SidebarText,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 8, 0),
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand,
                Margin = Padding.Empty,
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = SidebarHover;
            btn.FlatAppearance.MouseDownBackColor = SidebarActive;

            if (action != null)
            {
                btn.Click += (_, _) =>
                {
                    // Reset màu nút cũ
                    if (_activeBtn != null)
                    {
                        _activeBtn.BackColor = SidebarBg;
                        _activeBtn.ForeColor = SidebarText;
                    }
                    // Đặt nút hiện tại là active
                    btn.BackColor = SidebarActive;
                    btn.ForeColor = Color.White;
                    _activeBtn = btn;
                    action();
                };
            }
            return btn;
        }

        private Label MakeSectionLabel(string text)
        {
            return new Label
            {
                Text = "  " + text,
                Width = 230,
                Height = 30,
                ForeColor = SidebarSection,
                BackColor = SidebarBg,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(10, 0, 0, 2),
                Margin = new Padding(0, 8, 0, 0)
            };
        }

        private Panel MakeSeparator()
        {
            return new Panel
            {
                Width = 230,
                Height = 1,
                BackColor = Color.FromArgb(30, 41, 59),
                Margin = new Padding(0, 4, 0, 4)
            };
        }

        // ──────────────────────────────────────────────
        //  HELPERS: Role normalization
        // ──────────────────────────────────────────────
        private static string NormalizeRole(string? role)
        {
            if (string.IsNullOrWhiteSpace(role)) return string.Empty;
            string norm = role.Trim().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(norm.Length);
            foreach (char c in norm)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
                if (char.IsLetterOrDigit(c)) sb.Append(char.ToLowerInvariant(c));
            }
            return sb.ToString();
        }

        private static string GetRoleDisplay(string? role)
        {
            return NormalizeRole(role) switch
            {
                "sale"            => "Nhân viên Sale",
                "quanly"          => "Quản lý",
                "ketoan"          => "Kế toán",
                "nvphutrach"       => "Nhân viên phụ trách",
                "nhanvienphutrach" => "Nhân viên phụ trách",
                _                 => role ?? "Không xác định"
            };
        }
    }
}

namespace HomeStayDorm.UI
{
    partial class frmTaoYeuCauTraPhong
    {
        private System.ComponentModel.IContainer components = null;

        // ── Controls ──────────────────────────────────────────────────
        // Header
        private System.Windows.Forms.Label       lblTitle;
        private System.Windows.Forms.Label       lblBreadcrumb;
        private System.Windows.Forms.Label       lblNgayHT;
        private System.Windows.Forms.Label       lblNhanVien;

        // Section 1 - Tìm kiếm
        private System.Windows.Forms.GroupBox    grpSearch;
        private System.Windows.Forms.Label       lblSDT, lblCCCD, lblMaHD, lblHoTen;
        private System.Windows.Forms.TextBox     txtSDT, txtCCCD, txtMaHopDong, txtHoTen;
        private System.Windows.Forms.Button      btnTimKiem, btnLamMoi;
        private System.Windows.Forms.Label       lblHint;

        // Section 2 - Thông tin
        private System.Windows.Forms.GroupBox    grpThongTin;
        private System.Windows.Forms.GroupBox    grpKhachThue;
        private System.Windows.Forms.Label       lbl_HoTen, lbl_SDT, lbl_Email, lbl_CCCD, lbl_DiaChi;
        private System.Windows.Forms.Label       valHoTen, valSDT, valEmail, valCCCD, valDiaChi;
        private System.Windows.Forms.GroupBox    grpHopDong;
        private System.Windows.Forms.Label       lbl_MaHD, lbl_MaCoc, lbl_Phong, lbl_Loai;
        private System.Windows.Forms.Label       lbl_NgayBD, lbl_NgayHH, lbl_TrangThai, lbl_Coc;
        private System.Windows.Forms.Label       valMaHD, valMaCoc, valPhong, valLoai;
        private System.Windows.Forms.Label       valNgayBD, valNgayHH, valTrangThai, valCoc;

        // Section 3 - Yêu cầu
        private System.Windows.Forms.GroupBox    grpYeuCau;
        private System.Windows.Forms.Label       lblNgayDuKien, lblLyDo, lblGhiChu;
        private System.Windows.Forms.DateTimePicker dtpNgayDuKien;
        private System.Windows.Forms.ComboBox    cboLyDo;
        private System.Windows.Forms.TextBox     txtGhiChu;
        private System.Windows.Forms.Label       lblCounter;
        private System.Windows.Forms.Panel       pnlLuuY;
        private System.Windows.Forms.Label       lblLuuYTitle, lblLuuYContent;

        // Footer
        private System.Windows.Forms.Button      btnHuyBo, btnTaoYeuCau;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Khởi tạo
            lblTitle        = new System.Windows.Forms.Label();
            lblBreadcrumb   = new System.Windows.Forms.Label();
            lblNgayHT       = new System.Windows.Forms.Label();
            lblNhanVien     = new System.Windows.Forms.Label();
            grpSearch       = new System.Windows.Forms.GroupBox();
            lblSDT          = new System.Windows.Forms.Label();
            lblCCCD         = new System.Windows.Forms.Label();
            lblMaHD         = new System.Windows.Forms.Label();
            lblHoTen        = new System.Windows.Forms.Label();
            txtSDT          = new System.Windows.Forms.TextBox();
            txtCCCD         = new System.Windows.Forms.TextBox();
            txtMaHopDong    = new System.Windows.Forms.TextBox();
            txtHoTen        = new System.Windows.Forms.TextBox();
            btnTimKiem      = new System.Windows.Forms.Button();
            btnLamMoi       = new System.Windows.Forms.Button();
            lblHint         = new System.Windows.Forms.Label();
            grpThongTin     = new System.Windows.Forms.GroupBox();
            grpKhachThue    = new System.Windows.Forms.GroupBox();
            lbl_HoTen       = new System.Windows.Forms.Label();
            lbl_SDT         = new System.Windows.Forms.Label();
            lbl_Email       = new System.Windows.Forms.Label();
            lbl_CCCD        = new System.Windows.Forms.Label();
            lbl_DiaChi      = new System.Windows.Forms.Label();
            valHoTen        = new System.Windows.Forms.Label();
            valSDT          = new System.Windows.Forms.Label();
            valEmail        = new System.Windows.Forms.Label();
            valCCCD         = new System.Windows.Forms.Label();
            valDiaChi       = new System.Windows.Forms.Label();
            grpHopDong      = new System.Windows.Forms.GroupBox();
            lbl_MaHD        = new System.Windows.Forms.Label();
            lbl_MaCoc       = new System.Windows.Forms.Label();
            lbl_Phong       = new System.Windows.Forms.Label();
            lbl_Loai        = new System.Windows.Forms.Label();
            lbl_NgayBD      = new System.Windows.Forms.Label();
            lbl_NgayHH      = new System.Windows.Forms.Label();
            lbl_TrangThai   = new System.Windows.Forms.Label();
            lbl_Coc         = new System.Windows.Forms.Label();
            valMaHD         = new System.Windows.Forms.Label();
            valMaCoc        = new System.Windows.Forms.Label();
            valPhong        = new System.Windows.Forms.Label();
            valLoai         = new System.Windows.Forms.Label();
            valNgayBD       = new System.Windows.Forms.Label();
            valNgayHH       = new System.Windows.Forms.Label();
            valTrangThai    = new System.Windows.Forms.Label();
            valCoc          = new System.Windows.Forms.Label();
            grpYeuCau       = new System.Windows.Forms.GroupBox();
            lblNgayDuKien   = new System.Windows.Forms.Label();
            lblLyDo         = new System.Windows.Forms.Label();
            lblGhiChu       = new System.Windows.Forms.Label();
            dtpNgayDuKien   = new System.Windows.Forms.DateTimePicker();
            cboLyDo         = new System.Windows.Forms.ComboBox();
            txtGhiChu       = new System.Windows.Forms.TextBox();
            lblCounter      = new System.Windows.Forms.Label();
            pnlLuuY         = new System.Windows.Forms.Panel();
            lblLuuYTitle    = new System.Windows.Forms.Label();
            lblLuuYContent  = new System.Windows.Forms.Label();
            btnHuyBo        = new System.Windows.Forms.Button();
            btnTaoYeuCau    = new System.Windows.Forms.Button();

            // ── FORM ─────────────────────────────────────────────────
            this.Text          = "Tạo yêu cầu trả phòng";
            this.Size          = new System.Drawing.Size(1100, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor     = System.Drawing.Color.White;
            this.Font          = new System.Drawing.Font("Segoe UI", 9f);
            this.MinimumSize   = new System.Drawing.Size(1000, 650);

            // ── HEADER ───────────────────────────────────────────────
            lblTitle.Text      = "TẠO YÊU CẦU TRẢ PHÒNG";
            lblTitle.Font      = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            lblTitle.AutoSize  = true;
            lblTitle.Location  = new System.Drawing.Point(20, 15);

            lblBreadcrumb.Text      = "Quản lý trả phòng  /  Tạo yêu cầu trả phòng";
            lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            lblBreadcrumb.AutoSize  = true;
            lblBreadcrumb.Location  = new System.Drawing.Point(23, 45);

            lblNgayHT.Text      = "📅  " + System.DateTime.Today.ToString("dd/MM/yyyy");
            lblNgayHT.AutoSize  = true;
            lblNgayHT.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblNgayHT.Font      = new System.Drawing.Font("Segoe UI", 9.5f);
            lblNgayHT.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblNgayHT.Location  = new System.Drawing.Point(680, 25);

            lblNhanVien.Text      = "👤  Nhân viên: Nguyễn Văn A";
            lblNhanVien.AutoSize  = true;
            lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblNhanVien.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            lblNhanVien.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblNhanVien.Location  = new System.Drawing.Point(820, 25);

            var sepHeader = new System.Windows.Forms.Label();
            sepHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepHeader.Location = new System.Drawing.Point(20, 65);
            sepHeader.Size = new System.Drawing.Size(1040, 2);
            sepHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(sepHeader);

            // ── SECTION 1: TRA CỨU ───────────────────────────────────
            grpSearch.Text      = "1. TRA CỨU THÔNG TIN KHÁCH THUÊ";
            grpSearch.Location  = new System.Drawing.Point(20, 75);
            grpSearch.Size      = new System.Drawing.Size(1040, 90);
            grpSearch.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpSearch.BackColor = System.Drawing.Color.White;
            grpSearch.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Labels
            MkLabel(lblSDT,   "Số điện thoại",  20,  25, grpSearch);
            MkLabel(lblCCCD,  "CCCD/CMND",        200, 25, grpSearch);
            MkLabel(lblMaHD,  "Mã hợp đồng",      380, 25, grpSearch);
            MkLabel(lblHoTen, "Họ tên khách thuê", 560, 25, grpSearch);

            // TextBoxes
            MkTextBox(txtSDT,       20,  45, 160, grpSearch); txtSDT.PlaceholderText       = "Nhập số điện thoại";
            MkTextBox(txtCCCD,     200,  45, 160, grpSearch); txtCCCD.PlaceholderText      = "Nhập CCCD/CMND";
            MkTextBox(txtMaHopDong, 380, 45, 160, grpSearch); txtMaHopDong.PlaceholderText = "Nhập mã hợp đồng";
            MkTextBox(txtHoTen,    560,  45, 160, grpSearch); txtHoTen.PlaceholderText     = "Nhập họ tên";

            // Buttons
            btnTimKiem.Text      = "🔍 Tìm kiếm";
            btnTimKiem.Size      = new System.Drawing.Size(120, 28);
            btnTimKiem.Location  = new System.Drawing.Point(740, 44);
            btnTimKiem.BackColor = System.Drawing.Color.FromArgb(30, 90, 168);
            btnTimKiem.ForeColor = System.Drawing.Color.White;
            btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.Font      = new System.Drawing.Font("Segoe UI", 9f);
            btnTimKiem.Click    += btnTimKiem_Click;

            btnLamMoi.Text      = "↺ Làm mới";
            btnLamMoi.Size      = new System.Drawing.Size(110, 28);
            btnLamMoi.Location  = new System.Drawing.Point(870, 44);
            btnLamMoi.BackColor = System.Drawing.Color.White;
            btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            btnLamMoi.Font      = new System.Drawing.Font("Segoe UI", 9f);
            btnLamMoi.Click    += btnLamMoi_Click;

            lblHint.Text      = "ⓘ  Nhập số điện thoại (bắt buộc) hoặc CCCD/CMND, mã hợp đồng hoặc họ tên để tra cứu. Hệ thống sẽ hiển thị thông tin khách hàng và hợp đồng.";
            lblHint.Location  = new System.Drawing.Point(20, 72);
            lblHint.Size      = new System.Drawing.Size(1000, 18);
            lblHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            lblHint.Font      = new System.Drawing.Font("Segoe UI", 8.5f);

            grpSearch.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblSDT, lblCCCD, lblMaHD, lblHoTen,
                txtSDT, txtCCCD, txtMaHopDong, txtHoTen,
                btnTimKiem, btnLamMoi, lblHint
            });

            var sepSearch = new System.Windows.Forms.Label();
            sepSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepSearch.Location = new System.Drawing.Point(20, 175);
            sepSearch.Size = new System.Drawing.Size(1040, 2);
            sepSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(sepSearch);

            // ── SECTION 2: THÔNG TIN ─────────────────────────────────
            grpThongTin.Text      = "2. THÔNG TIN KHÁCH THUÊ & HỢP ĐỒNG";
            grpThongTin.Location  = new System.Drawing.Point(20, 185);
            grpThongTin.Size      = new System.Drawing.Size(1040, 175);
            grpThongTin.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpThongTin.BackColor = System.Drawing.Color.White;
            grpThongTin.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Khách thuê (Trái)
            grpKhachThue.Text      = "THÔNG TIN KHÁCH THUÊ";
            grpKhachThue.Location  = new System.Drawing.Point(15, 20);
            grpKhachThue.Size      = new System.Drawing.Size(490, 145);
            grpKhachThue.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpKhachThue.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            grpKhachThue.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);

            MkInfoRow(lbl_HoTen,  valHoTen,  "Họ và tên",    15, 30, grpKhachThue);
            MkInfoRow(lbl_SDT,    valSDT,     "Số điện thoại",15, 55, grpKhachThue);
            MkInfoRow(lbl_Email,  valEmail,   "Email",         15, 80, grpKhachThue);
            MkInfoRow(lbl_CCCD,   valCCCD,    "CCCD/CMND",    15,105, grpKhachThue);
            MkInfoRow(lbl_DiaChi, valDiaChi,  "Địa chỉ",      15,130, grpKhachThue);

            grpThongTin.Controls.Add(grpKhachThue);

            // Hợp đồng (Phải)
            grpHopDong.Text      = "THÔNG TIN HỢP ĐỒNG";
            grpHopDong.Location  = new System.Drawing.Point(520, 20);
            grpHopDong.Size      = new System.Drawing.Size(505, 145);
            grpHopDong.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpHopDong.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            grpHopDong.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            grpHopDong.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Cột trái của hợp đồng
            MkInfoRow(lbl_MaHD,      valMaHD,      "Mã hợp đồng",      15, 25, grpHopDong);
            MkInfoRow(lbl_MaCoc,     valMaCoc,      "Mã phiếu đặt cọc", 15, 48, grpHopDong);
            MkInfoRow(lbl_Phong,     valPhong,      "Phòng / Giường",   15, 71, grpHopDong);
            MkInfoRow(lbl_Loai,      valLoai,       "Loại thuê",         15,94, grpHopDong);
            
            // Cột phải của hợp đồng
            MkInfoRow(lbl_NgayBD,    valNgayBD,    "Ngày bắt đầu ở",  260, 25, grpHopDong);
            MkInfoRow(lbl_NgayHH,    valNgayHH,    "Ngày hết hạn HĐ", 260, 48, grpHopDong);
            MkInfoRow(lbl_TrangThai, valTrangThai, "Trạng thái HĐ",   260, 71, grpHopDong);
            MkInfoRow(lbl_Coc,       valCoc,       "Tiền cọc",         260,94, grpHopDong);

            grpThongTin.Controls.Add(grpHopDong);

            var sepInfo = new System.Windows.Forms.Label();
            sepInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepInfo.Location = new System.Drawing.Point(20, 370);
            sepInfo.Size = new System.Drawing.Size(1040, 2);
            sepInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(sepInfo);

            // ── SECTION 3: YÊU CẦU TRẢ PHÒNG ────────────────────────
            grpYeuCau.Text      = "3. THÔNG TIN YÊU CẦU TRẢ PHÒNG";
            grpYeuCau.Location  = new System.Drawing.Point(20, 380);
            grpYeuCau.Size      = new System.Drawing.Size(1040, 185);
            grpYeuCau.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpYeuCau.BackColor = System.Drawing.Color.White;
            grpYeuCau.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Date picker
            lblNgayDuKien.Text     = "Ngày dự kiến trả phòng *";
            lblNgayDuKien.Location = new System.Drawing.Point(15, 30);
            lblNgayDuKien.AutoSize = true;
            lblNgayDuKien.Font     = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);

            dtpNgayDuKien.Location   = new System.Drawing.Point(15, 50);
            dtpNgayDuKien.Size       = new System.Drawing.Size(300, 23);
            dtpNgayDuKien.Format     = System.Windows.Forms.DateTimePickerFormat.Short;

            // Reason combobox
            lblLyDo.Text     = "Lý do trả phòng *";
            lblLyDo.Location = new System.Drawing.Point(340, 30);
            lblLyDo.AutoSize = true;
            lblLyDo.Font     = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);

            cboLyDo.Location      = new System.Drawing.Point(340, 50);
            cboLyDo.Size          = new System.Drawing.Size(350, 23);
            cboLyDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Note textbox
            lblGhiChu.Text     = "Ghi chú";
            lblGhiChu.Location = new System.Drawing.Point(15, 80);
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font     = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);

            txtGhiChu.Location      = new System.Drawing.Point(15, 100);
            txtGhiChu.Size          = new System.Drawing.Size(675, 75);
            txtGhiChu.Multiline     = true;
            txtGhiChu.MaxLength     = 500;
            txtGhiChu.PlaceholderText = "Nhập ghi chú (nếu có)...";
            txtGhiChu.TextChanged  += txtGhiChu_TextChanged;
            txtGhiChu.Anchor        = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            lblCounter.Text      = "0 / 500";
            lblCounter.Location  = new System.Drawing.Point(645, 162);
            lblCounter.AutoSize  = true;
            lblCounter.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            lblCounter.Font      = new System.Drawing.Font("Segoe UI", 8.5f);
            lblCounter.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            // LƯU Ý Sidebar
            pnlLuuY.Location  = new System.Drawing.Point(715, 30);
            pnlLuuY.Size      = new System.Drawing.Size(310, 145);
            pnlLuuY.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            pnlLuuY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlLuuY.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            lblLuuYTitle.Text      = "LƯU Ý";
            lblLuuYTitle.Location  = new System.Drawing.Point(15, 15);
            lblLuuYTitle.AutoSize  = true;
            lblLuuYTitle.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            lblLuuYTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);

            lblLuuYContent.Text =
                "•  Sau khi tạo yêu cầu, hồ sơ sẽ được chuyển\r\n" +
                "   sang bước kiểm tra hiện trạng và đối soát\r\n" +
                "   tài chính.\r\n\r\n" +
                "•  Vui lòng kiểm tra kỹ thông tin trước khi\r\n   lưu.";
            lblLuuYContent.Location  = new System.Drawing.Point(15, 45);
            lblLuuYContent.Size      = new System.Drawing.Size(280, 100);
            lblLuuYContent.Font      = new System.Drawing.Font("Segoe UI", 9f);
            lblLuuYContent.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            pnlLuuY.Controls.AddRange(new System.Windows.Forms.Control[] { lblLuuYTitle, lblLuuYContent });

            grpYeuCau.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblNgayDuKien, dtpNgayDuKien, lblLyDo, cboLyDo,
                lblGhiChu, txtGhiChu, lblCounter, pnlLuuY
            });

            var sepFooter = new System.Windows.Forms.Label();
            sepFooter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepFooter.Location = new System.Drawing.Point(0, 580);
            sepFooter.Size = new System.Drawing.Size(1100, 2);
            sepFooter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(sepFooter);

            // ── FOOTER BUTTONS ────────────────────────────────────────
            btnHuyBo.Text      = "✕  Hủy bỏ";
            btnHuyBo.Size      = new System.Drawing.Size(120, 38);
            btnHuyBo.Location  = new System.Drawing.Point(680, 595);
            btnHuyBo.BackColor = System.Drawing.Color.White;
            btnHuyBo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            btnHuyBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHuyBo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            btnHuyBo.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnHuyBo.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnHuyBo.Click    += btnHuyBo_Click;

            btnTaoYeuCau.Text      = "✓  Tạo yêu cầu trả phòng";
            btnTaoYeuCau.Size      = new System.Drawing.Size(250, 38);
            btnTaoYeuCau.Location  = new System.Drawing.Point(815, 595);
            btnTaoYeuCau.BackColor = System.Drawing.Color.FromArgb(0, 140, 70);
            btnTaoYeuCau.ForeColor = System.Drawing.Color.White;
            btnTaoYeuCau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTaoYeuCau.FlatAppearance.BorderSize = 0;
            btnTaoYeuCau.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnTaoYeuCau.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnTaoYeuCau.Click    += btnTaoYeuCau_Click;

            // ── THÊM VÀO FORM ─────────────────────────────────────────
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, lblBreadcrumb, lblNgayHT, lblNhanVien,
                grpSearch, grpThongTin, grpYeuCau,
                btnHuyBo, btnTaoYeuCau
            });

            this.Load += frmTaoYeuCauTraPhong_Load;
        }

        // ── Helpers ──────────────────────────────────────────────────
        private void MkLabel(System.Windows.Forms.Label lbl, string text, int x, int y, System.Windows.Forms.Control parent)
        {
            lbl.Text      = text;
            lbl.Location  = new System.Drawing.Point(x, y);
            lbl.AutoSize  = true;
            lbl.Font      = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            parent.Controls.Add(lbl);
        }

        private void MkTextBox(System.Windows.Forms.TextBox txt, int x, int y, int w, System.Windows.Forms.Control parent)
        {
            txt.Location  = new System.Drawing.Point(x, y);
            txt.Size      = new System.Drawing.Size(w, 23);
            txt.Font      = new System.Drawing.Font("Segoe UI", 9f);
            parent.Controls.Add(txt);
        }

        private void MkInfoRow(System.Windows.Forms.Label lblKey, System.Windows.Forms.Label lblVal, string key, int x, int y, System.Windows.Forms.Control parent)
        {
            lblKey.Text      = key;
            lblKey.Location  = new System.Drawing.Point(x, y);
            lblKey.AutoSize  = true;
            lblKey.Font      = new System.Drawing.Font("Segoe UI", 9f);
            lblKey.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            var colon = new System.Windows.Forms.Label();
            colon.Text = ":";
            colon.Location = new System.Drawing.Point(x + 110, y);
            colon.AutoSize = true;
            colon.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            parent.Controls.Add(colon);

            lblVal.Text      = "—";
            lblVal.Location  = new System.Drawing.Point(x + 130, y);
            lblVal.AutoSize  = true;
            lblVal.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblVal.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);

            parent.Controls.Add(lblKey);
            parent.Controls.Add(lblVal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}

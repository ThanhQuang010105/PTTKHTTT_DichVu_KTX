namespace HomeStayDorm.UI
{
    partial class frmDoiSoatCoc
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblUser;

        // Section 1 - Search
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.RadioButton radSDT;
        private System.Windows.Forms.RadioButton radCCCD;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblSearchHint;

        // Section 2 - Info
        private System.Windows.Forms.GroupBox grpInfo;
        // Khach thue
        private System.Windows.Forms.GroupBox grpKhach;
        private System.Windows.Forms.Label valMaKH, valHoTen, valSDT, valCCCD, valEmail;
        // Hop dong
        private System.Windows.Forms.GroupBox grpHD;
        private System.Windows.Forms.Label valMaHD, valPhong, valNgayBD, valNgayDuKien, valNgayTao;
        // Phieu coc
        private System.Windows.Forms.GroupBox grpCoc;
        private System.Windows.Forms.Label valMaPhieu, valNgayCoc, valTienCoc, valHinhThuc, valBoPhan;

        // Section 3 - Finance
        private System.Windows.Forms.GroupBox grpFinance;
        private System.Windows.Forms.DataGridView dgvTinhToan;
        private System.Windows.Forms.DataGridView dgvChiTietHuHong;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblTongHuHong;

        // Footer
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.Button btnTinhLai;
        private System.Windows.Forms.Button btnXacNhan;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Khởi tạo
            lblTitle = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            lblUser = new System.Windows.Forms.Label();
            
            grpSearch = new System.Windows.Forms.GroupBox();
            radSDT = new System.Windows.Forms.RadioButton();
            radCCCD = new System.Windows.Forms.RadioButton();
            txtPhone = new System.Windows.Forms.TextBox();
            txtCCCD = new System.Windows.Forms.TextBox();
            btnTimKiem = new System.Windows.Forms.Button();
            btnLamMoi = new System.Windows.Forms.Button();
            lblSearchHint = new System.Windows.Forms.Label();
            
            grpInfo = new System.Windows.Forms.GroupBox();
            grpKhach = new System.Windows.Forms.GroupBox();
            valMaKH = new System.Windows.Forms.Label(); valHoTen = new System.Windows.Forms.Label(); valSDT = new System.Windows.Forms.Label(); valCCCD = new System.Windows.Forms.Label(); valEmail = new System.Windows.Forms.Label();
            grpHD = new System.Windows.Forms.GroupBox();
            valMaHD = new System.Windows.Forms.Label(); valPhong = new System.Windows.Forms.Label(); valNgayBD = new System.Windows.Forms.Label(); valNgayDuKien = new System.Windows.Forms.Label(); valNgayTao = new System.Windows.Forms.Label();
            grpCoc = new System.Windows.Forms.GroupBox();
            valMaPhieu = new System.Windows.Forms.Label(); valNgayCoc = new System.Windows.Forms.Label(); valTienCoc = new System.Windows.Forms.Label(); valHinhThuc = new System.Windows.Forms.Label(); valBoPhan = new System.Windows.Forms.Label();
            
            grpFinance = new System.Windows.Forms.GroupBox();
            dgvTinhToan = new System.Windows.Forms.DataGridView();
            dgvChiTietHuHong = new System.Windows.Forms.DataGridView();
            lblGhiChu = new System.Windows.Forms.Label();
            txtGhiChu = new System.Windows.Forms.TextBox();
            lblTongHuHong = new System.Windows.Forms.Label();
            
            btnHuyBo = new System.Windows.Forms.Button();
            btnTinhLai = new System.Windows.Forms.Button();
            btnXacNhan = new System.Windows.Forms.Button();

            // FORM
            this.Text = "Đối soát cọc - Deposit Reconciliation";
            this.Size = new System.Drawing.Size(1200, 850);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9f);

            // HEADER
            lblTitle.Text = "ĐỐI SOÁT CỌC";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.AutoSize = true;

            lblDate.Text = "📅 03/06/2026";
            lblDate.Location = new System.Drawing.Point(750, 25);
            lblDate.AutoSize = true;

            lblUser.Text = "👤 Nguyễn Văn A (Kế toán)";
            lblUser.Location = new System.Drawing.Point(900, 25);
            lblUser.AutoSize = true;

            // SECTION 1: SEARCH
            grpSearch.Text = "1. TRA CỨU KHÁCH HÀNG / HỢP ĐỒNG";
            grpSearch.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpSearch.Location = new System.Drawing.Point(20, 60);
            grpSearch.Size = new System.Drawing.Size(1140, 100);
            grpSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            radSDT.Text = "Số điện thoại"; radSDT.Location = new System.Drawing.Point(20, 25); radSDT.Checked = true; radSDT.AutoSize = true;
            radCCCD.Text = "CCCD/CMND"; radCCCD.Location = new System.Drawing.Point(220, 25); radCCCD.AutoSize = true;

            txtPhone.Location = new System.Drawing.Point(20, 50); txtPhone.Size = new System.Drawing.Size(180, 23); txtPhone.PlaceholderText = "Nhập số điện thoại";
            txtCCCD.Location = new System.Drawing.Point(220, 50); txtCCCD.Size = new System.Drawing.Size(180, 23); txtCCCD.PlaceholderText = "Nhập CCCD/CMND";

            btnTimKiem.Text = "🔍 Tìm kiếm"; btnTimKiem.Location = new System.Drawing.Point(420, 48); btnTimKiem.Size = new System.Drawing.Size(120, 28);
            btnTimKiem.BackColor = System.Drawing.Color.FromArgb(30, 90, 168); btnTimKiem.ForeColor = System.Drawing.Color.White; btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            btnLamMoi.Text = "↺ Làm mới"; btnLamMoi.Location = new System.Drawing.Point(550, 48); btnLamMoi.Size = new System.Drawing.Size(110, 28);
            btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            lblSearchHint.Text = "ⓘ Chỉ hiển thị các hợp đồng có trạng thái: Đã kiểm tra hiện trạng";
            lblSearchHint.Location = new System.Drawing.Point(20, 78); lblSearchHint.AutoSize = true; lblSearchHint.Font = new System.Drawing.Font("Segoe UI", 8f); lblSearchHint.ForeColor = System.Drawing.Color.Gray;

            grpSearch.Controls.AddRange(new System.Windows.Forms.Control[] { radSDT, radCCCD, txtPhone, txtCCCD, btnTimKiem, btnLamMoi, lblSearchHint });

            // SECTION 2: INFO
            grpInfo.Text = "2. THÔNG TIN TỔNG QUAN";
            grpInfo.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpInfo.Location = new System.Drawing.Point(20, 170);
            grpInfo.Size = new System.Drawing.Size(1140, 170);
            grpInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Khach
            grpKhach.Text = "Thông tin khách thuê"; grpKhach.Location = new System.Drawing.Point(10, 20); grpKhach.Size = new System.Drawing.Size(360, 140);
            grpKhach.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            MkInfoRow("Mã khách hàng", valMaKH, 10, 25, grpKhach);
            MkInfoRow("Họ và tên", valHoTen, 10, 45, grpKhach);
            MkInfoRow("Số điện thoại", valSDT, 10, 65, grpKhach);
            MkInfoRow("CCCD/CMND", valCCCD, 10, 85, grpKhach);
            MkInfoRow("Email", valEmail, 10, 105, grpKhach);

            // Hop dong
            grpHD.Text = "Thông tin hợp đồng"; grpHD.Location = new System.Drawing.Point(380, 20); grpHD.Size = new System.Drawing.Size(360, 140);
            grpHD.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            MkInfoRow("Mã hợp đồng", valMaHD, 10, 25, grpHD);
            MkInfoRow("Phòng / Giường", valPhong, 10, 45, grpHD);
            MkInfoRow("Ngày bắt đầu ở", valNgayBD, 10, 65, grpHD);
            MkInfoRow("Ngày dự kiến trả", valNgayDuKien, 10, 85, grpHD);
            MkInfoRow("Ngày tạo yc trả", valNgayTao, 10, 105, grpHD);

            // Phieu coc
            grpCoc.Text = "Thông tin phiếu đặt cọc"; grpCoc.Location = new System.Drawing.Point(750, 20); grpCoc.Size = new System.Drawing.Size(380, 140);
            grpCoc.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            MkInfoRow("Mã phiếu cọc", valMaPhieu, 10, 25, grpCoc);
            MkInfoRow("Ngày đặt cọc", valNgayCoc, 10, 45, grpCoc);
            MkInfoRow("Số tiền cọc", valTienCoc, 10, 65, grpCoc);
            MkInfoRow("Hình thức cọc", valHinhThuc, 10, 85, grpCoc);
            MkInfoRow("Bộ phận tạo", valBoPhan, 10, 105, grpCoc);

            grpInfo.Controls.AddRange(new System.Windows.Forms.Control[] { grpKhach, grpHD, grpCoc });

            // SECTION 3: FINANCE
            grpFinance.Text = "3. BẢNG ĐỐI SOÁT TÀI CHÍNH";
            grpFinance.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            grpFinance.Location = new System.Drawing.Point(20, 350);
            grpFinance.Size = new System.Drawing.Size(1140, 390);
            grpFinance.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // DGV Left
            dgvTinhToan.Location = new System.Drawing.Point(10, 25);
            dgvTinhToan.Size = new System.Drawing.Size(650, 350);
            dgvTinhToan.AllowUserToAddRows = false;
            dgvTinhToan.RowHeadersVisible = false;
            dgvTinhToan.BackgroundColor = System.Drawing.Color.White;
            dgvTinhToan.Font = new System.Drawing.Font("Segoe UI", 9f);
            dgvTinhToan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            // DGV Right
            dgvChiTietHuHong.Location = new System.Drawing.Point(680, 25);
            dgvChiTietHuHong.Size = new System.Drawing.Size(450, 150);
            dgvChiTietHuHong.AllowUserToAddRows = false;
            dgvChiTietHuHong.RowHeadersVisible = false;
            dgvChiTietHuHong.BackgroundColor = System.Drawing.Color.White;
            dgvChiTietHuHong.Font = new System.Drawing.Font("Segoe UI", 9f);
            dgvChiTietHuHong.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            lblTongHuHong.Text = "Tổng chi phí hư hỏng: 0 VNĐ";
            lblTongHuHong.Location = new System.Drawing.Point(680, 185);
            lblTongHuHong.AutoSize = true;
            lblTongHuHong.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblTongHuHong.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            lblGhiChu.Text = "Ghi chú"; lblGhiChu.Location = new System.Drawing.Point(680, 220); lblGhiChu.AutoSize = true; lblGhiChu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            txtGhiChu.Location = new System.Drawing.Point(680, 240); txtGhiChu.Size = new System.Drawing.Size(450, 130); txtGhiChu.Multiline = true; txtGhiChu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            txtGhiChu.PlaceholderText = "Nhập ghi chú nếu có...";

            grpFinance.Controls.AddRange(new System.Windows.Forms.Control[] { dgvTinhToan, dgvChiTietHuHong, lblTongHuHong, lblGhiChu, txtGhiChu });

            // FOOTER
            btnHuyBo.Text = "✕ Hủy bỏ"; btnHuyBo.Location = new System.Drawing.Point(780, 755); btnHuyBo.Size = new System.Drawing.Size(110, 35);
            btnHuyBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHuyBo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            
            btnTinhLai.Text = "⚙ Tính lại"; btnTinhLai.Location = new System.Drawing.Point(900, 755); btnTinhLai.Size = new System.Drawing.Size(110, 35);
            btnTinhLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTinhLai.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            btnXacNhan.Text = "✓ Xác nhận đối soát"; btnXacNhan.Location = new System.Drawing.Point(1020, 755); btnXacNhan.Size = new System.Drawing.Size(160, 35);
            btnXacNhan.BackColor = System.Drawing.Color.FromArgb(0, 140, 70); btnXacNhan.ForeColor = System.Drawing.Color.White; btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnXacNhan.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, lblDate, lblUser,
                grpSearch, grpInfo, grpFinance,
                btnHuyBo, btnTinhLai, btnXacNhan
            });

            this.Load += frmDoiSoatCoc_Load;
        }

        private void MkInfoRow(string label, System.Windows.Forms.Label valLbl, int x, int y, System.Windows.Forms.Control parent)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = label;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lbl.ForeColor = System.Drawing.Color.Gray;

            var colon = new System.Windows.Forms.Label();
            colon.Text = ":";
            colon.Location = new System.Drawing.Point(x + 100, y);
            colon.AutoSize = true;
            colon.ForeColor = System.Drawing.Color.Gray;

            valLbl.Text = "---";
            valLbl.Location = new System.Drawing.Point(x + 115, y);
            valLbl.AutoSize = true;
            valLbl.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Regular);
            valLbl.ForeColor = System.Drawing.Color.Black;

            parent.Controls.Add(lbl);
            parent.Controls.Add(colon);
            parent.Controls.Add(valLbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}

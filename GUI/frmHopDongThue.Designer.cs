namespace HomeStayDorm.GUI
{
    partial class frmHopDongThue
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHeaderInfo = new System.Windows.Forms.Label();
            
            // Section 1
            this.grpSection1 = new System.Windows.Forms.GroupBox();
            this.cboHoSoCuTru = new System.Windows.Forms.ComboBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            
            // Section 2
            this.grpSection2 = new System.Windows.Forms.GroupBox();
            this.pnlKhachThue = new System.Windows.Forms.Panel();
            this.lblThongTinKhachTitle = new System.Windows.Forms.Label();
            this.lblMaHoSo = new System.Windows.Forms.Label();
            this.lblTenKhachHang = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            
            this.pnlPhongThue = new System.Windows.Forms.Panel();
            this.lblThongTinPhongTitle = new System.Windows.Forms.Label();
            this.lblTenPhongGiouong = new System.Windows.Forms.Label();
            this.lblDonGiaThue = new System.Windows.Forms.Label();
            this.lblTienCocTuDong = new System.Windows.Forms.Label();
            this.lblMaPhieuCoc = new System.Windows.Forms.Label();

            // Section 3
            this.grpSection3 = new System.Windows.Forms.GroupBox();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayBD = new System.Windows.Forms.Label();
            this.lblNgayHH = new System.Windows.Forms.Label();
            
            this.lblKyThanhToan = new System.Windows.Forms.Label();
            this.rdoKy1Thang = new System.Windows.Forms.RadioButton();
            this.rdoKy3Thang = new System.Windows.Forms.RadioButton();
            this.rdoKy6Thang = new System.Windows.Forms.RadioButton();
            
            this.chkPhiDichVu = new System.Windows.Forms.CheckedListBox();
            this.lblDichVuTitle = new System.Windows.Forms.Label();

            // Bottom Buttons
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnHuyBo = new System.Windows.Forms.Button();
            this.btnTaoHopDong = new System.Windows.Forms.Button();

            this.grpSection1.SuspendLayout();
            this.grpSection2.SuspendLayout();
            this.pnlKhachThue.SuspendLayout();
            this.pnlPhongThue.SuspendLayout();
            this.grpSection3.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // Form Settings
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "MHHopDongThue";
            this.Text = "Lập hợp đồng thuê - Lease Contract Creation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Header Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "LẬP HỢP ĐỒNG THUÊ";

            // Header Info (Date + User)
            this.lblHeaderInfo.AutoSize = true;
            this.lblHeaderInfo.Location = new System.Drawing.Point(700, 28);
            this.lblHeaderInfo.Text = "📅 " + System.DateTime.Now.ToString("dd/MM/yyyy") + "   👤 Nhân viên Sale";

            // --- SECTION 1: CHỌN HỒ SƠ ---
            this.grpSection1.Text = "1. CHỌN HỒ SƠ CƯ TRÚ (ĐÃ DUYỆT)";
            this.grpSection1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSection1.ForeColor = System.Drawing.Color.DimGray;
            this.grpSection1.Location = new System.Drawing.Point(20, 70);
            this.grpSection1.Size = new System.Drawing.Size(900, 80);
            
            this.cboHoSoCuTru.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHoSoCuTru.Location = new System.Drawing.Point(20, 35);
            this.cboHoSoCuTru.Size = new System.Drawing.Size(300, 25);
            this.cboHoSoCuTru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location = new System.Drawing.Point(340, 33);
            this.btnLamMoi.Size = new System.Drawing.Size(90, 30);

            this.grpSection1.Controls.Add(this.cboHoSoCuTru);
            this.grpSection1.Controls.Add(this.btnLamMoi);

            // --- SECTION 2: THÔNG TIN TỔNG QUAN ---
            this.grpSection2.Text = "2. THÔNG TIN TỔNG QUAN";
            this.grpSection2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSection2.ForeColor = System.Drawing.Color.DimGray;
            this.grpSection2.Location = new System.Drawing.Point(20, 170);
            this.grpSection2.Size = new System.Drawing.Size(900, 160);

            // Panel Khách thuê
            this.pnlKhachThue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKhachThue.Location = new System.Drawing.Point(20, 30);
            this.pnlKhachThue.Size = new System.Drawing.Size(420, 110);
            
            this.lblThongTinKhachTitle.Text = "Thông tin khách thuê";
            this.lblThongTinKhachTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblThongTinKhachTitle.Location = new System.Drawing.Point(10, 10);
            
            this.lblMaHoSo.Text = "Mã hồ sơ / cư trú  : ---";
            this.lblMaHoSo.Location = new System.Drawing.Point(10, 40);
            this.lblMaHoSo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMaHoSo.AutoSize = true;

            this.lblTenKhachHang.Text = "Họ và tên                : ---";
            this.lblTenKhachHang.Location = new System.Drawing.Point(10, 65);
            this.lblTenKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTenKhachHang.AutoSize = true;

            this.lblCCCD.Text = "CCCD/CMND         : ---";
            this.lblCCCD.Location = new System.Drawing.Point(10, 90);
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCCCD.AutoSize = true;

            this.pnlKhachThue.Controls.Add(this.lblThongTinKhachTitle);
            this.pnlKhachThue.Controls.Add(this.lblMaHoSo);
            this.pnlKhachThue.Controls.Add(this.lblTenKhachHang);
            this.pnlKhachThue.Controls.Add(this.lblCCCD);

            // Panel Phòng & Cọc
            this.pnlPhongThue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPhongThue.Location = new System.Drawing.Point(460, 30);
            this.pnlPhongThue.Size = new System.Drawing.Size(420, 110);

            this.lblThongTinPhongTitle.Text = "Thông tin phòng & Cọc (Tự động)";
            this.lblThongTinPhongTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblThongTinPhongTitle.Location = new System.Drawing.Point(10, 10);
            this.lblThongTinPhongTitle.AutoSize = true;

            this.lblTenPhongGiouong.Text = "Phòng thuê            : ---";
            this.lblTenPhongGiouong.Location = new System.Drawing.Point(10, 40);
            this.lblTenPhongGiouong.AutoSize = true;

            this.lblDonGiaThue.Text = "Đơn giá thuê         : ---";
            this.lblDonGiaThue.Location = new System.Drawing.Point(10, 65);
            this.lblDonGiaThue.AutoSize = true;

            this.lblTienCocTuDong.Text = "Tiền cọc đã thu     : ---";
            this.lblTienCocTuDong.Location = new System.Drawing.Point(220, 40);
            this.lblTienCocTuDong.AutoSize = true;

            this.lblMaPhieuCoc.Text = "Mã phiếu cọc        : ---";
            this.lblMaPhieuCoc.Location = new System.Drawing.Point(220, 65);
            this.lblMaPhieuCoc.AutoSize = true;

            this.pnlPhongThue.Controls.Add(this.lblThongTinPhongTitle);
            this.pnlPhongThue.Controls.Add(this.lblTenPhongGiouong);
            this.pnlPhongThue.Controls.Add(this.lblDonGiaThue);
            this.pnlPhongThue.Controls.Add(this.lblTienCocTuDong);
            this.pnlPhongThue.Controls.Add(this.lblMaPhieuCoc);

            this.grpSection2.Controls.Add(this.pnlKhachThue);
            this.grpSection2.Controls.Add(this.pnlPhongThue);

            // --- SECTION 3: CẤU HÌNH CHI TIẾT ---
            this.grpSection3.Text = "3. CẤU HÌNH HỢP ĐỒNG & DỊCH VỤ";
            this.grpSection3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSection3.ForeColor = System.Drawing.Color.DimGray;
            this.grpSection3.Location = new System.Drawing.Point(20, 350);
            this.grpSection3.Size = new System.Drawing.Size(900, 230);

            this.lblNgayBD.Text = "Ngày bắt đầu:";
            this.lblNgayBD.Location = new System.Drawing.Point(20, 40);
            this.lblNgayBD.AutoSize = true;
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(120, 37);
            this.dtpNgayBatDau.Size = new System.Drawing.Size(150, 25);
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            this.lblNgayHH.Text = "Ngày hết hạn:";
            this.lblNgayHH.Location = new System.Drawing.Point(320, 40);
            this.lblNgayHH.AutoSize = true;
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(420, 37);
            this.dtpNgayHetHan.Size = new System.Drawing.Size(150, 25);
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            this.lblKyThanhToan.Text = "Kỳ thanh toán:";
            this.lblKyThanhToan.Location = new System.Drawing.Point(20, 85);
            this.lblKyThanhToan.AutoSize = true;
            
            this.rdoKy1Thang.Text = "1 Tháng";
            this.rdoKy1Thang.Location = new System.Drawing.Point(140, 83);
            this.rdoKy1Thang.AutoSize = true;
            this.rdoKy1Thang.Checked = true;

            this.rdoKy3Thang.Text = "3 Tháng";
            this.rdoKy3Thang.Location = new System.Drawing.Point(240, 83);
            this.rdoKy3Thang.AutoSize = true;

            this.rdoKy6Thang.Text = "6 Tháng";
            this.rdoKy6Thang.Location = new System.Drawing.Point(340, 83);
            this.rdoKy6Thang.AutoSize = true;

            this.lblDichVuTitle.Text = "Dịch vụ đi kèm (Check chọn):";
            this.lblDichVuTitle.Location = new System.Drawing.Point(20, 130);
            this.lblDichVuTitle.AutoSize = true;

            this.chkPhiDichVu.Location = new System.Drawing.Point(20, 155);
            this.chkPhiDichVu.Size = new System.Drawing.Size(850, 60);
            this.chkPhiDichVu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkPhiDichVu.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPhiDichVu.CheckOnClick = true;

            this.grpSection3.Controls.Add(this.lblNgayBD);
            this.grpSection3.Controls.Add(this.dtpNgayBatDau);
            this.grpSection3.Controls.Add(this.lblNgayHH);
            this.grpSection3.Controls.Add(this.dtpNgayHetHan);
            this.grpSection3.Controls.Add(this.lblKyThanhToan);
            this.grpSection3.Controls.Add(this.rdoKy1Thang);
            this.grpSection3.Controls.Add(this.rdoKy3Thang);
            this.grpSection3.Controls.Add(this.rdoKy6Thang);
            this.grpSection3.Controls.Add(this.lblDichVuTitle);
            this.grpSection3.Controls.Add(this.chkPhiDichVu);

            // --- BOTTOM PANEL (BUTTONS) ---
            this.pnlBottom.Location = new System.Drawing.Point(20, 600);
            this.pnlBottom.Size = new System.Drawing.Size(900, 50);

            this.btnHuyBo.Text = "✕ Hủy bỏ";
            this.btnHuyBo.BackColor = System.Drawing.Color.White;
            this.btnHuyBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyBo.Location = new System.Drawing.Point(600, 5);
            this.btnHuyBo.Size = new System.Drawing.Size(100, 40);

            this.btnTaoHopDong.Text = "✓ Xác nhận & Tạo HĐ";
            this.btnTaoHopDong.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnTaoHopDong.ForeColor = System.Drawing.Color.White;
            this.btnTaoHopDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoHopDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoHopDong.Location = new System.Drawing.Point(710, 5);
            this.btnTaoHopDong.Size = new System.Drawing.Size(180, 40);

            this.pnlBottom.Controls.Add(this.btnHuyBo);
            this.pnlBottom.Controls.Add(this.btnTaoHopDong);

            // Add all to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblHeaderInfo);
            this.Controls.Add(this.grpSection1);
            this.Controls.Add(this.grpSection2);
            this.Controls.Add(this.grpSection3);
            this.Controls.Add(this.pnlBottom);

            this.grpSection1.ResumeLayout(false);
            this.grpSection2.ResumeLayout(false);
            this.pnlKhachThue.ResumeLayout(false);
            this.pnlKhachThue.PerformLayout();
            this.pnlPhongThue.ResumeLayout(false);
            this.pnlPhongThue.PerformLayout();
            this.grpSection3.ResumeLayout(false);
            this.grpSection3.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHeaderInfo;
        
        private System.Windows.Forms.GroupBox grpSection1;
        private System.Windows.Forms.ComboBox cboHoSoCuTru;
        private System.Windows.Forms.Button btnLamMoi;

        private System.Windows.Forms.GroupBox grpSection2;
        private System.Windows.Forms.Panel pnlKhachThue;
        private System.Windows.Forms.Label lblThongTinKhachTitle;
        private System.Windows.Forms.Label lblMaHoSo;
        private System.Windows.Forms.Label lblTenKhachHang;
        private System.Windows.Forms.Label lblCCCD;
        
        private System.Windows.Forms.Panel pnlPhongThue;
        private System.Windows.Forms.Label lblThongTinPhongTitle;
        private System.Windows.Forms.Label lblTenPhongGiouong;
        private System.Windows.Forms.Label lblDonGiaThue;
        private System.Windows.Forms.Label lblTienCocTuDong;
        private System.Windows.Forms.Label lblMaPhieuCoc;

        private System.Windows.Forms.GroupBox grpSection3;
        private System.Windows.Forms.Label lblNgayBD;
        private System.Windows.Forms.Label lblNgayHH;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.Label lblKyThanhToan;
        private System.Windows.Forms.RadioButton rdoKy1Thang;
        private System.Windows.Forms.RadioButton rdoKy3Thang;
        private System.Windows.Forms.RadioButton rdoKy6Thang;
        private System.Windows.Forms.Label lblDichVuTitle;
        private System.Windows.Forms.CheckedListBox chkPhiDichVu;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.Button btnTaoHopDong;
    }
}
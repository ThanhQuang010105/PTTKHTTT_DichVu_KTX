namespace HomeStayDorm.GUI
{
    partial class frmHopDongThue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Phương thức khởi tạo các component giao diện.
        /// Sẽ giải quyết triệt để toàn bộ lỗi CS0103 của bạn.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboPhieuCoc = new System.Windows.Forms.ComboBox();
            this.txtChiNhanh = new System.Windows.Forms.TextBox();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.rdo1Thang = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();

            // 
            // cboPhieuCoc
            // 
            this.cboPhieuCoc.FormattingEnabled = true;
            this.cboPhieuCoc.Location = new System.Drawing.Point(30, 40);
            this.cboPhieuCoc.Name = "cboPhieuCoc";
            this.cboPhieuCoc.Size = new System.Drawing.Size(250, 23);
            this.cboPhieuCoc.SelectedIndexChanged += new System.EventHandler(this.cboPhieuCoc_SelectedIndexChanged);

            // 
            // txtChiNhanh
            // 
            this.txtChiNhanh.Location = new System.Drawing.Point(30, 90);
            this.txtChiNhanh.Name = "txtChiNhanh";
            this.txtChiNhanh.ReadOnly = true;
            this.txtChiNhanh.Size = new System.Drawing.Size(250, 23);

            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Location = new System.Drawing.Point(30, 140);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.ReadOnly = true;
            this.txtGiaThue.Size = new System.Drawing.Size(250, 23);

            // 
            // lblTienCoc
            // 
            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.ForeColor = System.Drawing.Color.Red;
            this.lblTienCoc.Location = new System.Drawing.Point(30, 190);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Size = new System.Drawing.Size(43, 15);
            this.lblTienCoc.Text = "0 VND";

            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Location = new System.Drawing.Point(30, 240);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(250, 23);

            // 
            // rdo1Thang
            // 
            this.rdo1Thang.AutoSize = true;
            this.rdo1Thang.Location = new System.Drawing.Point(30, 290);
            this.rdo1Thang.Name = "rdo1Thang";
            this.rdo1Thang.Size = new System.Drawing.Size(66, 19);
            this.rdo1Thang.TabStop = true;
            this.rdo1Thang.Text = "1 Tháng";
            this.rdo1Thang.UseVisualStyleBackColor = true;

            // 
            // frmHopDongThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.rdo1Thang);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.lblTienCoc);
            this.Controls.Add(this.txtGiaThue);
            this.Controls.Add(this.txtChiNhanh);
            this.Controls.Add(this.cboPhieuCoc);
            this.Name = "frmHopDongThue";
            this.Text = "Lập Hợp Đồng Thuê";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Định nghĩa các biến thành viên (Các nút, ô nhập liệu mà file chính đang tìm kiếm)
        private System.Windows.Forms.ComboBox cboPhieuCoc;
        private System.Windows.Forms.TextBox txtChiNhanh;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.RadioButton rdo1Thang;
    }
}
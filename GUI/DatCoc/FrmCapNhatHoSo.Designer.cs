namespace HomeStayDorm.GUI.DatCoc
{
    partial class FrmCapNhatHoSo
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

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGioiTinhPhong = new System.Windows.Forms.TextBox();
            this.txtSoGiuongCoc = new System.Windows.Forms.TextBox();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.txtMaDatCocGoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtQueQuan = new System.Windows.Forms.TextBox();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvDanhSachThanhVien = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueQuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoa = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSoGiuongDaCoc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSoNguoiDaNhap = new System.Windows.Forms.TextBox();
            this.btnLuuHoSo = new System.Windows.Forms.Button();
            this.btnLuuTamHoSo = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachThanhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGioiTinhPhong);
            this.groupBox1.Controls.Add(this.txtSoGiuongCoc);
            this.groupBox1.Controls.Add(this.txtMaPhong);
            this.groupBox1.Controls.Add(this.txtMaDatCocGoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[THÔNG TIN ĐẶT CỌC]";
            // 
            // txtGioiTinhPhong
            // 
            this.txtGioiTinhPhong.Location = new System.Drawing.Point(150, 140);
            this.txtGioiTinhPhong.Name = "txtGioiTinhPhong";
            this.txtGioiTinhPhong.ReadOnly = true;
            this.txtGioiTinhPhong.Size = new System.Drawing.Size(200, 22);
            this.txtGioiTinhPhong.TabIndex = 7;
            // 
            // txtSoGiuongCoc
            // 
            this.txtSoGiuongCoc.Location = new System.Drawing.Point(150, 105);
            this.txtSoGiuongCoc.Name = "txtSoGiuongCoc";
            this.txtSoGiuongCoc.ReadOnly = true;
            this.txtSoGiuongCoc.Size = new System.Drawing.Size(200, 22);
            this.txtSoGiuongCoc.TabIndex = 6;
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Location = new System.Drawing.Point(150, 70);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.ReadOnly = true;
            this.txtMaPhong.Size = new System.Drawing.Size(200, 22);
            this.txtMaPhong.TabIndex = 5;
            // 
            // txtMaDatCocGoc
            // 
            this.txtMaDatCocGoc.Location = new System.Drawing.Point(150, 35);
            this.txtMaDatCocGoc.Name = "txtMaDatCocGoc";
            this.txtMaDatCocGoc.ReadOnly = true;
            this.txtMaDatCocGoc.Size = new System.Drawing.Size(200, 22);
            this.txtMaDatCocGoc.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Giới tính phòng:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số giường cọc:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã phòng/giường:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phiếu đặt cọc:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Controls.Add(this.txtQueQuan);
            this.groupBox2.Controls.Add(this.cboGioiTinh);
            this.groupBox2.Controls.Add(this.txtSDT);
            this.groupBox2.Controls.Add(this.txtCCCD);
            this.groupBox2.Controls.Add(this.txtHoTen);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 240);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[NHẬP THÔNG TIN THÀNH VIÊN MỚI]";
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThem.Location = new System.Drawing.Point(150, 195);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(200, 30);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm vào danh sách";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtQueQuan
            // 
            this.txtQueQuan.Location = new System.Drawing.Point(150, 160);
            this.txtQueQuan.Name = "txtQueQuan";
            this.txtQueQuan.Size = new System.Drawing.Size(200, 22);
            this.txtQueQuan.TabIndex = 9;
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboGioiTinh.Location = new System.Drawing.Point(150, 125);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(200, 24);
            this.cboGioiTinh.TabIndex = 8;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(150, 95);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(200, 22);
            this.txtSDT.TabIndex = 7;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(150, 65);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(200, 22);
            this.txtCCCD.TabIndex = 6;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(150, 35);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(200, 22);
            this.txtHoTen.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Quê quán (*):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Giới tính (*):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "SĐT (*):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "CCCD (*):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Họ và tên (*):";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnXoa);
            this.groupBox3.Controls.Add(this.dgvDanhSachThanhVien);
            this.groupBox3.Location = new System.Drawing.Point(400, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 438);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[DANH SÁCH THÀNH VIÊN HIỆN TẠI TRONG NHÓM]";
            // 
            // dgvDanhSachThanhVien
            // 
            this.dgvDanhSachThanhVien.AllowUserToAddRows = false;
            this.dgvDanhSachThanhVien.AllowUserToDeleteRows = false;
            this.dgvDanhSachThanhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachThanhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachThanhVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.HoTen,
            this.CCCD,
            this.GioiTinh,
            this.QueQuan,
            this.SDT});
            this.dgvDanhSachThanhVien.Location = new System.Drawing.Point(10, 25);
            this.dgvDanhSachThanhVien.MultiSelect = false;
            this.dgvDanhSachThanhVien.Name = "dgvDanhSachThanhVien";
            this.dgvDanhSachThanhVien.ReadOnly = true;
            this.dgvDanhSachThanhVien.RowHeadersWidth = 51;
            this.dgvDanhSachThanhVien.RowTemplate.Height = 24;
            this.dgvDanhSachThanhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachThanhVien.Size = new System.Drawing.Size(540, 360);
            this.dgvDanhSachThanhVien.TabIndex = 0;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.Width = 40;
            // 
            // HoTen
            // 
            this.HoTen.DataPropertyName = "hoTen";
            this.HoTen.HeaderText = "Họ và tên";
            this.HoTen.Name = "HoTen";
            this.HoTen.ReadOnly = true;
            // 
            // CCCD
            // 
            this.CCCD.DataPropertyName = "cccd";
            this.CCCD.HeaderText = "Số CCCD";
            this.CCCD.Name = "CCCD";
            this.CCCD.ReadOnly = true;
            // 
            // GioiTinh
            // 
            this.GioiTinh.DataPropertyName = "gioiTinh";
            this.GioiTinh.HeaderText = "Giới tính";
            this.GioiTinh.Name = "GioiTinh";
            this.GioiTinh.ReadOnly = true;
            // 
            // QueQuan
            // 
            this.QueQuan.DataPropertyName = "queQuan";
            this.QueQuan.HeaderText = "Quê Quán";
            this.QueQuan.Name = "QueQuan";
            this.QueQuan.ReadOnly = true;
            this.QueQuan.Visible = false;
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "sdt";
            this.SDT.HeaderText = "SĐT";
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            this.SDT.Visible = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.IndianRed;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(450, 395);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 30);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(407, 473);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Số giường đã cọc:";
            // 
            // txtSoGiuongDaCoc
            // 
            this.txtSoGiuongDaCoc.Location = new System.Drawing.Point(517, 470);
            this.txtSoGiuongDaCoc.Name = "txtSoGiuongDaCoc";
            this.txtSoGiuongDaCoc.ReadOnly = true;
            this.txtSoGiuongDaCoc.Size = new System.Drawing.Size(50, 22);
            this.txtSoGiuongDaCoc.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(590, 473);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 5;
            this.label11.Text = "Số người đã nhập:";
            // 
            // txtSoNguoiDaNhap
            // 
            this.txtSoNguoiDaNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoNguoiDaNhap.Location = new System.Drawing.Point(713, 470);
            this.txtSoNguoiDaNhap.Name = "txtSoNguoiDaNhap";
            this.txtSoNguoiDaNhap.ReadOnly = true;
            this.txtSoNguoiDaNhap.Size = new System.Drawing.Size(50, 24);
            this.txtSoNguoiDaNhap.TabIndex = 6;
            // 
            // btnLuuHoSo
            // 
            this.btnLuuHoSo.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnLuuHoSo.ForeColor = System.Drawing.Color.White;
            this.btnLuuHoSo.Location = new System.Drawing.Point(560, 520);
            this.btnLuuHoSo.Name = "btnLuuHoSo";
            this.btnLuuHoSo.Size = new System.Drawing.Size(120, 35);
            this.btnLuuHoSo.TabIndex = 7;
            this.btnLuuHoSo.Text = "Gửi hồ sơ";
            this.btnLuuHoSo.UseVisualStyleBackColor = false;
            this.btnLuuHoSo.Click += new System.EventHandler(this.btnLuuHoSo_Click);
            // 
            // btnLuuTamHoSo
            // 
            this.btnLuuTamHoSo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnLuuTamHoSo.Location = new System.Drawing.Point(690, 520);
            this.btnLuuTamHoSo.Name = "btnLuuTamHoSo";
            this.btnLuuTamHoSo.Size = new System.Drawing.Size(120, 35);
            this.btnLuuTamHoSo.TabIndex = 8;
            this.btnLuuTamHoSo.Text = "Lưu tạm hồ sơ";
            this.btnLuuTamHoSo.UseVisualStyleBackColor = false;
            this.btnLuuTamHoSo.Click += new System.EventHandler(this.btnLuuTamHoSo_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(820, 520);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 9;
            this.btnDong.Text = "Hủy bỏ";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmCapNhatHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 580);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLuuTamHoSo);
            this.Controls.Add(this.btnLuuHoSo);
            this.Controls.Add(this.txtSoNguoiDaNhap);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSoGiuongDaCoc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCapNhatHoSo";
            this.Text = "CẬP NHẬT HỒ SƠ LƯU TRÚ NHÓM THÀNH VIÊN";
            this.Load += new System.EventHandler(this.FrmCapNhatHoSo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachThanhVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGioiTinhPhong;
        private System.Windows.Forms.TextBox txtSoGiuongCoc;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.TextBox txtMaDatCocGoc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQueQuan;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvDanhSachThanhVien;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSoGiuongDaCoc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSoNguoiDaNhap;
        private System.Windows.Forms.Button btnLuuHoSo;
        private System.Windows.Forms.Button btnLuuTamHoSo;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn QueQuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
    }
}

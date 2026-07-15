namespace HomeStayDorm.GUI.DatCoc
{
    partial class FrmTraCuuDatCoc
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
            groupBox1 = new GroupBox();
            btnTimKiem = new Button();
            btnLamMoi = new Button();
            cboTrangThai = new ComboBox();
            mskCCCD = new MaskedTextBox();
            txtSDT = new TextBox();
            txtMaDatCoc = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dgvDanhSachPhieuCoc = new DataGridView();
            STT = new DataGridViewTextBoxColumn();
            maPhieuCoc = new DataGridViewTextBoxColumn();
            KhachDaiDien = new DataGridViewTextBoxColumn();
            soDienThoai = new DataGridViewTextBoxColumn();
            PhongGiuongCoc = new DataGridViewTextBoxColumn();
            SoGiuongDat = new DataGridViewTextBoxColumn();
            soTienCoc = new DataGridViewTextBoxColumn();
            trangThai = new DataGridViewTextBoxColumn();
            btnXemChiTietLapHoSo = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachPhieuCoc).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnTimKiem);
            groupBox1.Controls.Add(btnLamMoi);
            groupBox1.Controls.Add(cboTrangThai);
            groupBox1.Controls.Add(mskCCCD);
            groupBox1.Controls.Add(txtSDT);
            groupBox1.Controls.Add(txtMaDatCoc);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(10, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(831, 112);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "[KHUNG TÌM KIẾM VÀ BỘ LỌC]";
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(656, 70);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(105, 28);
            btnTimKiem.TabIndex = 9;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(656, 23);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(105, 28);
            btnLamMoi.TabIndex = 8;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Chờ bổ sung", "Chờ phê duyệt", "Đã duyệt", "Đã hủy / Quá hạn" });
            cboTrangThai.Location = new Point(420, 75);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(176, 23);
            cboTrangThai.TabIndex = 7;
            // 
            // mskCCCD
            // 
            mskCCCD.Location = new Point(131, 75);
            mskCCCD.Name = "mskCCCD";
            mskCCCD.Size = new Size(176, 23);
            mskCCCD.TabIndex = 6;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(420, 28);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(176, 23);
            txtSDT.TabIndex = 5;
            // 
            // txtMaDatCoc
            // 
            txtMaDatCoc.Location = new Point(131, 28);
            txtMaDatCoc.Name = "txtMaDatCoc";
            txtMaDatCoc.Size = new Size(176, 23);
            txtMaDatCoc.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(324, 78);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 3;
            label4.Text = "Trạng thái phiếu:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(324, 31);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 2;
            label3.Text = "Số điện thoại:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 78);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "CCCD:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 31);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 0;
            label1.Text = "Mã phiếu đặt cọc:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDanhSachPhieuCoc);
            groupBox2.Location = new Point(10, 131);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(831, 328);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "[DANH SÁCH PHIẾU ĐẶT CỌC]";
            // 
            // dgvDanhSachPhieuCoc
            // 
            dgvDanhSachPhieuCoc.AllowUserToAddRows = false;
            dgvDanhSachPhieuCoc.AllowUserToDeleteRows = false;
            dgvDanhSachPhieuCoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachPhieuCoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSachPhieuCoc.Columns.AddRange(new DataGridViewColumn[] { STT, maPhieuCoc, KhachDaiDien, soDienThoai, PhongGiuongCoc, SoGiuongDat, soTienCoc, trangThai });
            dgvDanhSachPhieuCoc.Location = new Point(9, 23);
            dgvDanhSachPhieuCoc.MultiSelect = false;
            dgvDanhSachPhieuCoc.Name = "dgvDanhSachPhieuCoc";
            dgvDanhSachPhieuCoc.ReadOnly = true;
            dgvDanhSachPhieuCoc.RowHeadersWidth = 51;
            dgvDanhSachPhieuCoc.RowTemplate.Height = 24;
            dgvDanhSachPhieuCoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachPhieuCoc.Size = new Size(814, 291);
            dgvDanhSachPhieuCoc.TabIndex = 0;
            // 
            // STT
            // 
            STT.DataPropertyName = "STT";
            STT.HeaderText = "STT";
            STT.Name = "STT";
            STT.ReadOnly = true;
            // 
            // maPhieuCoc
            // 
            maPhieuCoc.DataPropertyName = "maPhieuCoc";
            maPhieuCoc.HeaderText = "Mã đặt cọc";
            maPhieuCoc.Name = "maPhieuCoc";
            maPhieuCoc.ReadOnly = true;
            // 
            // KhachDaiDien
            // 
            KhachDaiDien.DataPropertyName = "KhachDaiDien";
            KhachDaiDien.HeaderText = "Khách đại diện";
            KhachDaiDien.Name = "KhachDaiDien";
            KhachDaiDien.ReadOnly = true;
            // 
            // soDienThoai
            // 
            soDienThoai.DataPropertyName = "soDienThoai";
            soDienThoai.HeaderText = "Số điện thoại";
            soDienThoai.Name = "soDienThoai";
            soDienThoai.ReadOnly = true;
            // 
            // PhongGiuongCoc
            // 
            PhongGiuongCoc.DataPropertyName = "PhongGiuongCoc";
            PhongGiuongCoc.HeaderText = "Phòng/Giường cọc";
            PhongGiuongCoc.Name = "PhongGiuongCoc";
            PhongGiuongCoc.ReadOnly = true;
            // 
            // SoGiuongDat
            // 
            SoGiuongDat.DataPropertyName = "SoGiuongDat";
            SoGiuongDat.HeaderText = "Số giường đặt";
            SoGiuongDat.Name = "SoGiuongDat";
            SoGiuongDat.ReadOnly = true;
            // 
            // soTienCoc
            // 
            soTienCoc.DataPropertyName = "soTienCoc";
            soTienCoc.HeaderText = "Tiền cọc";
            soTienCoc.Name = "soTienCoc";
            soTienCoc.ReadOnly = true;
            // 
            // trangThai
            // 
            trangThai.DataPropertyName = "trangThai";
            trangThai.HeaderText = "Trạng thái";
            trangThai.Name = "trangThai";
            trangThai.ReadOnly = true;
            // 
            // btnXemChiTietLapHoSo
            // 
            btnXemChiTietLapHoSo.Location = new Point(737, 469);
            btnXemChiTietLapHoSo.Name = "btnXemChiTietLapHoSo";
            btnXemChiTietLapHoSo.Size = new Size(105, 28);
            btnXemChiTietLapHoSo.TabIndex = 2;
            btnXemChiTietLapHoSo.Text = "Xem chi tiết";
            btnXemChiTietLapHoSo.UseVisualStyleBackColor = true;
            btnXemChiTietLapHoSo.Click += btnXemChiTietLapHoSo_Click;
            // 
            // FrmTraCuuDatCoc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 511);
            Controls.Add(btnXemChiTietLapHoSo);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmTraCuuDatCoc";
            Text = "TRA CỨU THÔNG TIN ĐẶT CỌC";
            Load += FrmTraCuuDatCoc_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachPhieuCoc).EndInit();
            ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaDatCoc;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.MaskedTextBox mskCCCD;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuCoc;
        private System.Windows.Forms.Button btnXemChiTietLapHoSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPhieuCoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn KhachDaiDien;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhongGiuongCoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoGiuongDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn soTienCoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThai;
    }
}

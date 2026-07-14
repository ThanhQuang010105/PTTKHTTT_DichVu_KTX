using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HomeStayDorm.BLL; // Gọi tầng BLL
using HomeStayDorm.DTO; // Gọi tầng DTO

namespace HomeStayDorm.GUI
{
    public partial class frmHopDongThue : Form
    {
        // Khởi tạo đối tượng từ tầng BLL mới
        private HopDongThueBLL bllLapHopDong = new HopDongThueBLL();
        
        // Biến lưu trữ thông tin hồ sơ đang được chọn
        private ChiTietHoSo_DTO currentHoSoInfo = null;

        public frmHopDongThue()
        {
            InitializeComponent();
            this.Load += frmHopDongThue_Load;
            
            // Gắn sự kiện (Sử dụng SelectionChangeCommitted để tránh bug ngầm)
            cboHoSoCuTru.SelectionChangeCommitted += cboHoSoCuTru_SelectionChangeCommitted;
            
            btnLamMoi.Click += btnLamMoi_Click;
            btnTaoHopDong.Click += btnTaoHopDong_Click;
            btnHuyBo.Click += btnHuyBo_Click;
        }

        private void frmHopDongThue_Load(object sender, EventArgs e)
        {
            LoadComboBoxHoSo();
            LoadDanhSachDichVu();
        }

        private void LoadComboBoxHoSo()
        {
            cboHoSoCuTru.DataSource = bllLapHopDong.GetHoSoDaDuyet();
            cboHoSoCuTru.DisplayMember = "DisplayText";
            cboHoSoCuTru.ValueMember = "MaHoSo";
            cboHoSoCuTru.SelectedIndex = -1; // Mặc định hiển thị rỗng ban đầu
        }

        private void LoadDanhSachDichVu()
        {
            chkPhiDichVu.DataSource = bllLapHopDong.GetDanhSachDichVu();
            chkPhiDichVu.DisplayMember = "DisplayText";
            chkPhiDichVu.ValueMember = "MaDV";
        }

        // ================== EVENT HANDLERS ==================

        // PHA 1: Trích xuất hồ sơ & Tính tiền cọc tự động
        private void cboHoSoCuTru_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHoSoCuTru.SelectedValue != null)
            {
                string maHoSo = cboHoSoCuTru.SelectedValue.ToString(); 
                
                currentHoSoInfo = bllLapHopDong.LayChiTietHoSo(maHoSo);

                if (currentHoSoInfo != null)
                {
                    // 1. Đổ dữ liệu Khách hàng
                    lblMaHoSo.Text = $"Mã hồ sơ / cư trú  : {currentHoSoInfo.MaHoSo}";
                    lblTenKhachHang.Text = $"Họ và tên                : {currentHoSoInfo.TenKhachHang}";
                    lblCCCD.Text = $"CCCD/CMND         : {currentHoSoInfo.CCCD}";
                    
                    // 2. Xử lý an toàn: Nếu khách chưa có phòng (chưa cọc)
                    string thongTinPhong = string.IsNullOrEmpty(currentHoSoInfo.MaPhong) 
                                        ? "Chưa xếp phòng" 
                                        : $"{currentHoSoInfo.TenPhong} ({currentHoSoInfo.MaPhong})";
                                        
                    // 3. Đổ dữ liệu Phòng & Cọc
                    lblTenPhongGiouong.Text = $"Phòng thuê            : {thongTinPhong}";
                    lblDonGiaThue.Text = $"Đơn giá thuê         : {currentHoSoInfo.DonGiaThue:N0} VNĐ";
                    lblTienCocTuDong.Text = $"Tiền cọc đã thu     : {currentHoSoInfo.TienCocTuDong:N0} VNĐ";
                    lblMaPhieuCoc.Text = $"Mã phiếu cọc        : {currentHoSoInfo.MaPhieuDatCoc}";

                    // 4. Gợi ý ngày hết hạn (Mặc định +6 tháng)
                    dtpNgayHetHan.Value = dtpNgayBatDau.Value.AddMonths(6);
                }
            }
        }

        // PHA 2: Lập hợp đồng và đồng bộ xuống tầng DB
        private void btnTaoHopDong_Click(object sender, EventArgs e)
        {
            if (currentHoSoInfo == null || string.IsNullOrEmpty(currentHoSoInfo.MaPhong))
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ cư trú hợp lệ và đã được xếp phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tính toán Thời hạn (Tháng) dựa vào 2 DateTimePicker
            int thoiHanThang = ((dtpNgayHetHan.Value.Year - dtpNgayBatDau.Value.Year) * 12) + dtpNgayHetHan.Value.Month - dtpNgayBatDau.Value.Month;
            
            if (thoiHanThang <= 0)
            {
                MessageBox.Show("Ngày hết hạn phải lớn hơn ngày bắt đầu ít nhất 1 tháng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kyThanhToan = rdoKy1Thang.Checked ? "1 Tháng" : rdoKy3Thang.Checked ? "3 Tháng" : "6 Tháng";
            
            // Map dữ liệu vào DTO chuẩn
            HopDongThue_DTO newHopDong = new HopDongThue_DTO
            {
                MaHopDong = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss"), // Phát sinh tự động
                MaKH = currentHoSoInfo.MaKH,
                MaPhong = currentHoSoInfo.MaPhong,
                MaPhieuCoc = currentHoSoInfo.MaPhieuDatCoc,
                NgayBatDau = dtpNgayBatDau.Value,
                ThoiHanThang = thoiHanThang,
                KyThanhToan = kyThanhToan,
                TienCoc = currentHoSoInfo.TienCocTuDong
            };

            // Lấy danh sách mã dịch vụ đi kèm
            List<string> listMaDV = new List<string>();
            foreach (var item in chkPhiDichVu.CheckedItems)
            {
                listMaDV.Add(((DichVu_DTO)item).MaDV);
            }

            // Gọi BLL xử lý nghiệp vụ
            bool isSuccess = bllLapHopDong.XuLyTaoHopDong(newHopDong, listMaDV);

            if (isSuccess)
            {
                MessageBox.Show("Tạo hợp đồng thành công!\nTrạng thái HĐ: 'Chờ thanh toán'.", 
                                "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null); // Reset form
                LoadComboBoxHoSo(); // Cập nhật lại list (hồ sơ này đã biến mất khỏi Combobox do đã lập HĐ)
            }
            else
            {
                MessageBox.Show("Tạo hợp đồng thất bại. Vui lòng kiểm tra lại dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Trả Form về trạng thái mặc định
            cboHoSoCuTru.SelectedIndex = -1;
            currentHoSoInfo = null;

            lblMaHoSo.Text = "Mã hồ sơ / cư trú  : ---";
            lblTenKhachHang.Text = "Họ và tên               : ---";
            lblCCCD.Text = "CCCD/CMND         : ---";
            lblTenPhongGiouong.Text = "Phòng thuê            : ---";
            lblDonGiaThue.Text = "Đơn giá thuê         : ---";
            lblTienCocTuDong.Text = "Tiền cọc đã thu     : ---";
            lblMaPhieuCoc.Text = "Mã phiếu cọc        : ---";

            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now;
            rdoKy1Thang.Checked = true;
            
            // Bỏ check tất cả các dịch vụ
            for (int i = 0; i < chkPhiDichVu.Items.Count; i++)
            {
                chkPhiDichVu.SetItemChecked(i, false);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
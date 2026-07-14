using System;
using System.Data;
using System.Windows.Forms;
using HomeStayDorm.BUS;
using HomeStayDorm.DTO;

namespace HomeStayDorm.GUI
{
    public partial class frmHopDongThue : Form
    {
        private HopDong_BUS _busHopDong = new HopDong_BUS();
        private PhongGiuong_DTO _phongHienTai = new PhongGiuong_DTO();

        public frmHopDongThue()
        {
            InitializeComponent(); // Sinh tự động bởi WinForms Designer
            LoadComboBoxPhieuCoc();
        }

        private void LoadComboBoxPhieuCoc()
        {
            DataTable dtPhieuCoc = _busHopDong.LayDanhSachPhieuCoc();
            cboPhieuCoc.DataSource = dtPhieuCoc;
            cboPhieuCoc.DisplayMember = "hoTen"; // Hoặc custom hiển thị "Mã - Tên - SĐT"
            cboPhieuCoc.ValueMember = "MaPhieu";
        }

        // Kích hoạt khi Sale chọn 1 khách hàng từ mục 1
        private void cboPhieuCoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhieuCoc.SelectedValue != null && cboPhieuCoc.SelectedValue is string maPhieu)
            {
                // 1. Load thông tin phòng
                _phongHienTai = _busHopDong.LayChiTietPhong(maPhieu);
                
                txtChiNhanh.Text = _phongHienTai.ChiNhanh;
                txtGiaThue.Text = _phongHienTai.GiaThue.ToString("N0") + " VND / tháng";

                // 2. Tính tiền cọc tự động hiển thị ra Label đỏ
                // Giả định phòng này có sức chứa tối đa là 4 người
                decimal tienCoc = _busHopDong.TinhTienQuyDinhCoc(
                    _phongHienTai.GiaThue, 
                    _phongHienTai.SoGiuongThue, 
                    _phongHienTai.ThueNguyenPhong, 
                    sucChuaToiDa: 4);

                lblTienCoc.Text = $"{tienCoc:N0} VND";
            }
        }

        private void btnTaoHopDong_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ UI
            HopDongThue_DTO hopDongMoi = new HopDongThue_DTO
            {
                MaKH = "KH001", // Thực tế lấy ẩn từ DTO khách hàng
                MaPhong = _phongHienTai.MaPhong,
                MaPhieuCoc = cboPhieuCoc.SelectedValue.ToString(),
                NgayBatDau = dtpNgayBatDau.Value,
                KyThanhToan = rdo1Thang.Checked ? "1 tháng" : "3 tháng",
            };

            // Thực thi Business Logic
            bool result = _busHopDong.XuLyTaoHopDong(hopDongMoi);

            if (result)
            {
                MessageBox.Show("Tạo hợp đồng thành công! Dữ liệu đã được chuyển sang phòng Kế toán.", 
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi tạo hợp đồng.", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
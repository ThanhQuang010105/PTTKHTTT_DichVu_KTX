using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HomeStayDorm.BLL.DatCoc;
using HomeStayDorm.DTO;

namespace HomeStayDorm.GUI.DatCoc
{
    public partial class FrmTraCuuDatCoc : Form
    {
        private PhieuDatCoc bll = new PhieuDatCoc();

        public FrmTraCuuDatCoc()
        {
            InitializeComponent();
        }

        private void FrmTraCuuDatCoc_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0; // Select "Tất cả"
            dgvDanhSachPhieuCoc.AutoGenerateColumns = false;

            // Tự động load danh sách phiếu đặt cọc khi vừa mở form
            LoadDuLieuBanDau();
        }

        private void LoadDuLieuBanDau()
        {
            List<PhieuDatCocDTO> ds = bll.TimKiemPhieuDatCoc("", "", "", "Tất cả");
            if (ds != null && ds.Count > 0)
            {
                HienThiDanhSach(ds);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maPhieu = txtMaDatCoc.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = mskCCCD.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem?.ToString();

            // Alt: Kiểm tra bộ lọc dữ liệu đầu vào (Tất cả các ô nhập liệu đều trống)
            if (string.IsNullOrEmpty(maPhieu) && 
                string.IsNullOrEmpty(sdt) && 
                string.IsNullOrEmpty(cccd) && 
                (string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả"))
            {
                HienThiThongBao("Vui lòng nhập ít nhất một điều kiện để tìm kiếm!");
                return;
            }

            // Gọi xử lý nghiệp vụ tìm kiếm
            List<PhieuDatCocDTO> ds = bll.TimKiemPhieuDatCoc(maPhieu, sdt, cccd, trangThai);

            // Alt: Kiểm tra kết quả
            if (ds == null || ds.Count == 0)
            {
                HienThiThongBao("Không tìm thấy thông tin đặt cọc hợp lệ!");
                dgvDanhSachPhieuCoc.DataSource = null;
            }
            else
            {
                HienThiDanhSach(ds);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDatCoc.Clear();
            txtSDT.Clear();
            mskCCCD.Clear();
            cboTrangThai.SelectedIndex = 0;
            
            // Xóa lưới rồi load lại toàn bộ dữ liệu ban đầu
            dgvDanhSachPhieuCoc.DataSource = null;
            LoadDuLieuBanDau();
        }

        private void btnXemChiTietLapHoSo_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhieuCoc.SelectedRows.Count > 0)
            {
                // Giả định grid view bind trực tiếp list hoặc data source
                string maPhieu = dgvDanhSachPhieuCoc.SelectedRows[0].Cells["maPhieuCoc"].Value?.ToString();
                
                // Gọi BLL kiểm tra (theo Sequence Diagram)
                bool hopLe = bll.KiemTraDieuKienLapHoSo(maPhieu);

                // Alt: Kiểm tra tiền điều kiện
                if (!hopLe)
                {
                    HienThiThongBao("Phiếu đặt cọc này đã hết hiệu lực, không thể lập hồ sơ cư trú!");
                }
                else
                {
                    // Trả về True (Hợp lệ) -> Điều hướng đến form khác
                    var selectedDto = (PhieuDatCocDTO)dgvDanhSachPhieuCoc.SelectedRows[0].DataBoundItem;
                    
                    FrmCapNhatHoSo frm = new FrmCapNhatHoSo();
                    frm.MaDatCocGoc = selectedDto.maPhieuCoc;
                    frm.PhongGiuongCoc = selectedDto.PhongGiuongCoc;
                    frm.SoGiuongCoc = selectedDto.SoGiuongDat;
                    frm.GioiTinhPhong = selectedDto.GioiTinhPhong;
                    frm.TrangThai = selectedDto.trangThai;
                    
                    frm.ShowDialog();
                }
            }
            else
            {
                HienThiThongBao("Vui lòng chọn một phiếu đặt cọc để xem chi tiết!");
            }
        }

        private void HienThiDanhSach(List<PhieuDatCocDTO> ds)
        {
            dgvDanhSachPhieuCoc.DataSource = ds;
        }

        private void HienThiThongBao(string msg)
        {
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

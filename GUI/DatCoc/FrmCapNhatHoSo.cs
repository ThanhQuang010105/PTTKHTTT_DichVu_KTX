using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.DatCoc;
using HomeStayDorm.DTO;

namespace HomeStayDorm.GUI.DatCoc
{
    public partial class FrmCapNhatHoSo : Form
    {
        private HoSoCuTru bll = new HoSoCuTru();
        private List<HoSoCuTruDTO> inMemoryList = new List<HoSoCuTruDTO>();

        // Dữ liệu từ màn hình trước truyền sang
        public string MaDatCocGoc { get; set; }
        public string PhongGiuongCoc { get; set; }
        public int SoGiuongCoc { get; set; }
        public string GioiTinhPhong { get; set; }
        public string TrangThai { get; set; }

        public FrmCapNhatHoSo()
        {
            InitializeComponent();
        }

        private void FrmCapNhatHoSo_Load(object sender, EventArgs e)
        {
            dgvDanhSachThanhVien.AutoGenerateColumns = false;

            // Load thông tin gốc lên các Label (TextBox readonly)
            txtMaDatCocGoc.Text = MaDatCocGoc;
            txtMaPhong.Text = PhongGiuongCoc;
            txtSoGiuongCoc.Text = SoGiuongCoc.ToString();
            txtGioiTinhPhong.Text = GioiTinhPhong;
            txtSoGiuongDaCoc.Text = SoGiuongCoc.ToString();

            cboGioiTinh.SelectedIndex = 0;
            
            // Load danh sách người đã có trong hồ sơ
            inMemoryList = bll.GetHoSoCuTruByDatCoc(MaDatCocGoc);
            
            RefreshGrid();
            CapNhatSoNguoiDaNhap();

            // Khóa form nếu trạng thái là Đã duyệt
            if (TrangThai == "Đã duyệt")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuuHoSo.Enabled = false;
                btnLuuTamHoSo.Enabled = false;
                txtHoTen.Enabled = false;
                txtCCCD.Enabled = false;
                txtSDT.Enabled = false;
                cboGioiTinh.Enabled = false;
                txtQueQuan.Enabled = false;
                HienThiThongBao("Phiếu đặt cọc đã được duyệt. Bạn chỉ có thể xem chi tiết hồ sơ lưu trú, không thể chỉnh sửa.");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Reset màu viền đỏ
            ResetViendem();

            string hoTen = txtHoTen.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString();
            string queQuan = txtQueQuan.Text.Trim();

            // 1. Kiểm tra thiếu dữ liệu (Validation)
            if (!bll.KiemTraBatBuoc(hoTen, cccd, sdt, queQuan, gioiTinh))
            {
                DanhDauLoiNhapLieu();
                HienThiThongBao("Vui lòng nhập đầy đủ các trường bắt buộc có dấu (*).");
                return;
            }

            // 2. Kiểm tra giới tính
            if (gioiTinh != GioiTinhPhong)
            {
                HienThiThongBao("Giới tính thành viên không phù hợp với quy định lưu trú của phòng!");
                return;
            }

            // 3. Kiểm tra vượt sức chứa
            if (!bll.KiemTraGioiHanNguoiO(inMemoryList.Count, SoGiuongCoc))
            {
                HienThiThongBao("Số lượng người vượt quá sức chứa tối đa mà nhóm đã tiến hành đóng tiền đặt cọc giữ chỗ!");
                return;
            }

            int nextStt = 1;
            if (inMemoryList.Count > 0)
            {
                foreach (var item in inMemoryList)
                {
                    if (item.STT >= nextStt) nextStt = item.STT + 1;
                }
            }

            // Thêm vào danh sách in-memory
            HoSoCuTruDTO thongTinKhachHang = new HoSoCuTruDTO
            {
                STT = nextStt, // Số thứ tự nối tiếp danh sách
                hoTen = hoTen,
                cccd = cccd,
                sdt = sdt,
                gioiTinh = gioiTinh,
                queQuan = queQuan,
                maDatCoc = MaDatCocGoc,
                isNew = true
            };

            inMemoryList.Add(thongTinKhachHang);
            RefreshGrid();
            CapNhatSoNguoiDaNhap();

            // Làm sạch ô nhập
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtSDT.Clear();
            txtQueQuan.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachThanhVien.SelectedRows.Count > 0)
            {
                int stt = Convert.ToInt32(dgvDanhSachThanhVien.SelectedRows[0].Cells["STT"].Value);
                HoSoCuTruDTO itemToRemove = inMemoryList.Find(x => x.STT == stt);
                if (itemToRemove != null)
                {
                    if (!itemToRemove.isNew)
                    {
                        // Nếu là khách đã lưu trong DB, xóa luôn khỏi DB
                        if (!bll.DeleteHoSoCuTru(itemToRemove.maKH))
                        {
                            HienThiThongBao("Không thể xóa thành viên này từ cơ sở dữ liệu!");
                            return;
                        }
                    }
                    inMemoryList.Remove(itemToRemove);
                    RefreshGrid();
                    CapNhatSoNguoiDaNhap();
                }
            }
            else
            {
                HienThiThongBao("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void btnLuuHoSo_Click(object sender, EventArgs e)
        {
            if (inMemoryList.Count == 0)
            {
                HienThiThongBao("Chưa có thành viên nào trong danh sách!");
                return;
            }

            bool allSuccess = true;
            foreach (var khach in inMemoryList)
            {
                if (khach.isNew)
                {
                    string result = bll.LuuHoSoCuTru(khach, 0, SoGiuongCoc); 
                    if (result != "Thành công")
                    {
                        allSuccess = false;
                        HienThiThongBao($"Có lỗi khi lưu thành viên {khach.hoTen}: {result}");
                        break;
                    }
                }
            }

            if (allSuccess)
            {
                // Gửi hồ sơ -> Cập nhật trạng thái phiếu cọc
                bll.UpdateTrangThaiPhieuDatCoc(MaDatCocGoc, "Chờ phê duyệt");
                MessageBox.Show("Lưu hồ sơ thành công! Đã chuyển trạng thái sang 'Chờ phê duyệt'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnLuuTamHoSo_Click(object sender, EventArgs e)
        {
            if (inMemoryList.Count > 0)
            {
                bool allSuccess = true;
                foreach (var khach in inMemoryList)
                {
                    if (khach.isNew)
                    {
                        string result = bll.LuuHoSoCuTru(khach, 0, SoGiuongCoc);
                        if (result != "Thành công")
                        {
                            allSuccess = false;
                            HienThiThongBao($"Có lỗi khi lưu tạm thành viên {khach.hoTen}: {result}");
                            break;
                        }
                    }
                }
                
                if (allSuccess)
                {
                    HienThiThongBao("Lưu tạm hồ sơ thành công!");
                    // Tải lại danh sách từ CSDL để cập nhật trạng thái isNew và maKH cho các thành viên mới
                    inMemoryList = bll.GetHoSoCuTruByDatCoc(MaDatCocGoc);
                    RefreshGrid();
                    CapNhatSoNguoiDaNhap();
                }
            }
            else
            {
                HienThiThongBao("Chưa có thông tin để lưu tạm!");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshGrid()
        {
            dgvDanhSachThanhVien.DataSource = null;
            dgvDanhSachThanhVien.DataSource = inMemoryList;
        }

        private void CapNhatSoNguoiDaNhap()
        {
            txtSoNguoiDaNhap.Text = inMemoryList.Count.ToString();
            
            // Nếu đủ người thì khóa nút Thêm (theo yêu cầu: "Nếu {Số người đã nhập} > {Số giường đặt cọc gốc} -> Khóa")
            // Hoặc = Số giường cọc
            if (inMemoryList.Count >= SoGiuongCoc)
            {
                btnThem.Enabled = false;
            }
            else
            {
                btnThem.Enabled = true;
            }
        }

        private void HienThiThongBao(string msg)
        {
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DanhDauLoiNhapLieu()
        {
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim())) txtHoTen.BackColor = Color.LightPink;
            if (string.IsNullOrEmpty(txtCCCD.Text.Trim())) txtCCCD.BackColor = Color.LightPink;
            if (string.IsNullOrEmpty(txtSDT.Text.Trim())) txtSDT.BackColor = Color.LightPink;
            if (string.IsNullOrEmpty(txtQueQuan.Text.Trim())) txtQueQuan.BackColor = Color.LightPink;
        }

        private void ResetViendem()
        {
            txtHoTen.BackColor = SystemColors.Window;
            txtCCCD.BackColor = SystemColors.Window;
            txtSDT.BackColor = SystemColors.Window;
            txtQueQuan.BackColor = SystemColors.Window;
        }
    }
}

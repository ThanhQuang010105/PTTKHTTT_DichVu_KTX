using System;
using System.Windows.Forms;
using HomeStayDorm.BLL.QuanTriHeThong;
using HomeStayDorm.DTO;

namespace HomeStayDorm.UI
{
    public partial class frmThongTinKhachHang : Form
    {
        private KhachHangBLL khachHangBLL;
        private KhachHangDTO currentKhachHang;

        public frmThongTinKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            LockInputs();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập SĐT hoặc CCCD!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHangDTO kh = khachHangBLL.GetKhachHangBySearch(keyword);
            if (kh != null)
            {
                currentKhachHang = kh;
                txtMaKH.Text = kh.MaKH;
                txtHoTen.Text = kh.HoTen;
                txtCCCD.Text = kh.CCCD;
                txtSDT.Text = kh.SDT;
                txtEmail.Text = kh.Email;
                cboGioiTinh.Text = kh.GioiTinh;
                txtQuocTich.Text = kh.QuocTich;
                txtKhaNangTC.Text = kh.KhaNangTC;
                lblStatus.Text = "";
                
                LockInputs();
                btnSua.Enabled = true;
            }
            else
            {
                ClearInputs();
                MessageBox.Show("Không tìm thấy khách hàng với thông tin vừa nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "Không tìm thấy";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentKhachHang == null)
            {
                MessageBox.Show("Vui lòng tìm kiếm khách hàng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UnlockInputs();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (currentKhachHang == null)
            {
                MessageBox.Show("Vui lòng tìm kiếm khách hàng trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentKhachHang.HoTen = txtHoTen.Text.Trim();
            currentKhachHang.CCCD = txtCCCD.Text.Trim();
            currentKhachHang.SDT = txtSDT.Text.Trim();
            currentKhachHang.Email = txtEmail.Text.Trim();
            currentKhachHang.GioiTinh = cboGioiTinh.Text.Trim();
            currentKhachHang.QuocTich = txtQuocTich.Text.Trim();
            currentKhachHang.KhaNangTC = txtKhaNangTC.Text.Trim();

            bool result = khachHangBLL.UpdateKhachHang(currentKhachHang);
            if (result)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "";
                LockInputs();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LockInputs()
        {
            txtHoTen.Enabled = false;
            txtCCCD.Enabled = false;
            txtSDT.Enabled = false;
            txtEmail.Enabled = false;
            cboGioiTinh.Enabled = false;
            txtQuocTich.Enabled = false;
            txtKhaNangTC.Enabled = false;
            
            btnSua.Enabled = currentKhachHang != null;
            btnCapNhat.Enabled = false;
        }

        private void UnlockInputs()
        {
            txtHoTen.Enabled = true;
            txtCCCD.Enabled = true;
            txtSDT.Enabled = true;
            txtEmail.Enabled = true;
            cboGioiTinh.Enabled = true;
            txtQuocTich.Enabled = true;
            txtKhaNangTC.Enabled = true;
            
            btnSua.Enabled = false;
            btnCapNhat.Enabled = true;
        }

        private void ClearInputs()
        {
            currentKhachHang = null;
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtCCCD.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            cboGioiTinh.SelectedIndex = -1;
            txtQuocTich.Text = "";
            txtKhaNangTC.Text = "";
            lblStatus.Text = "";
            
            LockInputs();
            btnSua.Enabled = false;
        }
    }
}

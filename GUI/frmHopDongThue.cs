using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace HomeStayDorm.GUI
{
    public partial class MHHopDongThue : Form
    {
        private BUS_LapHopDong busLapHopDong = new BUS_LapHopDong();
        private ChiTietHoSo_DTO currentHoSoInfo = null;

        public MHHopDongThue()
        {
            InitializeComponent();
            this.Load += MHHopDongThue_Load;
            
            // Events Mapping
            cboHoSoCuTru.SelectedIndexChanged += cboHoSoCuTru_SelectedIndexChanged;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTaoHopDong.Click += btnTaoHopDong_Click;
            btnHuyBo.Click += btnHuyBo_Click;
        }

        private void MHHopDongThue_Load(object sender, EventArgs e)
        {
            LoadComboBoxHoSo();
            LoadDanhSachDichVu();
        }

        private void LoadComboBoxHoSo()
        {
            cboHoSoCuTru.DataSource = busLapHopDong.GetHoSoDaDuyet();
            cboHoSoCuTru.DisplayMember = "DisplayText";
            cboHoSoCuTru.ValueMember = "MaHoSo";
            cboHoSoCuTru.SelectedIndex = -1; // Mặc định không chọn gì
        }

        private void LoadDanhSachDichVu()
        {
            chkPhiDichVu.DataSource = busLapHopDong.GetDanhSachDichVu();
            chkPhiDichVu.DisplayMember = "DisplayText";
            chkPhiDichVu.ValueMember = "MaDV";
        }

        // ================== EVENT HANDLERS ==================

        // PHA 1 SEQUENCE: Trích xuất hồ sơ & Tính tiền cọc
        private void cboHoSoCuTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHoSoCuTru.SelectedIndex >= 0)
            {
                string maHoSo = cboHoSoCuTru.SelectedValue.ToString();
                currentHoSoInfo = busLapHopDong.LayChiTietHoSo(maHoSo);

                if (currentHoSoInfo != null)
                {
                    // Đổ dữ liệu lên UI theo định dạng phẳng
                    lblMaHoSo.Text = $"Mã hồ sơ / cư trú  : {currentHoSoInfo.MaHoSo}";
                    lblTenKhachHang.Text = $"Họ và tên                : {currentHoSoInfo.TenKhachHang}";
                    lblCCCD.Text = $"CCCD/CMND         : {currentHoSoInfo.CCCD}";
                    
                    lblTenPhongGiouong.Text = $"Phòng thuê            : {currentHoSoInfo.TenPhong} ({currentHoSoInfo.MaPhong})";
                    lblDonGiaThue.Text = $"Đơn giá thuê         : {currentHoSoInfo.DonGiaThue:N0} VNĐ";
                    lblTienCocTuDong.Text = $"Tiền cọc đã thu     : {currentHoSoInfo.TienCocTuDong:N0} VNĐ";
                    lblMaPhieuCoc.Text = $"Mã phiếu cọc        : {currentHoSoInfo.MaPhieuDatCoc}";

                    // Gợi ý ngày hết hạn (Mặc định +6 tháng)
                    dtpNgayHetHan.Value = dtpNgayBatDau.Value.AddMonths(6);
                }
            }
        }

        // PHA 2 SEQUENCE: Tạo hợp đồng và đồng bộ kế toán
        private void btnTaoHopDong_Click(object sender, EventArgs e)
        {
            if (currentHoSoInfo == null)
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ cư trú hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Thu thập dữ liệu từ Form (DTO)
            string kyThanhToan = rdoKy1Thang.Checked ? "1 Tháng" : rdoKy3Thang.Checked ? "3 Tháng" : "6 Tháng";
            
            HopDong_DTO newHopDong = new HopDong_DTO
            {
                MaHopDong = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss"), // Auto Generate
                MaKH = currentHoSoInfo.MaKH,
                MaPhong = currentHoSoInfo.MaPhong,
                MaPhieu = currentHoSoInfo.MaPhieuDatCoc,
                NgayBatDau = dtpNgayBatDau.Value,
                NgayKetThuc = dtpNgayHetHan.Value,
                KyThanhToan = kyThanhToan,
                GiaThue = currentHoSoInfo.DonGiaThue
            };

            // Lấy danh sách dịch vụ đi kèm
            List<string> listMaDV = new List<string>();
            foreach (var item in chkPhiDichVu.CheckedItems)
            {
                listMaDV.Add(((DichVu_DTO)item).MaDV);
            }

            // 2. Gọi BLL xử lý nghiệp vụ Validation & Insert
            bool isSuccess = busLapHopDong.XuLyTaoHopDong(newHopDong, listMaDV);

            if (isSuccess)
            {
                MessageBox.Show("Tạo hợp đồng thành công!\nTrạng thái: Chờ thanh toán. Đã chuyển số liệu sang Kế toán.", 
                                "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null); // Reset form
                LoadComboBoxHoSo(); // Cập nhật lại list (hồ sơ này đã mất khỏi list)
            }
            else
            {
                MessageBox.Show("Tạo hợp đồng thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboHoSoCuTru.SelectedIndex = -1;
            currentHoSoInfo = null;

            lblMaHoSo.Text = "Mã hồ sơ / cư trú  : ---";
            lblTenKhachHang.Text = "Họ và tên                : ---";
            lblCCCD.Text = "CCCD/CMND         : ---";
            lblTenPhongGiouong.Text = "Phòng thuê            : ---";
            lblDonGiaThue.Text = "Đơn giá thuê         : ---";
            lblTienCocTuDong.Text = "Tiền cọc đã thu     : ---";
            lblMaPhieuCoc.Text = "Mã phiếu cọc        : ---";

            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now;
            rdoKy1Thang.Checked = true;
            
            for (int i = 0; i < chkPhiDichVu.Items.Count; i++)
                chkPhiDichVu.SetItemChecked(i, false);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    #region ARCHITECTURE CLASSES (DTO, DAL, BLL)
    // Các class này theo chuẩn 3 lớp, thực tế sẽ nằm ở các file riêng biệt

    // 1. DATA TRANSFER OBJECTS (DTO)
    public class HoSoCuTru_DTO
    {
        public string MaHoSo { get; set; }
        public string TenKhachHang { get; set; }
        public string DisplayText { get; set; }
    }

    public class ChiTietHoSo_DTO
    {
        public string MaHoSo { get; set; }
        public string MaKH { get; set; }
        public string TenKhachHang { get; set; }
        public string CCCD { get; set; }
        public string MaPhieuDatCoc { get; set; }
        public decimal TienCocTuDong { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public decimal DonGiaThue { get; set; }
    }

    public class DichVu_DTO
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public string DisplayText { get; set; }
    }

    public class HopDong_DTO
    {
        public string MaHopDong { get; set; }
        public string MaKH { get; set; }
        public string MaPhong { get; set; }
        public string MaPhieu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string KyThanhToan { get; set; }
        public decimal GiaThue { get; set; }
    }

    // 2. DATA ACCESS LAYER (DAL) - Gọi trực tiếp các Hàm SQL đã viết
    public class DAL_HopDong
    {
        private string connectionString = "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;";

        public List<HoSoCuTru_DTO> DAL_GetHoSoDaDuyet()
        {
            List<HoSoCuTru_DTO> list = new List<HoSoCuTru_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetHoSoCuTru_DaDuyet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new HoSoCuTru_DTO { 
                        MaHoSo = dr["MaHoSo"].ToString(), 
                        TenKhachHang = dr["TenKhachHang"].ToString(), 
                        DisplayText = dr["DisplayText"].ToString() 
                    });
                }
            }
            return list;
        }

        public ChiTietHoSo_DTO DAL_GetChiTietHoSo(string maHoSo)
        {
            ChiTietHoSo_DTO dto = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetChiTietHoSoLapHopDong", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoSo", maHoSo);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dto = new ChiTietHoSo_DTO
                    {
                        MaHoSo = dr["MaHoSo"].ToString(),
                        MaKH = dr["MaKH"].ToString(),
                        TenKhachHang = dr["TenKhachHang"].ToString(),
                        CCCD = dr["CCCD"].ToString(),
                        MaPhieuDatCoc = dr["MaPhieuDatCoc"] != DBNull.Value ? dr["MaPhieuDatCoc"].ToString() : "Chưa cọc",
                        TienCocTuDong = dr["TienCocTuDong"] != DBNull.Value ? Convert.ToDecimal(dr["TienCocTuDong"]) : 0,
                        MaPhong = dr["MaPhong"].ToString(),
                        TenPhong = dr["TenPhong"].ToString(),
                        DonGiaThue = dr["DonGiaThue"] != DBNull.Value ? Convert.ToDecimal(dr["DonGiaThue"]) : 0
                    };
                }
            }
            return dto;
        }

        public List<DichVu_DTO> DAL_GetDanhSachDichVu()
        {
            List<DichVu_DTO> list = new List<DichVu_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDanhSachDichVu", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new DichVu_DTO { 
                        MaDV = dr["MaDV"].ToString(), 
                        TenDV = dr["TenDV"].ToString(), 
                        DonGia = Convert.ToDecimal(dr["DonGia"]), 
                        DisplayText = dr["DisplayText"].ToString() 
                    });
                }
            }
            return list;
        }

        public bool DAL_InsertHopDong(HopDong_DTO hd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_HopDongThue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHopDong", hd.MaHopDong);
                cmd.Parameters.AddWithValue("@MaKH", hd.MaKH);
                cmd.Parameters.AddWithValue("@MaPhong", hd.MaPhong);
                cmd.Parameters.AddWithValue("@MaPhieu", hd.MaPhieu);
                cmd.Parameters.AddWithValue("@NgayBatDau", hd.NgayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", hd.NgayKetThuc);
                cmd.Parameters.AddWithValue("@KyThanhToan", hd.KyThanhToan);
                cmd.Parameters.AddWithValue("@GiaThue", hd.GiaThue);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DAL_InsertHopDongDichVu(string maHopDong, string maDV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_HopDong_DichVu", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHopDong", maHopDong);
                cmd.Parameters.AddWithValue("@MaDV", maDV);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // 3. BUSINESS LOGIC LAYER (BLL)
    public class BUS_LapHopDong
    {
        private DAL_HopDong dal = new DAL_HopDong();

        public List<HoSoCuTru_DTO> GetHoSoDaDuyet() => dal.DAL_GetHoSoDaDuyet();
        public ChiTietHoSo_DTO LayChiTietHoSo(string maHoSo) => dal.DAL_GetChiTietHoSo(maHoSo);
        public List<DichVu_DTO> GetDanhSachDichVu() => dal.DAL_GetDanhSachDichVu();

        public bool XuLyTaoHopDong(HopDong_DTO hd, List<string> selectedServices)
        {
            // Validate Logic cơ bản
            if (hd.NgayKetThuc <= hd.NgayBatDau) return false;

            // Thực thi ghi xuống DB
            bool isInserted = dal.DAL_InsertHopDong(hd);
            if (isInserted)
            {
                // Thêm các dịch vụ đi kèm
                foreach (var maDV in selectedServices)
                {
                    dal.DAL_InsertHopDongDichVu(hd.MaHopDong, maDV);
                }
                return true;
            }
            return false;
        }
    }
    #endregion
}
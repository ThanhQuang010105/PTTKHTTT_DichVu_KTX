using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HomeStayDorm.BLL.DangKyThue;
using HomeStayDorm.DTO;
using HomeStayDorm.UI.Common;

namespace HomeStayDorm.UI.DangKyThue
{
    public class frmLichXemPhong : Form
    {
        private readonly DangKyThueBLL _dangKyThueBLL = new DangKyThueBLL();
        private readonly TraCuuPhongGiuongBLL _traCuuPhongGiuongBLL = new TraCuuPhongGiuongBLL();
        private readonly LichXemPhongBLL _lichXemPhongBLL = new LichXemPhongBLL();
        private readonly TextBox txtMaDangKy = new TextBox();
        private readonly TextBox txtThongTinKhach = new TextBox();
        private readonly TextBox txtNhuCau = new TextBox();
        private readonly DateTimePicker dtpNgayGioHen = new DateTimePicker();
        private readonly TextBox txtGhiChu = new TextBox();
        private readonly DataGridView dgvKetQua = UiHelper.Grid();
        private readonly DataGridView dgvLichHen = UiHelper.Grid();
        private readonly Label lblTrangThai = new Label();
        private PhieuDangKyThueDTO? _phieuDangKy;

        public frmLichXemPhong()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Lập lịch xem phòng";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1100, 780);
            MinimumSize = new Size(900, 560);
            BackColor = UiHelper.Surface;
            Font = new Font("Segoe UI", 10F);
            AutoScroll = false;

            Panel viewport = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiHelper.Surface
            };

            Panel content = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1080, 995),
                BackColor = UiHelper.Surface
            };

            Label title = new Label
            {
                Text = "Lập lịch xem phòng",
                Location = new Point(24, 22),
                Size = new Size(1020, 38),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = UiHelper.Text
            };

            GroupBox info = new GroupBox
            {
                Text = "Tra cứu phiếu đăng ký",
                Location = new Point(22, 80),
                Size = new Size(1038, 175),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            Button btnTim = UiHelper.Button("Tìm kiếm", UiHelper.Primary);
            btnTim.Location = new Point(842, 29);
            btnTim.Size = new Size(130, 42);
            btnTim.TextAlign = ContentAlignment.MiddleCenter;
            btnTim.Click += BtnTim_Click;

            AddField(info, "Mã đăng ký", txtMaDangKy, 20, 34, 120, 680);
            AddField(info, "Khách hàng", txtThongTinKhach, 20, 78, 120, 822);
            AddField(info, "Nhu cầu", txtNhuCau, 20, 122, 120, 822);
            txtThongTinKhach.ReadOnly = true;
            txtNhuCau.ReadOnly = true;
            info.Controls.Add(btnTim);

            GroupBox results = new GroupBox
            {
                Text = "Danh sách phòng/giường phù hợp",
                Location = new Point(22, 275),
                Size = new Size(1038, 285),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            results.Controls.Add(dgvKetQua);

            GroupBox scheduleInput = new GroupBox
            {
                Text = "Thông tin lịch hẹn",
                Location = new Point(22, 580),
                Size = new Size(1038, 95),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };

            Label lblNgayGio = InlineLabel("Ngày giờ hẹn", 20, 38, 105);
            dtpNgayGioHen.Format = DateTimePickerFormat.Custom;
            dtpNgayGioHen.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayGioHen.Location = new Point(135, 34);
            dtpNgayGioHen.Size = new Size(180, 36);

            Label lblGhiChu = InlineLabel("Ghi chú", 345, 38, 65);
            txtGhiChu.Location = new Point(418, 34);
            txtGhiChu.Size = new Size(315, 36);
            txtGhiChu.PlaceholderText = "Ghi chú";

            Button btnLuu = UiHelper.Button("Lưu lịch hẹn", UiHelper.Primary);
            btnLuu.Location = new Point(748, 29);
            btnLuu.Size = new Size(145, 42);
            btnLuu.TextAlign = ContentAlignment.MiddleCenter;
            btnLuu.Click += BtnLuu_Click;

            Button btnLamMoi = UiHelper.Button("Làm mới");
            btnLamMoi.Location = new Point(905, 29);
            btnLamMoi.Size = new Size(105, 42);
            btnLamMoi.TextAlign = ContentAlignment.MiddleCenter;
            btnLamMoi.Click += (_, _) => ResetForm();

            scheduleInput.Controls.Add(lblNgayGio);
            scheduleInput.Controls.Add(dtpNgayGioHen);
            scheduleInput.Controls.Add(lblGhiChu);
            scheduleInput.Controls.Add(txtGhiChu);
            scheduleInput.Controls.Add(btnLuu);
            scheduleInput.Controls.Add(btnLamMoi);

            GroupBox schedule = new GroupBox
            {
                Text = "Lịch hẹn đã lập",
                Location = new Point(22, 695),
                Size = new Size(1038, 230),
                Padding = new Padding(12),
                ForeColor = UiHelper.Text
            };
            schedule.Controls.Add(dgvLichHen);

            lblTrangThai.Location = new Point(24, 940);
            lblTrangThai.Size = new Size(1020, 42);
            lblTrangThai.ForeColor = UiHelper.Muted;
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;

            content.Controls.Add(title);
            content.Controls.Add(info);
            content.Controls.Add(results);
            content.Controls.Add(scheduleInput);
            content.Controls.Add(schedule);
            content.Controls.Add(lblTrangThai);
            viewport.Controls.Add(content);
            Controls.Add(viewport);

            ResetForm();
            LoadLichHen();
        }

        private static void AddField(Control parent, string labelText, Control control, int x, int y, int labelWidth, int controlWidth)
        {
            Label label = InlineLabel(labelText, x, y + 2, labelWidth);
            control.Location = new Point(x + labelWidth + 12, y);
            control.Size = new Size(controlWidth, 36);
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            parent.Controls.Add(label);
            parent.Controls.Add(control);
        }

        private static Label InlineLabel(string text, int x, int y, int width)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = UiHelper.Muted
            };
        }

        private void ResetForm()
        {
            txtMaDangKy.Clear();
            txtThongTinKhach.Clear();
            txtNhuCau.Clear();
            txtGhiChu.Clear();
            dgvKetQua.DataSource = null;
            _phieuDangKy = null;
            dtpNgayGioHen.Value = DateTime.Now.AddDays(1);
            lblTrangThai.Text = "Nhập mã đăng ký để lấy thông tin và danh sách phòng/giường phù hợp.";
            lblTrangThai.ForeColor = UiHelper.Muted;
        }

        private void LoadLichHen()
        {
            try
            {
                dgvLichHen.DataSource = _lichXemPhongBLL.LayDanhSach();
            }
            catch
            {
                dgvLichHen.DataSource = null;
            }
        }

        private void BtnTim_Click(object? sender, EventArgs e)
        {
            try
            {
                DataTable chiTiet = _dangKyThueBLL.LayChiTietDangKy(txtMaDangKy.Text);
                if (chiTiet.Rows.Count == 0)
                {
                    lblTrangThai.Text = "Không tìm thấy phiếu đăng ký.";
                    lblTrangThai.ForeColor = UiHelper.Danger;
                    return;
                }

                _phieuDangKy = _dangKyThueBLL.TaoDtoTuDongChiTiet(chiTiet.Rows[0]);
                txtThongTinKhach.Text = $"{_phieuDangKy.HoTenKhachHang} - {_phieuDangKy.SoDienThoai}";
                txtNhuCau.Text = $"{_phieuDangKy.HinhThucThue}, {_phieuDangKy.KhuVucMongMuon}, {_phieuDangKy.LoaiPhong}";

                TraCuuPhongGiuongResult ketQua = _traCuuPhongGiuongBLL.TraCuuPhongGiuongKhaDung(_phieuDangKy);
                dgvKetQua.DataSource = ketQua.KetQua;
                lblTrangThai.Text = ketQua.ThongBao;
                lblTrangThai.ForeColor = UiHelper.Success;
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_phieuDangKy == null)
                {
                    throw new InvalidOperationException("Vui lòng tra cứu phiếu đăng ký trước.");
                }

                if (dgvKetQua.CurrentRow == null)
                {
                    throw new InvalidOperationException("Vui lòng chọn một phòng/giường trong danh sách.");
                }

                string loaiKetQua = Convert.ToString(dgvKetQua.CurrentRow.Cells["LoaiKetQua"].Value) ?? string.Empty;
                string maPhong = Convert.ToString(dgvKetQua.CurrentRow.Cells["MaPhong"].Value) ?? string.Empty;
                string? maGiuong = dgvKetQua.Columns.Contains("MaGiuong")
                    ? Convert.ToString(dgvKetQua.CurrentRow.Cells["MaGiuong"].Value)
                    : null;

                LichXemPhongDTO lich = new LichXemPhongDTO
                {
                    MaDangKy = _phieuDangKy.MaDangKy,
                    LoaiDoiTuong = loaiKetQua,
                    MaPhong = maPhong,
                    MaGiuong = maGiuong,
                    NgayGioHen = dtpNgayGioHen.Value,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                string maLich = _lichXemPhongBLL.TaoLich(lich);
                lblTrangThai.Text = $"Đã lập lịch #{maLich} và cập nhật phiếu {_phieuDangKy.MaDangKy} thành Đã hẹn xem phòng.";
                lblTrangThai.ForeColor = UiHelper.Success;
                LoadLichHen();
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = ex.Message;
                lblTrangThai.ForeColor = UiHelper.Danger;
            }
        }
    }
}

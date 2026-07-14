# Feature 02 - Tạo đăng ký thuê

## 1. Mục đích

Cho phép nhân viên sale nhập thông tin khách hàng và nhu cầu thuê để tạo phiếu đăng ký thuê. Feature này chỉ tạo phiếu đăng ký, không tra cứu phòng/giường.

## 2. GUI - Presentation layer

File: `UI/DangKyThue/frmDangKyThue.cs`

### Thành phần giao diện

| Nhóm | Thành phần | Kiểu | Vai trò |
|---|---|---|---|
| Thông tin khách hàng | `txtHoTen` | `TextBox` | Họ tên khách hàng. |
| Thông tin khách hàng | `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| Thông tin khách hàng | `txtCCCD` | `TextBox` | CCCD/CMND. |
| Thông tin khách hàng | `txtEmail` | `TextBox` | Email. |
| Thông tin khách hàng | `cboGioiTinh` | `ComboBox` | Giới tính/khu vực giới tính, lấy động từ DB qua `PhongBLL`. |
| Nhu cầu thuê | `cboHinhThucThue` | `ComboBox` | Giá trị cố định: `Thuê nguyên phòng`, `Thuê giường`. |
| Nhu cầu thuê | `cboKhuVuc` | `ComboBox` | Khu vực mong muốn, lấy từ `Chi_Nhanh.KhuVuc`. |
| Nhu cầu thuê | `cboLoaiPhong` | `ComboBox` | Loại phòng, lấy từ `Phong.LoaiPhong`. |
| Nhu cầu thuê | `nudSoNguoi` | `NumericUpDown` | Số người dự kiến ở. |
| Nhu cầu thuê | `nudGiaToiDa` | `NumericUpDown` | Giá tối đa. |
| Nhu cầu thuê | `dtpNgayVao` | `DateTimePicker` | Ngày vào dự kiến. |
| Nhu cầu thuê | `nudThoiHan` | `NumericUpDown` | Thời hạn thuê theo tháng. |
| Nhu cầu thuê | `txtTieuChiUuTien` | `TextBox` | Tiêu chí ưu tiên. |
| Thao tác | `btnLuu` | `Button` | Lưu phiếu đăng ký. |
| Thao tác | `btnXoaTrang` | `Button` | Reset form. |
| Trạng thái | `lblTrangThai` | `Label` | Hiển thị thông báo. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo layout, groupbox, control, button. |
| `LoadDefaultValues()` | Cấu hình combobox, numeric, date picker, placeholder. |
| `BtnLuu_Click(sender, e)` | Tạo DTO, gọi BLL kiểm tra, gọi BLL lưu phiếu. |
| `BtnXoaTrang_Click(sender, e)` | Reset form. |
| `ResetCacTruongNhapLieu()` | Xóa input và đưa control về mặc định. |
| `TaoThongTinDangKyTuDuLieuNhap()` | Tạo `PhieuDangKyThueDTO` từ dữ liệu nhập. |
| `SetStatus(message, color)` | Cập nhật thông báo trạng thái. |
| `AddField(...)`, `ConfigureButton(...)` | Hàm hỗ trợ dựng UI. |

## 3. DTO liên quan

File: `DTO/PhieuDangKyThueDTO.cs`

| Thuộc tính chính | Vai trò |
|---|---|
| `MaDangKy` | Mã phiếu đăng ký sau khi lưu. |
| `HoTenKhachHang`, `SoDienThoai`, `CCCD`, `Email`, `GioiTinh` | Thông tin khách hàng. |
| `HinhThucThue`, `KhuVucMongMuon`, `LoaiPhong` | Nhu cầu thuê. |
| `SoNguoiDuKien`, `GiaToiDa`, `NgayVaoDuKien`, `ThoiHanThueThang` | Điều kiện thuê. |
| `TieuChiUuTien`, `TrangThai`, `NgayTao` | Thông tin bổ sung/trạng thái. |

## 4. BLL - Business Logic Layer

File: `BLL/DangKyThue/DangKyThueBLL.cs`

| Hàm | Vai trò |
|---|---|
| `TaoDangKy(phieuDangKy)` | Hàm tổng hợp: kiểm tra hợp lệ rồi tạo phiếu. |
| `KiemTraThongTinHopLe(phieuDangKy)` | Kiểm tra dữ liệu bắt buộc và quy tắc nghiệp vụ. |
| `TaoPhieuDangKy(phieuDangKy)` | Gọi DAL lưu phiếu, cập nhật `MaDangKy`, trả `DangKyResult`. |
| `LayDanhSachDangKy()` | Lấy danh sách phiếu đăng ký. Dùng cho feature khác. |
| `LayChiTietDangKy(maDangKy)` | Lấy chi tiết một phiếu. Dùng cho lịch xem phòng. |
| `TaoDtoTuDongChiTiet(row)` | Map dòng chi tiết đăng ký sang `PhieuDangKyThueDTO`. |
| `LayLoiThongTinDangKy(phieuDangKy)` | Hàm private gom lỗi validate. |
| `LaHinhThucThueHopLe(hinhThucThue)` | Kiểm tra hình thức thuê hợp lệ. |

Class kết quả: `DangKyResult`

| Thành phần | Vai trò |
|---|---|
| `ThanhCong` | Kết quả xử lý. |
| `MaDangKy` | Mã phiếu tạo được. |
| `ThongBao` | Thông báo cho GUI. |
| `TaoThanhCong(maDangKy, thongBao)` | Tạo kết quả thành công. |
| `TaoThatBai(thongBao)` | Tạo kết quả thất bại. |

BLL danh mục dùng cho combobox:

| BLL | Hàm | Vai trò |
|---|---|---|
| `ChiNhanhBLL` | `LayDanhMucKhuVuc()` | Lấy khu vực từ DB. |
| `PhongBLL` | `LayDanhMucGioiTinhQuyDinh()` | Lấy danh mục giới tính/quy định phòng. |
| `PhongBLL` | `LayDanhMucLoaiPhong()` | Lấy loại phòng từ DB. |

## 5. DAL - Data Access Layer

File: `DAL/DangKyThue/DangKyThueDAL.cs`

| Hàm | Vai trò |
|---|---|
| `ThemPhieuDangKy(phieuDangKy)` | Tạo mã khách hàng, mã đăng ký, insert `Khach_hang`, insert `Dang_ky_thue`. |
| `TaoPhieuDangKy(phieuDangKy)` | Alias gọi `ThemPhieuDangKy`. |
| `LayDanhSachDangKy()` | SELECT danh sách phiếu đăng ký join khách hàng. |
| `LayChiTietDangKy(maDangKy)` | SELECT chi tiết một phiếu đăng ký. |
| `CapNhatTrangThai(maDangKy, trangThai)` | Cập nhật trạng thái phiếu đăng ký. |
| `TaoMaDangKyMoi()` | Sinh mã `DKxxx`. |
| `TaoMaKhachHangMoi()` | Sinh mã `KHxxx`. |
| `TaoCccdTam(maKhachHang)` | Tạo CCCD tạm nếu người dùng bỏ trống. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Khach_hang` | Lưu thông tin khách hàng. |
| `Dang_ky_thue` | Lưu phiếu đăng ký thuê. |
| `Chi_Nhanh` | Nguồn danh mục khu vực. |
| `Phong` | Nguồn danh mục giới tính và loại phòng. |

## 7. Sequence để tự vẽ

1. Nhân viên sale mở màn hình `frmDangKyThue`.
2. GUI gọi `LoadDefaultValues()` để nạp combobox và giá trị mặc định.
3. Nhân viên nhập thông tin khách hàng và nhu cầu thuê.
4. Nhân viên bấm `Lưu phiếu đăng ký`.
5. `BtnLuu_Click()` gọi `TaoThongTinDangKyTuDuLieuNhap()`.
6. GUI gọi `DangKyThueBLL.KiemTraThongTinHopLe(dto)`.
7. Nếu không hợp lệ, BLL trả `DangKyResult` thất bại, GUI hiển thị lỗi.
8. Nếu hợp lệ, GUI gọi `DangKyThueBLL.TaoPhieuDangKy(dto)`.
9. BLL gọi `DangKyThueDAL.ThemPhieuDangKy(dto)`.
10. DAL sinh mã khách hàng và mã đăng ký.
11. DAL insert `Khach_hang`.
12. DAL insert `Dang_ky_thue` với trạng thái `Mới tạo`.
13. DAL trả `MaDK`.
14. BLL trả `DangKyResult` thành công.
15. GUI hiển thị thông báo tạo phiếu thành công.

## 8. Ghi chú

- Feature này không gọi tra cứu phòng/giường.
- Hình thức thuê là enum nghiệp vụ cố định trong `DangKyThueBLL`.
- Khu vực, giới tính, loại phòng lấy động từ DB nếu có dữ liệu.

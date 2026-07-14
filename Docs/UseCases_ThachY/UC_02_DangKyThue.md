# UC 02 - Đăng ký thuê

## 1. Phạm vi use case

Nhân viên sale nhập thông tin khách hàng và nhu cầu thuê để tạo phiếu đăng ký thuê. UC này không tra cứu phòng/giường và không lập lịch xem phòng.

## 2. GUI / Presentation layer

File: `UI/DangKyThue/frmDangKyThue.cs`

### Control

| Nhóm | Control | Kiểu | Vai trò |
|---|---|---|---|
| Khách hàng | `txtHoTen` | `TextBox` | Họ tên khách hàng. |
| Khách hàng | `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| Khách hàng | `txtCCCD` | `TextBox` | CCCD/CMND. |
| Khách hàng | `txtEmail` | `TextBox` | Email. |
| Khách hàng | `cboGioiTinh` | `ComboBox` | Giới tính, lấy từ DB qua `PhongBLL.LayDanhMucGioiTinhQuyDinh`. |
| Nhu cầu | `cboHinhThucThue` | `ComboBox` | `Thuê nguyên phòng`, `Thuê giường`. |
| Nhu cầu | `cboKhuVuc` | `ComboBox` | Khu vực, lấy từ `ChiNhanhBLL.LayDanhMucKhuVuc`. |
| Nhu cầu | `cboLoaiPhong` | `ComboBox` | Loại phòng, lấy từ `PhongBLL.LayDanhMucLoaiPhong`. |
| Nhu cầu | `nudSoNguoi` | `NumericUpDown` | Số người dự kiến. |
| Nhu cầu | `nudGiaToiDa` | `NumericUpDown` | Giá tối đa. |
| Nhu cầu | `dtpNgayVao` | `DateTimePicker` | Ngày vào dự kiến. |
| Nhu cầu | `nudThoiHan` | `NumericUpDown` | Thời hạn thuê theo tháng. |
| Nhu cầu | `txtTieuChiUuTien` | `TextBox` | Tiêu chí ưu tiên. |
| Thao tác | `btnLuu` | `Button` | Lưu phiếu đăng ký. |
| Thao tác | `btnXoaTrang` | `Button` | Reset form. |
| Trạng thái | `lblTrangThai` | `Label` | Thông báo lỗi/thành công. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `frmDangKyThue()` | Constructor. |
| `InitializeComponent()` | Dựng UI. |
| `LoadDefaultValues()` | Nạp dữ liệu combobox và cấu hình control. |
| `BtnLuu_Click(sender, e)` | Tạo DTO, gọi BLL validate và lưu. |
| `BtnXoaTrang_Click(sender, e)` | Reset form. |
| `ResetCacTruongNhapLieu()` | Xóa input và đưa control về mặc định. |
| `TaoThongTinDangKyTuDuLieuNhap()` | Tạo `PhieuDangKyThueDTO` từ form. |
| `SetStatus(message, color)` | Hiển thị thông báo. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/DangKyThue/DangKyThueBLL.cs`

| Class/Hàm | Vai trò |
|---|---|
| `DangKyThueBLL` | Xử lý nghiệp vụ đăng ký thuê. |
| `TaoDangKy(phieuDangKy)` | Hàm tổng hợp: validate rồi lưu. |
| `KiemTraThongTinHopLe(phieuDangKy)` | Kiểm tra thông tin bắt buộc/quy tắc. |
| `TaoPhieuDangKy(phieuDangKy)` | Gọi DAL thêm phiếu, trả `DangKyResult`. |
| `LayLoiThongTinDangKy(phieuDangKy)` | Hàm private gom lỗi validate. |
| `LaHinhThucThueHopLe(hinhThucThue)` | Kiểm tra hình thức thuê. |
| `DangKyResult` | Kết quả trả về GUI. |

BLL hỗ trợ nạp combobox:

| Class/Hàm | Vai trò |
|---|---|
| `ChiNhanhBLL.LayDanhMucKhuVuc()` | Nạp khu vực. |
| `PhongBLL.LayDanhMucGioiTinhQuyDinh()` | Nạp giới tính. |
| `PhongBLL.LayDanhMucLoaiPhong()` | Nạp loại phòng. |

## 4. DAL - Data Access layer

File: `DAL/DangKyThue/DangKyThueDAL.cs`

| Hàm | Vai trò |
|---|---|
| `ThemPhieuDangKy(phieuDangKy)` | Insert `Khach_hang` và `Dang_ky_thue`. |
| `TaoPhieuDangKy(phieuDangKy)` | Alias gọi `ThemPhieuDangKy`. |
| `TaoMaDangKyMoi()` | Sinh mã `DKxxx`. |
| `TaoMaKhachHangMoi()` | Sinh mã `KHxxx`. |
| `TaoCccdTam(maKhachHang)` | Tạo CCCD tạm nếu bỏ trống. |

DAL hỗ trợ combobox:

| Class/Hàm | Vai trò |
|---|---|
| `ChiNhanhDAL.LayDanhMucKhuVuc()` | Đọc `Chi_Nhanh.KhuVuc`. |
| `PhongDAL.LayDanhMucGioiTinhQuyDinh()` | Đọc `Phong.GioiTinhQuyDinh`. |
| `PhongDAL.LayDanhMucLoaiPhong()` | Đọc `Phong.LoaiPhong`. |

## 5. DTO và Database

DTO: `DTO/PhieuDangKyThueDTO.cs`

| Thuộc tính chính | Vai trò |
|---|---|
| `HoTenKhachHang`, `SoDienThoai`, `CCCD`, `Email`, `GioiTinh` | Thông tin khách hàng. |
| `HinhThucThue`, `KhuVucMongMuon`, `LoaiPhong` | Nhu cầu thuê. |
| `SoNguoiDuKien`, `GiaToiDa`, `NgayVaoDuKien`, `ThoiHanThueThang` | Điều kiện thuê. |
| `TieuChiUuTien`, `TrangThai`, `NgayTao` | Thông tin bổ sung. |

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Khach_hang` | Lưu thông tin khách hàng. |
| `Dang_ky_thue` | Lưu phiếu đăng ký thuê. |
| `Chi_Nhanh`, `Phong` | Nguồn dữ liệu combobox. |

## 6. Sequence để vẽ

1. Actor mở `frmDangKyThue`.
2. GUI gọi `LoadDefaultValues()`.
3. Actor nhập thông tin khách hàng và nhu cầu thuê.
4. Actor bấm `Lưu phiếu đăng ký`.
5. `BtnLuu_Click()` gọi `TaoThongTinDangKyTuDuLieuNhap()`.
6. GUI gọi `DangKyThueBLL.KiemTraThongTinHopLe(dto)`.
7. Nếu không hợp lệ, BLL trả `DangKyResult` thất bại, GUI hiển thị lỗi.
8. Nếu hợp lệ, GUI gọi `DangKyThueBLL.TaoPhieuDangKy(dto)`.
9. BLL gọi `DangKyThueDAL.ThemPhieuDangKy(dto)`.
10. DAL sinh `MaKH` và `MaDK`.
11. DAL insert `Khach_hang`.
12. DAL insert `Dang_ky_thue` với trạng thái `Mới tạo`.
13. DAL trả `MaDK`.
14. BLL trả `DangKyResult` thành công.
15. GUI hiển thị thông báo tạo phiếu thành công.

## 7. Ghi chú để vẽ 3-layer

- Không vẽ `TraCuuPhongGiuongBLL` trong UC này.
- `DangKyResult` là object kết quả từ BLL về GUI.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình được tổ chức theo luồng tuyến tính từ trên xuống dưới. Nhân viên sale nhập thông tin khách hàng ở khối thông tin cá nhân, sau đó nhập nhu cầu thuê ở khối nhu cầu. Cách chia này bám sát quá trình tiếp nhận khách hàng ngoài thực tế: hỏi thông tin liên hệ trước, rồi mới ghi nhận nhu cầu thuê.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Nút phụ `Xóa trắng form` cho phép đưa toàn bộ trường nhập về trạng thái mặc định khi nhập nhầm hoặc bắt đầu tiếp nhận khách mới. Việc reset toàn form giúp nhân viên không phải xóa thủ công từng ô, giảm lỗi sót dữ liệu cũ.

**Tính nhất quán:** Các trường bắt buộc được đánh dấu `*` trong nhãn như họ tên, số điện thoại, giới tính, hình thức thuê, khu vực, loại phòng. Nút hành động chính `Lưu phiếu đăng ký` dùng màu primary, còn nút phụ dùng kiểu ít nổi bật hơn để tránh nhầm hành động chính/phụ.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Nhân viên sale tiếp nhận thông tin từ khách hàng và nhập vào nhóm thông tin khách hàng: họ tên, số điện thoại, CCCD, email, giới tính.

**Bước 2:** Nhân viên nhập nhu cầu thuê: hình thức thuê, khu vực, loại phòng, số người, giá tối đa, ngày vào, thời hạn thuê và tiêu chí ưu tiên.

**Bước 3:** Nếu nhập sai nhiều thông tin, nhân viên nhấn `Xóa trắng form` để nhập lại từ đầu.

**Bước 4:** Nhân viên nhấn `Lưu phiếu đăng ký`.

**Bước 5:** Hệ thống kiểm tra hợp lệ dữ liệu nhập. Nếu thiếu hoặc sai thông tin, hệ thống hiển thị lỗi và không ghi CSDL.

**Bước 6:** Nếu hợp lệ, hệ thống lưu thông tin khách hàng vào `Khach_hang` và phiếu đăng ký vào `Dang_ky_thue`.

**Bước 7:** Hệ thống trả mã đăng ký và hiển thị thông báo tạo phiếu thành công.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TextBox` dùng cho dữ liệu văn bản như họ tên, số điện thoại, CCCD, email và tiêu chí ưu tiên. `NumericUpDown` dùng cho số người, giá tối đa và thời hạn thuê để hạn chế nhập sai kiểu dữ liệu. `DateTimePicker` chuẩn hóa ngày vào dự kiến theo định dạng ngày. `ComboBox` dùng cho giới tính, hình thức thuê, khu vực và loại phòng để nhân viên chọn từ danh sách có kiểm soát.

**Logic bắt lỗi:** `DangKyThueBLL.KiemTraThongTinHopLe()` gọi hàm private `LayLoiThongTinDangKy()` để kiểm tra các trường bắt buộc. Nếu có lỗi, BLL trả `DangKyResult.TaoThatBai`, GUI hiển thị lỗi trên `lblTrangThai` và không gọi DAL.

**Logic nghiệp vụ:** Khi dữ liệu hợp lệ, GUI gọi `DangKyThueBLL.TaoPhieuDangKy()`. BLL gọi `DangKyThueDAL.ThemPhieuDangKy()`. DAL sinh mã khách hàng, sinh mã đăng ký, insert `Khach_hang`, sau đó insert `Dang_ky_thue` với trạng thái ban đầu là `Mới tạo`. UC này chỉ dừng ở tạo phiếu, phần tìm phòng/giường được tách sang UC `Tra cứu phòng/giường`.

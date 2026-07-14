# UC 07 - Quản lý nhân viên

## 1. Phạm vi use case

Quản lý thêm, sửa, xem danh sách và khóa tài khoản nhân viên.

## 2. GUI / Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyNhanVien.cs`

### Control

| Control | Kiểu | Vai trò |
|---|---|---|
| `dgvNhanVien` | `DataGridView` | Danh sách nhân viên. |
| `txtHoTen` | `TextBox` | Họ tên. |
| `txtTenDangNhap` | `TextBox` | Tên đăng nhập hiển thị. |
| `txtEmail` | `TextBox` | Email. |
| `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| `txtMatKhau` | `TextBox` | Mật khẩu mới, bỏ trống nếu không đổi. |
| `cboVaiTro` | `ComboBox` | Vai trò, lấy từ DB. |
| `cboChiNhanh` | `ComboBox` | Chi nhánh, lấy từ DB. |
| `cboTrangThai` | `ComboBox` | Trạng thái. |
| `dtpNgayVaoLam` | `DateTimePicker` | Ngày vào làm hiển thị. |
| `btnMoi` | `Button` | Clear form. |
| `btnKhoa` | `Button` | Khóa tài khoản. |
| `btnLuu` | `Button` | Lưu nhân viên. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Dựng màn hình. |
| `LoadCombos()` | Nạp vai trò, trạng thái, chi nhánh. |
| `LoadData()` | Nạp danh sách nhân viên. |
| `FillSelectedRow()` | Đổ dòng đang chọn lên form. |
| `ClearForm()` | Reset form. |
| `BtnLuu_Click(sender, e)` | Tạo `NhanVienDTO`, gọi BLL lưu. |
| `BtnKhoa_Click(sender, e)` | Gọi BLL khóa tài khoản. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/QuanTriHeThong/NhanVienBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách nhân viên. |
| `LayDanhMucVaiTro()` | Lấy danh mục vai trò. |
| `Luu(nhanVien, matKhauMoi)` | Validate thông tin và gọi DAL lưu. |
| `KhoaTaiKhoan(maNhanVien)` | Validate mã nhân viên và gọi DAL khóa. |

BLL dùng thêm:

| Class/Hàm | Vai trò |
|---|---|
| `ChiNhanhBLL.LayDanhSach()` | Nạp combobox chi nhánh. |

## 4. DAL - Data Access layer

File: `DAL/QuanTriHeThong/NhanVienDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT nhân viên join chi nhánh. |
| `LayDanhMucVaiTro()` | SELECT DISTINCT vai trò. |
| `Luu(nhanVien)` | UPDATE nếu có mã nhân viên, INSERT nếu thêm mới. |
| `KhoaTaiKhoan(maNhanVien)` | UPDATE `MatKhau = 'LOCKED'`. |
| `TaoMaMoi()` | Sinh mã `NVxxx`. |

File dùng thêm: `DAL/QuanTriHeThong/ChiNhanhDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT chi nhánh cho combobox. |

## 5. DTO và Database

DTO: `DTO/NhanVienDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaNhanVien` | Mã nhân viên. |
| `HoTen`, `Email`, `SoDienThoai` | Thông tin cá nhân. |
| `TenDangNhap` | Tên đăng nhập hiển thị. |
| `VaiTro` | Vai trò phân quyền. |
| `MaChiNhanh`, `TenChiNhanh` | Chi nhánh làm việc. |
| `MatKhauHash` | Mật khẩu/hash lưu xuống DB. |
| `TrangThai`, `NgayVaoLam` | Trạng thái và ngày vào làm. |

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Nhan_Vien` | Lưu nhân viên, vai trò, mật khẩu. |
| `Chi_Nhanh` | Chọn và hiển thị chi nhánh. |

## 6. Sequence để vẽ

### Thêm/sửa nhân viên

1. Actor mở `frmQuanLyNhanVien`.
2. GUI gọi `LoadCombos()` và `LoadData()`.
3. `LoadCombos()` gọi `NhanVienBLL.LayDanhMucVaiTro()` và `ChiNhanhBLL.LayDanhSach()`.
4. `LoadData()` gọi `NhanVienBLL.LayDanhSach()`.
5. DAL trả dữ liệu cho GUI bind.
6. Actor nhập hoặc chọn nhân viên.
7. Nếu chọn dòng, GUI gọi `FillSelectedRow()`.
8. Actor bấm `Lưu`.
9. GUI tạo `NhanVienDTO`.
10. GUI gọi `NhanVienBLL.Luu(dto, matKhauMoi)`.
11. BLL validate họ tên, email, vai trò, chi nhánh, mật khẩu nếu thêm mới.
12. BLL set `MatKhauHash` nếu có mật khẩu mới.
13. BLL gọi `NhanVienDAL.Luu(dto)`.
14. DAL UPDATE hoặc INSERT `Nhan_Vien`.
15. GUI reload danh sách và hiển thị kết quả.

### Khóa tài khoản nhân viên

1. Actor chọn nhân viên.
2. Actor bấm `Khóa tài khoản`.
3. GUI gọi `NhanVienBLL.KhoaTaiKhoan(_maNhanVien)`.
4. BLL validate mã nhân viên.
5. BLL gọi `NhanVienDAL.KhoaTaiKhoan(maNhanVien)`.
6. DAL UPDATE `Nhan_Vien.MatKhau = 'LOCKED'`.
7. GUI reload danh sách và hiển thị kết quả.

## 7. Ghi chú để vẽ 3-layer

- Khóa tài khoản là soft-delete theo mật khẩu `LOCKED`, không xóa nhân viên.
- `TrangThai` trong danh sách được suy ra từ `MatKhau`: `LOCKED` là nghỉ việc, còn lại là đang làm.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình quản lý nhân viên đặt form thông tin ở phía trên và danh sách nhân viên ở phía dưới. Quản lý có thể nhập mới hoặc chọn một nhân viên trong bảng để chỉnh sửa, phù hợp với thói quen thao tác CRUD.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Ô mật khẩu có placeholder `Bỏ trống khi không đổi mật khẩu`, giúp tránh việc vô tình đổi mật khẩu khi chỉnh thông tin khác. Nút `Khóa tài khoản` không xóa nhân viên mà chỉ khóa đăng nhập bằng trạng thái đặc biệt trong DB.

**Tính nhất quán:** Thông tin văn bản dùng `TextBox`, vai trò/chi nhánh/trạng thái dùng `ComboBox`, ngày vào làm dùng `DateTimePicker`, danh sách dùng `DataGridView`. Nút `Lưu` là hành động chính, nút `Khóa tài khoản` dùng màu cảnh báo.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Quản lý mở màn hình `Quản lý nhân viên`.

**Bước 2:** Hệ thống nạp danh mục vai trò, chi nhánh và danh sách nhân viên.

**Bước 3:** Quản lý nhập nhân viên mới hoặc chọn một nhân viên trong bảng để chỉnh sửa.

**Bước 4:** Nếu thêm mới, quản lý nhập mật khẩu ban đầu cho nhân viên.

**Bước 5:** Quản lý nhấn `Lưu`.

**Bước 6:** Hệ thống kiểm tra họ tên, email, vai trò, chi nhánh và mật khẩu nếu là nhân viên mới.

**Bước 7:** Nếu hợp lệ, hệ thống thêm mới hoặc cập nhật nhân viên trong DB.

**Bước 8:** Nếu cần khóa tài khoản, quản lý chọn nhân viên và nhấn `Khóa tài khoản`.

**Bước 9:** Hệ thống cập nhật mật khẩu của nhân viên thành `LOCKED`, reload danh sách và hiển thị trạng thái mới.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TextBox` dùng cho họ tên, tên đăng nhập, email, số điện thoại và mật khẩu. `ComboBox` dùng cho vai trò, chi nhánh và trạng thái nhằm tránh nhập sai danh mục. `DateTimePicker` chuẩn hóa ngày vào làm. `DataGridView` hỗ trợ chọn nhanh nhân viên cần sửa hoặc khóa.

**Logic bắt lỗi:** `NhanVienBLL.Luu()` kiểm tra họ tên, email, vai trò, chi nhánh và yêu cầu mật khẩu nếu thêm nhân viên mới. `NhanVienBLL.KhoaTaiKhoan()` kiểm tra phải có mã nhân viên đang chọn.

**Logic nghiệp vụ:** Khi lưu, GUI tạo `NhanVienDTO`, BLL validate rồi gọi `NhanVienDAL.Luu()`. DAL UPDATE nếu nhân viên đã tồn tại hoặc INSERT nếu là nhân viên mới. Khi khóa tài khoản, BLL gọi `NhanVienDAL.KhoaTaiKhoan()`, DAL cập nhật `Nhan_Vien.MatKhau = 'LOCKED'`. Danh sách nhân viên suy ra trạng thái từ giá trị mật khẩu này.

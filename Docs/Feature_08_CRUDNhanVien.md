# Feature 08 - CRUD Nhân viên

## 1. Mục đích

Cho phép quản lý thêm, sửa, xem danh sách và khóa tài khoản nhân viên.

## 2. GUI - Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyNhanVien.cs`

### Thành phần giao diện

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `dgvNhanVien` | `DataGridView` | Danh sách nhân viên. |
| `txtHoTen` | `TextBox` | Họ tên nhân viên. |
| `txtTenDangNhap` | `TextBox` | Tên đăng nhập, hiện map theo `MaNV` trong DAL. |
| `txtEmail` | `TextBox` | Email. |
| `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| `txtMatKhau` | `TextBox` | Mật khẩu mới, bỏ trống nếu không đổi. |
| `cboVaiTro` | `ComboBox` | Vai trò, lấy động từ DB có fallback. |
| `cboChiNhanh` | `ComboBox` | Chi nhánh, lấy từ `ChiNhanhBLL.LayDanhSach`. |
| `cboTrangThai` | `ComboBox` | `Đang làm`, `Nghỉ việc`. |
| `dtpNgayVaoLam` | `DateTimePicker` | Ngày vào làm, hiện chỉ dùng trên GUI. |
| `btnMoi` | `Button` | Clear form. |
| `btnKhoa` | `Button` | Khóa tài khoản. |
| `btnLuu` | `Button` | Lưu nhân viên. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo màn hình. |
| `LoadCombos()` | Nạp vai trò, trạng thái, chi nhánh. |
| `LoadData()` | Nạp danh sách nhân viên. |
| `FillSelectedRow()` | Đổ dòng đang chọn lên form. |
| `ClearForm()` | Reset form. |
| `BtnLuu_Click(sender, e)` | Tạo `NhanVienDTO`, gọi BLL lưu. |
| `BtnKhoa_Click(sender, e)` | Gọi BLL khóa tài khoản. |
| `AddField(...)`, `FixedButton(...)` | Hàm hỗ trợ UI. |

## 3. DTO liên quan

File: `DTO/NhanVienDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaNhanVien` | Mã nhân viên. |
| `HoTen` | Họ tên. |
| `TenDangNhap` | Tên đăng nhập hiển thị. |
| `Email` | Email. |
| `SoDienThoai` | Số điện thoại. |
| `VaiTro` | Vai trò phân quyền. |
| `MaChiNhanh`, `TenChiNhanh` | Chi nhánh làm việc. |
| `MatKhauHash` | Mật khẩu/hash lưu xuống DB. |
| `TrangThai` | Trạng thái tài khoản. |
| `NgayVaoLam` | Ngày vào làm hiển thị. |

## 4. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/NhanVienBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách nhân viên. |
| `LayDanhMucVaiTro()` | Lấy danh mục vai trò từ DB. |
| `Luu(nhanVien, matKhauMoi)` | Validate thông tin, set `MatKhauHash`, gọi DAL lưu. |
| `KhoaTaiKhoan(maNhanVien)` | Validate mã nhân viên, gọi DAL khóa tài khoản. |

BLL liên quan:

| BLL | Hàm | Vai trò |
|---|---|---|
| `ChiNhanhBLL` | `LayDanhSach()` | Nạp combobox chi nhánh. |

## 5. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/NhanVienDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT danh sách nhân viên join chi nhánh. |
| `LayDanhMucVaiTro()` | SELECT DISTINCT vai trò. |
| `Luu(nhanVien)` | UPDATE nếu tồn tại, INSERT nếu thêm mới. |
| `KhoaTaiKhoan(maNhanVien)` | UPDATE `MatKhau = 'LOCKED'`. |
| `TaoMaMoi()` | Sinh mã nhân viên `NVxxx`. |
| `DocTheoTenDangNhapHoacEmail(login)` | Dùng cho đăng nhập. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Nhan_Vien` | Lưu thông tin nhân viên, vai trò, mật khẩu. |
| `Chi_Nhanh` | Join để hiển thị và chọn chi nhánh. |

## 7. Sequence để tự vẽ

### Thêm/sửa nhân viên

1. Quản lý mở `frmQuanLyNhanVien`.
2. GUI gọi `LoadCombos()` và `LoadData()`.
3. Quản lý nhập/chỉnh thông tin nhân viên.
4. Quản lý bấm `Lưu`.
5. `BtnLuu_Click()` tạo `NhanVienDTO`.
6. GUI gọi `NhanVienBLL.Luu(dto, txtMatKhau.Text)`.
7. BLL kiểm tra họ tên, email, vai trò, chi nhánh.
8. Nếu thêm mới mà chưa nhập mật khẩu, BLL báo lỗi.
9. Nếu hợp lệ, BLL set `nhanVien.MatKhauHash`.
10. BLL gọi `NhanVienDAL.Luu(dto)`.
11. DAL UPDATE nếu tồn tại, INSERT nếu thêm mới.
12. DAL trả `MaNhanVien`.
13. GUI gọi `LoadData()` và hiển thị thành công.

### Khóa tài khoản

1. Quản lý chọn nhân viên trên `dgvNhanVien`.
2. GUI gọi `FillSelectedRow()`.
3. Quản lý bấm `Khóa tài khoản`.
4. GUI gọi `NhanVienBLL.KhoaTaiKhoan(_maNhanVien)`.
5. BLL kiểm tra mã nhân viên.
6. BLL gọi `NhanVienDAL.KhoaTaiKhoan(maNhanVien)`.
7. DAL UPDATE `Nhan_Vien.MatKhau = 'LOCKED'`.
8. GUI gọi `LoadData()` và hiển thị thành công.

## 8. Ghi chú

- Khóa tài khoản được biểu diễn bằng cách đặt `MatKhau = 'LOCKED'`.
- `TrangThai` trong danh sách được suy ra từ mật khẩu: nếu `LOCKED` thì `Nghỉ việc`, ngược lại `Đang làm`.
- Code hiện tại chưa ghi `NgayVaoLam` xuống DB; DAL trả `GETDATE()` để hiển thị.

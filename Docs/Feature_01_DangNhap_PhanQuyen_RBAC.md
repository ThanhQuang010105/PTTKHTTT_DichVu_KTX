# Feature 01 - Đăng nhập và phân quyền RBAC

## 1. Mục đích

Cho phép nhân viên đăng nhập bằng mã nhân viên/email/alias vai trò, kiểm tra mật khẩu, lưu thông tin người dùng hiện tại và mở Dashboard theo quyền.

## 2. GUI - Presentation layer

File: `UI/QuanTriHeThong/frmDangNhap.cs`

### Thành phần giao diện

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `txtTenDangNhap` | `TextBox` | Nhập tên đăng nhập, email hoặc alias `sale`, `quanly`, `ketoan`. |
| `txtMatKhau` | `TextBox` | Nhập mật khẩu, mặc định che bằng `PasswordChar = '*'`. |
| `chkHienMatKhau` | `CheckBox` | Bật/tắt hiển thị mật khẩu. |
| `btnDangNhap` | `Button` | Thực hiện đăng nhập. |
| `lblThongBao` | `Label` | Hiển thị lỗi hoặc gợi ý đăng nhập demo. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo màn hình đăng nhập, control, sự kiện. |
| `BtnDangNhap_Click(sender, e)` | Gọi `AuthBLL.DangNhap`, xử lý kết quả, set `SessionContext.CurrentUser`, mở `frmDashboard`. |

## 3. DTO liên quan

File: `DTO/NhanVienDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaNhanVien`, `TenDangNhap`, `HoTen`, `Email`, `SoDienThoai` | Thông tin nhận diện nhân viên. |
| `VaiTro` | Dùng cho RBAC trên Dashboard. |
| `MaChiNhanh`, `TenChiNhanh` | Chi nhánh làm việc. |
| `MatKhauHash` | Mật khẩu lưu trong DB hoặc hash mật khẩu. |
| `TrangThai` | Kiểm tra tài khoản còn hoạt động hay bị khóa/nghỉ việc. |

File: `UI/Common/SessionContext.cs`

| Thành phần | Vai trò |
|---|---|
| `CurrentUser` | Lưu nhân viên đã đăng nhập trong phiên hiện tại. |

## 4. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/AuthBLL.cs`

| Hàm | Vai trò |
|---|---|
| `DangNhap(tenDangNhapHoacEmail, matKhau)` | Kiểm tra input, đọc nhân viên từ DAL, kiểm tra trạng thái, kiểm tra mật khẩu, trả `DangNhapResult`. |
| `HashMatKhau(matKhau)` | Băm mật khẩu bằng SHA-256. |
| `MapNhanVien(row)` | Map `DataRow` từ DB sang `NhanVienDTO`. |

Class kết quả: `DangNhapResult`

| Hàm/thuộc tính | Vai trò |
|---|---|
| `ThanhCong` | Đăng nhập thành công/thất bại. |
| `NhanVien` | DTO nhân viên nếu đăng nhập thành công. |
| `ThongBao` | Thông báo cho GUI. |
| `TaoThanhCong(nhanVien)` | Tạo kết quả thành công. |
| `ThatBai(thongBao)` | Tạo kết quả thất bại. |

## 5. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/NhanVienDAL.cs`

| Hàm | Vai trò |
|---|---|
| `DocTheoTenDangNhapHoacEmail(tenDangNhapHoacEmail)` | Tìm nhân viên theo `MaNV`, email, phần trước `@` của email hoặc alias vai trò. |

File: `DAL/DBConnection.cs`

| Hàm | Vai trò |
|---|---|
| `ExecuteSqlQuery(sql, parameters)` | Thực thi SELECT dạng SQL text và trả về `DataTable`. |

## 6. Bảng CSDL liên quan

| Bảng | Cột dùng chính |
|---|---|
| `Nhan_Vien` | `MaNV`, `MaCN`, `HoTen`, `SDT`, `Email`, `VaiTro`, `MatKhau`. |
| `Chi_Nhanh` | `MaCN`, `TenChiNhanh`. |

## 7. Sequence để tự vẽ

1. Nhân viên nhập tên đăng nhập/email và mật khẩu trên `frmDangNhap`.
2. Nhân viên bấm `Đăng nhập`.
3. `frmDangNhap.BtnDangNhap_Click()` gọi `AuthBLL.DangNhap(tenDangNhapHoacEmail, matKhau)`.
4. `AuthBLL` kiểm tra input rỗng.
5. `AuthBLL` gọi `NhanVienDAL.DocTheoTenDangNhapHoacEmail(login)`.
6. `NhanVienDAL` truy vấn `Nhan_Vien` join `Chi_Nhanh`.
7. `AuthBLL` map dữ liệu sang `NhanVienDTO`.
8. `AuthBLL` kiểm tra `TrangThai`.
9. `AuthBLL` hash mật khẩu nhập bằng `HashMatKhau`.
10. `AuthBLL` so sánh mật khẩu nhập với `MatKhauHash` trong DB.
11. Nếu sai, trả `DangNhapResult.ThatBai`.
12. Nếu đúng, trả `DangNhapResult.TaoThanhCong(nhanVien)`.
13. GUI set `SessionContext.CurrentUser = result.NhanVien`.
14. GUI mở `frmDashboard(result.NhanVien)`.

## 8. Ghi chú RBAC

Phân quyền menu không nằm trong `AuthBLL`; nó nằm ở `frmDashboard`:

- `AddMenuButton(menu, text, open, roles)`
- `IsInRole(roles)`
- `NormalizeRole(role)`

Vai trò được chuẩn hóa để hỗ trợ cả `Quản lý`, `QuanLy`, khác dấu hoặc không dấu.

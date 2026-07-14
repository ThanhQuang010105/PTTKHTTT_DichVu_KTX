# UC 01 - Đăng nhập

## 1. Phạm vi use case

Actor đăng nhập vào hệ thống bằng tên đăng nhập/email/alias và mật khẩu. Nếu hợp lệ, hệ thống lưu user hiện tại và mở Dashboard.

## 2. GUI / Presentation layer

File: `UI/QuanTriHeThong/frmDangNhap.cs`

### Control

| Control | Kiểu | Vai trò |
|---|---|---|
| `txtTenDangNhap` | `TextBox` | Nhập mã nhân viên, email hoặc alias `sale`, `quanly`, `ketoan`. |
| `txtMatKhau` | `TextBox` | Nhập mật khẩu, có `PasswordChar = '*'`. |
| `chkHienMatKhau` | `CheckBox` | Bật/tắt hiện mật khẩu. |
| `btnDangNhap` | `Button` | Gọi xử lý đăng nhập. |
| `lblThongBao` | `Label` | Hiển thị thông báo lỗi/gợi ý. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `frmDangNhap()` | Constructor, gọi `InitializeComponent`. |
| `InitializeComponent()` | Dựng màn hình và gán event. |
| `BtnDangNhap_Click(sender, e)` | Gọi `AuthBLL.DangNhap`, xử lý thành công/thất bại, mở Dashboard. |
| `chkHienMatKhau.CheckedChanged` | Đổi `PasswordChar` của `txtMatKhau`. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/QuanTriHeThong/AuthBLL.cs`

| Class/Hàm | Vai trò |
|---|---|
| `AuthBLL` | Lớp nghiệp vụ đăng nhập. |
| `DangNhap(tenDangNhapHoacEmail, matKhau)` | Kiểm tra input, đọc nhân viên, kiểm tra trạng thái và mật khẩu. |
| `HashMatKhau(matKhau)` | Hash mật khẩu bằng SHA-256. |
| `MapNhanVien(row)` | Map `DataRow` sang `NhanVienDTO`. |
| `DangNhapResult` | Object kết quả trả về GUI. |
| `DangNhapResult.TaoThanhCong(nhanVien)` | Tạo kết quả đăng nhập thành công. |
| `DangNhapResult.ThatBai(thongBao)` | Tạo kết quả đăng nhập thất bại. |

## 4. DAL - Data Access layer

File: `DAL/QuanTriHeThong/NhanVienDAL.cs`

| Hàm | Vai trò |
|---|---|
| `DocTheoTenDangNhapHoacEmail(tenDangNhapHoacEmail)` | SELECT nhân viên theo `MaNV`, email, phần trước `@` của email hoặc alias vai trò. |

File: `DAL/DBConnection.cs`

| Hàm | Vai trò |
|---|---|
| `ExecuteSqlQuery(sql, parameters)` | Thực thi câu SELECT và trả `DataTable`. |

## 5. DTO và Database

DTO: `DTO/NhanVienDTO.cs`

| Thuộc tính chính | Vai trò |
|---|---|
| `MaNhanVien`, `TenDangNhap`, `HoTen`, `Email` | Thông tin định danh. |
| `VaiTro` | Vai trò dùng cho phân quyền dashboard. |
| `MatKhauHash` | Mật khẩu/hash đọc từ DB. |
| `TrangThai` | Kiểm tra tài khoản còn hoạt động hay bị khóa. |

Bảng DB:

| Bảng | Cột dùng chính |
|---|---|
| `Nhan_Vien` | `MaNV`, `MaCN`, `HoTen`, `SDT`, `Email`, `VaiTro`, `MatKhau`. |
| `Chi_Nhanh` | `MaCN`, `TenChiNhanh`. |

## 6. Sequence để vẽ

1. Actor nhập tên đăng nhập/email và mật khẩu.
2. Actor bấm `Đăng nhập`.
3. `frmDangNhap.BtnDangNhap_Click()` gọi `AuthBLL.DangNhap(login, matKhau)`.
4. `AuthBLL` kiểm tra input rỗng.
5. `AuthBLL` gọi `NhanVienDAL.DocTheoTenDangNhapHoacEmail(login)`.
6. `NhanVienDAL` gọi `DBConnection.ExecuteSqlQuery`.
7. DB trả `DataTable` nhân viên.
8. `AuthBLL.MapNhanVien(row)` tạo `NhanVienDTO`.
9. `AuthBLL` kiểm tra `TrangThai`.
10. `AuthBLL.HashMatKhau(matKhau)` tạo hash.
11. `AuthBLL` so sánh mật khẩu nhập/hash với `MatKhauHash`.
12. Nếu sai, trả `DangNhapResult.ThatBai`.
13. Nếu đúng, trả `DangNhapResult.TaoThanhCong`.
14. GUI set `SessionContext.CurrentUser`.
15. GUI mở `frmDashboard(result.NhanVien)`.

## 7. Ghi chú để vẽ 3-layer

- Có thể vẽ `DangNhapResult` như response object từ BLL về GUI.
- Dashboard phân quyền là hậu xử lý sau đăng nhập, không phải phần chính của UC đăng nhập.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình đăng nhập được thiết kế theo luồng đơn giản từ trên xuống dưới: nhập tài khoản, nhập mật khẩu, chọn tùy chọn hiện mật khẩu nếu cần, sau đó nhấn nút đăng nhập. Cách bố trí này phù hợp với thói quen thao tác nhanh của nhân viên trước khi bắt đầu ca làm việc.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Ô mật khẩu mặc định được che để đảm bảo riêng tư, nhưng có checkbox `Hiện mật khẩu` giúp người dùng tự kiểm tra lại khi nhập sai. Khi đăng nhập thất bại, hệ thống chỉ hiển thị thông báo lỗi trên `lblThongBao`, không đóng màn hình và không xóa tài khoản đã nhập.

**Tính nhất quán:** Nút hành động chính `Đăng nhập` sử dụng màu primary, được đặt sau hai trường nhập liệu chính. Trường `txtMatKhau` dùng `PasswordChar` giống chuẩn form đăng nhập phổ biến, giúp người dùng nhận biết ngay đây là thông tin nhạy cảm.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Nhân viên nhập tên đăng nhập, email hoặc alias vai trò vào `txtTenDangNhap`.

**Bước 2:** Nhân viên nhập mật khẩu vào `txtMatKhau`. Nếu cần kiểm tra lại mật khẩu, nhân viên tick `chkHienMatKhau`.

**Bước 3:** Nhân viên nhấn nút `Đăng nhập`.

**Bước 4:** Hệ thống kiểm tra thông tin đăng nhập qua `AuthBLL.DangNhap`.

**Bước 5:** Nếu thông tin không hợp lệ, hệ thống hiển thị thông báo lỗi. Nếu hợp lệ, hệ thống lưu `SessionContext.CurrentUser` và mở màn hình Dashboard.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TextBox` được dùng cho tài khoản và mật khẩu vì đây là dữ liệu dạng chuỗi. `CheckBox` được dùng cho tùy chọn hiện mật khẩu vì đây là trạng thái bật/tắt. `Label` được dùng để hiển thị thông báo ngay trên form, tránh mở nhiều hộp thoại gây gián đoạn.

**Logic bắt lỗi:** `AuthBLL.DangNhap()` chặn luồng nếu tên đăng nhập hoặc mật khẩu rỗng. Nếu không tìm thấy nhân viên hoặc mật khẩu không khớp, BLL trả `DangNhapResult.ThatBai`.

**Logic nghiệp vụ:** DAL đọc nhân viên bằng `NhanVienDAL.DocTheoTenDangNhapHoacEmail()`. BLL map dữ liệu sang `NhanVienDTO`, kiểm tra trạng thái tài khoản, hash mật khẩu nhập bằng `HashMatKhau()`, sau đó so sánh với mật khẩu trong DB. Chỉ khi tất cả điều kiện hợp lệ, GUI mới mở `frmDashboard`.

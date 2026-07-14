# Feature 10 - Hash mật khẩu

## 1. Mục đích

Băm mật khẩu bằng SHA-256 khi đăng nhập để hỗ trợ kiểm tra mật khẩu đã hash trong database.

## 2. GUI liên quan

### Màn hình đăng nhập

File: `UI/QuanTriHeThong/frmDangNhap.cs`

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `txtMatKhau` | `TextBox` | Nhập mật khẩu, che bằng `PasswordChar`. |
| `chkHienMatKhau` | `CheckBox` | Hiện/ẩn mật khẩu. |
| `btnDangNhap` | `Button` | Gọi `AuthBLL.DangNhap`. |

### Màn hình quản lý nhân viên

File: `UI/QuanTriHeThong/frmQuanLyNhanVien.cs`

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `txtMatKhau` | `TextBox` | Nhập mật khẩu mới khi thêm/sửa nhân viên. |
| `btnLuu` | `Button` | Gọi `NhanVienBLL.Luu(dto, matKhauMoi)`. |

## 3. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/AuthBLL.cs`

| Hàm | Vai trò |
|---|---|
| `HashMatKhau(matKhau)` | Băm chuỗi mật khẩu bằng SHA-256, trả chuỗi hex lowercase. |
| `DangNhap(tenDangNhapHoacEmail, matKhau)` | Hash mật khẩu nhập và so sánh với mật khẩu trong DB. |

File: `BLL/QuanTriHeThong/NhanVienBLL.cs`

| Hàm | Vai trò hiện tại |
|---|---|
| `Luu(nhanVien, matKhauMoi)` | Kiểm tra dữ liệu và gán `nhanVien.MatKhauHash = matKhauMoi` nếu có nhập mật khẩu mới. |

## 4. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/NhanVienDAL.cs`

| Hàm | Vai trò |
|---|---|
| `DocTheoTenDangNhapHoacEmail(login)` | Đọc `MatKhau AS MatKhauHash` để BLL đăng nhập kiểm tra. |
| `Luu(nhanVien)` | Lưu giá trị `MatKhauHash` vào cột `Nhan_Vien.MatKhau`. |
| `KhoaTaiKhoan(maNhanVien)` | Set `MatKhau = 'LOCKED'`. |

## 5. Bảng CSDL liên quan

| Bảng | Cột | Vai trò |
|---|---|---|
| `Nhan_Vien` | `MatKhau` | Lưu mật khẩu/hash hoặc `LOCKED`. |

## 6. Sequence để tự vẽ

### Đăng nhập với hash

1. Người dùng nhập mật khẩu trên `frmDangNhap`.
2. GUI gọi `AuthBLL.DangNhap(login, matKhau)`.
3. BLL gọi `NhanVienDAL.DocTheoTenDangNhapHoacEmail(login)`.
4. DAL trả nhân viên và `MatKhauHash`.
5. BLL gọi `HashMatKhau(matKhau)`.
6. BLL so sánh:
   - `nhanVien.MatKhauHash == matKhau`, hoặc
   - `nhanVien.MatKhauHash == hash`.
7. Nếu khớp, đăng nhập thành công.
8. Nếu không khớp, đăng nhập thất bại.

### Lưu mật khẩu nhân viên

1. Quản lý nhập mật khẩu mới ở `frmQuanLyNhanVien`.
2. GUI gọi `NhanVienBLL.Luu(dto, matKhauMoi)`.
3. BLL kiểm tra dữ liệu.
4. BLL gán `dto.MatKhauHash = matKhauMoi` nếu có nhập.
5. BLL gọi `NhanVienDAL.Luu(dto)`.
6. DAL lưu `@MatKhau` vào `Nhan_Vien.MatKhau`.

## 7. Ghi chú quan trọng

- Code đăng nhập đã có hàm hash SHA-256.
- Code đăng nhập đang hỗ trợ cả mật khẩu plain text và mật khẩu đã hash để tương thích dữ liệu demo.
- Code lưu nhân viên hiện tại chưa gọi `AuthBLL.HashMatKhau(matKhauMoi)` trước khi lưu. Nếu muốn feature “Hash mật khẩu” khớp hoàn toàn khi tạo/sửa nhân viên, nên chỉnh `NhanVienBLL.Luu` thành:

```csharp
nhanVien.MatKhauHash = string.IsNullOrWhiteSpace(matKhauMoi)
    ? null
    : AuthBLL.HashMatKhau(matKhauMoi);
```

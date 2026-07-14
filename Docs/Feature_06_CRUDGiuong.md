# Feature 06 - CRUD Giường

## 1. Mục đích

Cho phép quản lý thêm, sửa, xem danh sách và ngừng sử dụng giường trong phòng. Feature này nằm trong tab `Giường trong phòng` của `frmQuanLyPhong`.

## 2. GUI - Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyPhong.cs`

### Thành phần giao diện trong tab Giường

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `dgvGiuong` | `DataGridView` | Hiển thị danh sách giường theo phòng. |
| `cboPhongGiuong` | `ComboBox` | Chọn phòng để xem/thêm giường. |
| `txtTenGiuong` | `TextBox` | Nhập tên giường. |
| `nudGiaGiuong` | `NumericUpDown` | Giá thuê giường. |
| `cboTrangThaiGiuong` | `ComboBox` | Trạng thái: `Trống`, `Đã thuê`, `Ngừng sử dụng`. |
| `btnLuu` | `Button` | Lưu giường. |
| `btnXoa` | `Button` | Chuyển giường sang `Ngừng sử dụng`. |
| `btnMoi` | `Button` | Clear form. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm trong GUI liên quan đến giường

| Hàm | Vai trò |
|---|---|
| `CreateGiuongLayout()` | Tạo layout tab giường. |
| `LoadPhong()` | Nạp danh sách phòng vào `cboPhongGiuong`. |
| `LoadGiuong()` | Lấy danh sách giường theo phòng đang chọn. |
| `FillSelectedGiuong()` | Khi chọn dòng, đổ dữ liệu lên form. |
| `ClearGiuong()` | Reset form giường. |
| `BtnLuuGiuong_Click(sender, e)` | Tạo `GiuongDTO`, gọi `GiuongBLL.Luu`. |
| `BtnXoaGiuong_Click(sender, e)` | Gọi `GiuongBLL.Xoa`. |

## 3. DTO liên quan

File: `DTO/GiuongDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaGiuong` | Mã giường, rỗng khi thêm mới. |
| `MaPhong` | Phòng chứa giường. |
| `TenGiuong` | Tên giường. |
| `GiaThue` | Giá thuê giường. |
| `TrangThaiGiuong` | Trạng thái giường. |

## 4. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/GiuongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSachTheoPhong(maPhong)` | Lấy danh sách giường theo phòng. |
| `Luu(giuong)` | Validate và gọi DAL thêm/sửa giường. |
| `Xoa(maGiuong)` | Validate mã giường và gọi DAL ngừng sử dụng. |
| `TraCuuGiuongKhaDung(tieuChi)` | Alias phục vụ feature tra cứu. |
| `TraCuuGiuongPhuHop(tieuChi)` | Tra cứu giường phù hợp cho feature tra cứu. |

BLL liên quan:

| BLL | Hàm | Vai trò |
|---|---|---|
| `PhongBLL` | `LayDanhSach()` | Nạp combobox phòng. |

## 5. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/GiuongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSachTheoPhong(maPhong)` | SELECT giường theo mã phòng. |
| `Luu(giuong)` | Nếu `MaGiuong` tồn tại thì UPDATE, ngược lại INSERT. |
| `Xoa(maGiuong)` | UPDATE `TrangThaiGiuong = Ngừng sử dụng`. |
| `TaoMaMoi(maPhong)` | Sinh mã giường theo phòng. |
| `DocDanhSachGiuongTrong(tieuChi)` | Dùng cho feature tra cứu giường phù hợp. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Phong` | Chọn phòng chứa giường. |
| `Giuong` | Lưu thông tin giường. |

## 7. Sequence để tự vẽ

### Thêm/sửa giường

1. Quản lý mở `frmQuanLyPhong`, tab `Giường trong phòng`.
2. GUI nạp `cboPhongGiuong` từ `LoadPhong()`.
3. Quản lý chọn phòng.
4. GUI gọi `LoadGiuong()` để nạp danh sách giường.
5. Quản lý nhập/chỉnh thông tin giường.
6. Quản lý bấm `Lưu giường`.
7. `BtnLuuGiuong_Click()` tạo `GiuongDTO`.
8. GUI gọi `GiuongBLL.Luu(dto)`.
9. BLL kiểm tra mã phòng, tên giường, giá thuê, trạng thái.
10. Nếu lỗi, BLL throw exception, GUI hiển thị lỗi.
11. Nếu hợp lệ, BLL gọi `GiuongDAL.Luu(dto)`.
12. DAL UPDATE nếu giường đã tồn tại, INSERT nếu thêm mới.
13. DAL trả `MaGiuong`.
14. GUI gọi `LoadGiuong()` và hiển thị thành công.

### Ngừng sử dụng giường

1. Quản lý chọn giường trên `dgvGiuong`.
2. GUI gọi `FillSelectedGiuong()`.
3. Quản lý bấm `Xóa giường`.
4. GUI gọi `GiuongBLL.Xoa(_maGiuong)`.
5. BLL kiểm tra mã giường.
6. BLL gọi `GiuongDAL.Xoa(maGiuong)`.
7. DAL UPDATE `Giuong.TrangThaiGiuong = Ngừng sử dụng`.
8. GUI gọi `LoadGiuong()` và `ClearGiuong()`.

## 8. Ghi chú

- Xóa giường là soft-delete, không xóa vật lý.
- Mã giường sinh theo phòng, ví dụ dựa trên mã phòng `P101` sẽ tạo prefix tương ứng trong `TaoMaMoi`.

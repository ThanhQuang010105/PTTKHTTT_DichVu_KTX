# Feature 07 - CRUD Chi nhánh

## 1. Mục đích

Cho phép quản lý thêm, sửa, xem danh sách và ngừng hoạt động chi nhánh.

## 2. GUI - Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyChiNhanh.cs`

### Thành phần giao diện

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `dgvChiNhanh` | `DataGridView` | Danh sách chi nhánh. |
| `txtTen` | `TextBox` | Tên chi nhánh. |
| `txtKhuVuc` | `TextBox` | Khu vực. |
| `txtDiaChi` | `TextBox` | Địa chỉ. |
| `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| `cboTrangThai` | `ComboBox` | `Hoạt động`, `Ngừng hoạt động`. |
| `btnMoi` | `Button` | Clear form để thêm mới. |
| `btnXoa` | `Button` | Chuyển trạng thái ngừng hoạt động. |
| `btnLuu` | `Button` | Lưu chi nhánh. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo màn hình. |
| `LoadData()` | Nạp danh sách chi nhánh. |
| `FillSelectedRow()` | Đổ dòng đang chọn lên form. |
| `ClearForm()` | Reset form. |
| `BtnLuu_Click(sender, e)` | Tạo `ChiNhanhDTO`, gọi BLL lưu. |
| `BtnXoa_Click(sender, e)` | Gọi BLL ngừng hoạt động chi nhánh. |
| `AddField(...)`, `FixedButton(...)` | Hàm hỗ trợ UI. |

## 3. DTO liên quan

File: `DTO/ChiNhanhDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaChiNhanh` | Mã chi nhánh, rỗng khi thêm mới. |
| `TenChiNhanh` | Tên chi nhánh. |
| `KhuVuc` | Khu vực. |
| `DiaChi` | Địa chỉ. |
| `SoDienThoai` | Số điện thoại. |
| `TrangThai` | Trạng thái hoạt động. |

## 4. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/ChiNhanhBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách chi nhánh. |
| `LayDanhMucKhuVuc()` | Lấy danh mục khu vực cho các combobox khác. |
| `Luu(chiNhanh)` | Validate tên, khu vực, địa chỉ rồi gọi DAL lưu. |
| `Xoa(maChiNhanh)` | Validate mã chi nhánh rồi gọi DAL ngừng hoạt động. |

## 5. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/ChiNhanhDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT danh sách chi nhánh. |
| `LayDanhMucKhuVuc()` | SELECT DISTINCT khu vực còn hoạt động. |
| `Luu(chiNhanh)` | UPDATE nếu mã tồn tại, INSERT nếu thêm mới. |
| `Xoa(maChiNhanh)` | UPDATE `TrangThai = Ngừng hoạt động`. |
| `TaoMaMoi()` | Sinh mã chi nhánh `CNxx`. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Chi_Nhanh` | Lưu thông tin chi nhánh. |

## 7. Sequence để tự vẽ

### Thêm/sửa chi nhánh

1. Quản lý mở `frmQuanLyChiNhanh`.
2. GUI gọi `LoadData()`.
3. Quản lý nhập/chỉnh thông tin.
4. Quản lý bấm `Lưu`.
5. `BtnLuu_Click()` tạo `ChiNhanhDTO`.
6. GUI gọi `ChiNhanhBLL.Luu(dto)`.
7. BLL validate tên chi nhánh, khu vực, địa chỉ.
8. Nếu lỗi, GUI hiển thị lỗi.
9. Nếu hợp lệ, BLL gọi `ChiNhanhDAL.Luu(dto)`.
10. DAL UPDATE nếu tồn tại, INSERT nếu thêm mới.
11. DAL trả `MaChiNhanh`.
12. GUI gọi `LoadData()` và hiển thị thành công.

### Ngừng hoạt động chi nhánh

1. Quản lý chọn chi nhánh trên `dgvChiNhanh`.
2. GUI gọi `FillSelectedRow()`.
3. Quản lý bấm `Xóa/Ngừng hoạt động`.
4. GUI gọi `ChiNhanhBLL.Xoa(_maChiNhanh)`.
5. BLL kiểm tra mã chi nhánh.
6. BLL gọi `ChiNhanhDAL.Xoa(maChiNhanh)`.
7. DAL UPDATE `Chi_Nhanh.TrangThai = Ngừng hoạt động`.
8. GUI gọi `LoadData()` và `ClearForm()`.

## 8. Ghi chú

- Xóa chi nhánh là soft-delete bằng trạng thái.
- `LayDanhMucKhuVuc()` được dùng bởi đăng ký thuê và tra cứu phòng/giường.

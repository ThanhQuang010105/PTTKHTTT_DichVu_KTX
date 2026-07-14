# UC 06 - Quản lý chi nhánh

## 1. Phạm vi use case

Quản lý thêm, sửa, xem danh sách và ngừng hoạt động chi nhánh.

## 2. GUI / Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyChiNhanh.cs`

### Control

| Control | Kiểu | Vai trò |
|---|---|---|
| `dgvChiNhanh` | `DataGridView` | Danh sách chi nhánh. |
| `txtTen` | `TextBox` | Tên chi nhánh. |
| `txtKhuVuc` | `TextBox` | Khu vực. |
| `txtDiaChi` | `TextBox` | Địa chỉ. |
| `txtSoDienThoai` | `TextBox` | Số điện thoại. |
| `cboTrangThai` | `ComboBox` | Trạng thái hoạt động. |
| `btnMoi` | `Button` | Clear form. |
| `btnXoa` | `Button` | Ngừng hoạt động chi nhánh. |
| `btnLuu` | `Button` | Lưu chi nhánh. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Dựng màn hình. |
| `LoadData()` | Gọi BLL lấy danh sách chi nhánh. |
| `FillSelectedRow()` | Đổ dòng đang chọn lên form. |
| `ClearForm()` | Reset form. |
| `BtnLuu_Click(sender, e)` | Tạo `ChiNhanhDTO`, gọi BLL lưu. |
| `BtnXoa_Click(sender, e)` | Gọi BLL ngừng hoạt động chi nhánh. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/QuanTriHeThong/ChiNhanhBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách chi nhánh. |
| `LayDanhMucKhuVuc()` | Lấy khu vực cho các màn hình khác. |
| `Luu(chiNhanh)` | Validate và gọi DAL lưu. |
| `Xoa(maChiNhanh)` | Validate mã chi nhánh và gọi DAL ngừng hoạt động. |

## 4. DAL - Data Access layer

File: `DAL/QuanTriHeThong/ChiNhanhDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT danh sách chi nhánh. |
| `LayDanhMucKhuVuc()` | SELECT DISTINCT khu vực còn hoạt động. |
| `Luu(chiNhanh)` | UPDATE nếu có mã chi nhánh, INSERT nếu thêm mới. |
| `Xoa(maChiNhanh)` | UPDATE `TrangThai = Ngừng hoạt động`. |
| `TaoMaMoi()` | Sinh mã `CNxx`. |

## 5. DTO và Database

DTO: `DTO/ChiNhanhDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaChiNhanh` | Mã chi nhánh. |
| `TenChiNhanh` | Tên chi nhánh. |
| `KhuVuc` | Khu vực. |
| `DiaChi` | Địa chỉ. |
| `SoDienThoai` | Số điện thoại. |
| `TrangThai` | Trạng thái. |

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Chi_Nhanh` | Lưu chi nhánh. |

## 6. Sequence để vẽ

### Thêm/sửa chi nhánh

1. Actor mở `frmQuanLyChiNhanh`.
2. GUI gọi `LoadData()`.
3. GUI gọi `ChiNhanhBLL.LayDanhSach()`.
4. BLL gọi `ChiNhanhDAL.LayDanhSach()`.
5. DAL SELECT DB và trả danh sách.
6. Actor nhập hoặc chọn chi nhánh.
7. Nếu chọn dòng, GUI gọi `FillSelectedRow()`.
8. Actor bấm `Lưu`.
9. GUI tạo `ChiNhanhDTO`.
10. GUI gọi `ChiNhanhBLL.Luu(dto)`.
11. BLL validate tên, khu vực, địa chỉ.
12. BLL gọi `ChiNhanhDAL.Luu(dto)`.
13. DAL UPDATE hoặc INSERT `Chi_Nhanh`.
14. GUI reload danh sách và hiển thị kết quả.

### Ngừng hoạt động chi nhánh

1. Actor chọn chi nhánh.
2. Actor bấm `Xóa/Ngừng hoạt động`.
3. GUI gọi `ChiNhanhBLL.Xoa(_maChiNhanh)`.
4. BLL validate mã chi nhánh.
5. BLL gọi `ChiNhanhDAL.Xoa(maChiNhanh)`.
6. DAL UPDATE `Chi_Nhanh.TrangThai = Ngừng hoạt động`.
7. GUI reload danh sách và clear form.

## 7. Ghi chú để vẽ 3-layer

- Xóa là soft-delete bằng trạng thái.
- `LayDanhMucKhuVuc()` là hàm phụ được UC đăng ký thuê và tra cứu dùng lại.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình quản lý chi nhánh có form nhập liệu ở phía trên và bảng danh sách ở phía dưới. Quản lý có thể nhập chi nhánh mới hoặc chọn một dòng trong bảng để cập nhật, đúng với thói quen thao tác CRUD.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Nút `Thêm mới` giúp clear form trước khi tạo chi nhánh mới. Nút `Xóa/Ngừng hoạt động` không xóa vật lý mà chuyển trạng thái, giúp tránh mất dữ liệu chi nhánh đã phát sinh liên kết với phòng, nhân viên hoặc lịch sử thuê.

**Tính nhất quán:** Các trường thông tin chi nhánh dùng `TextBox`, trạng thái dùng `ComboBox`, danh sách dùng `DataGridView`. Nút `Lưu` là hành động chính, nút xóa dùng màu cảnh báo để nhắc người dùng đây là thao tác nhạy cảm.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Quản lý mở màn hình `Quản lý chi nhánh`.

**Bước 2:** Hệ thống nạp danh sách chi nhánh lên `dgvChiNhanh`.

**Bước 3:** Quản lý nhập thông tin chi nhánh mới hoặc chọn một dòng để chỉnh sửa.

**Bước 4:** Quản lý nhấn `Lưu`.

**Bước 5:** Hệ thống kiểm tra tên chi nhánh, khu vực và địa chỉ.

**Bước 6:** Nếu hợp lệ, hệ thống thêm mới hoặc cập nhật chi nhánh trong DB.

**Bước 7:** Nếu muốn ngừng hoạt động, quản lý chọn chi nhánh và nhấn `Xóa/Ngừng hoạt động`.

**Bước 8:** Hệ thống cập nhật trạng thái chi nhánh sang `Ngừng hoạt động` và reload danh sách.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TextBox` phù hợp với tên, khu vực, địa chỉ và số điện thoại. `ComboBox` giới hạn trạng thái vào hai giá trị hợp lệ. `DataGridView` giúp quản lý chọn bản ghi để sửa hoặc ngừng hoạt động.

**Logic bắt lỗi:** `ChiNhanhBLL.Luu()` kiểm tra các trường bắt buộc gồm tên chi nhánh, khu vực và địa chỉ. `ChiNhanhBLL.Xoa()` kiểm tra phải có mã chi nhánh đang chọn.

**Logic nghiệp vụ:** Khi lưu, GUI tạo `ChiNhanhDTO`, BLL validate rồi gọi `ChiNhanhDAL.Luu()`. DAL kiểm tra `MaCN`: nếu đã tồn tại thì UPDATE, nếu chưa có thì sinh mã mới và INSERT. Khi xóa, DAL cập nhật `Chi_Nhanh.TrangThai = Ngừng hoạt động`.

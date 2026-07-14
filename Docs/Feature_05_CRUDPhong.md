# Feature 05 - CRUD Phòng

## 1. Mục đích

Cho phép quản lý thêm, sửa, xem danh sách và ngừng sử dụng phòng. Feature này nằm trong tab `Phòng` của màn hình quản lý phòng/giường.

## 2. GUI - Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyPhong.cs`

### Thành phần giao diện trong tab Phòng

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `dgvPhong` | `DataGridView` | Hiển thị danh sách phòng. |
| `cboChiNhanh` | `ComboBox` | Chọn chi nhánh, dữ liệu từ `ChiNhanhBLL.LayDanhSach`. |
| `txtTenPhong` | `TextBox` | Nhập tên phòng. |
| `txtKhuVuc` | `TextBox` | Nhập khu vực của phòng. |
| `cboGioiTinh` | `ComboBox` | Giới tính/quy định phòng, lấy động từ DB. |
| `cboLoaiPhong` | `ComboBox` | Loại phòng, lấy động từ DB. |
| `nudSucChua` | `NumericUpDown` | Sức chứa tối đa. |
| `nudGiaPhong` | `NumericUpDown` | Giá thuê phòng. |
| `cboTrangThaiPhong` | `ComboBox` | Trạng thái phòng: `Hoạt động`, `Ngừng sử dụng`. |
| `btnLuu` | `Button` | Lưu phòng. |
| `btnXoa` | `Button` | Chuyển phòng sang `Ngừng sử dụng`. |
| `btnMoi` | `Button` | Clear form để thêm mới. |
| `lblTrangThai` | `Label` | Hiển thị thông báo. |

### Hàm trong GUI liên quan đến phòng

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo form và tab. |
| `CreatePhongLayout()` | Tạo layout tab phòng. |
| `LoadCombos()` | Nạp chi nhánh, giới tính, loại phòng, trạng thái. |
| `LoadPhong()` | Lấy danh sách phòng và bind vào `dgvPhong`, đồng thời nạp combobox phòng cho tab giường. |
| `FillSelectedPhong()` | Khi chọn dòng, đổ dữ liệu lên form. |
| `ClearPhong()` | Reset form phòng. |
| `BtnLuuPhong_Click(sender, e)` | Tạo `PhongDTO`, gọi `PhongBLL.Luu`. |
| `BtnXoaPhong_Click(sender, e)` | Gọi `PhongBLL.Xoa`. |
| `ConfigureMoney(input)` | Cấu hình numeric tiền. |
| `AddField(...)`, `FixedButton(...)` | Hàm hỗ trợ dựng UI. |

## 3. DTO liên quan

File: `DTO/PhongDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaPhong` | Mã phòng, rỗng khi thêm mới. |
| `MaChiNhanh` | Chi nhánh chứa phòng. |
| `TenPhong` | Tên phòng. |
| `KhuVuc` | Khu vực. |
| `GioiTinhQuyDinh` | Giới tính/quy định phòng. |
| `LoaiPhong` | Loại phòng. |
| `SucChua` | Sức chứa tối đa. |
| `GiaThue` | Giá thuê phòng. |
| `TrangThaiPhong` | Trạng thái phòng. |

## 4. BLL - Business Logic Layer

File: `BLL/QuanTriHeThong/PhongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách phòng. |
| `LayDanhMucLoaiPhong()` | Lấy danh mục loại phòng. |
| `LayDanhMucGioiTinhQuyDinh()` | Lấy danh mục giới tính/quy định phòng. |
| `Luu(phong)` | Validate và gọi DAL thêm/sửa phòng. |
| `Xoa(maPhong)` | Validate mã phòng và gọi DAL ngừng sử dụng phòng. |

BLL liên quan:

| BLL | Hàm | Vai trò |
|---|---|---|
| `ChiNhanhBLL` | `LayDanhSach()` | Nạp combobox chi nhánh. |

## 5. DAL - Data Access Layer

File: `DAL/QuanTriHeThong/PhongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT phòng join chi nhánh. |
| `LayDanhMucLoaiPhong()` | SELECT DISTINCT loại phòng. |
| `LayDanhMucGioiTinhQuyDinh()` | SELECT DISTINCT giới tính/quy định phòng. |
| `Luu(phong)` | Nếu `MaPhong` tồn tại thì UPDATE, ngược lại INSERT. |
| `Xoa(maPhong)` | UPDATE `TrangThaiPhong = Ngừng sử dụng`. |
| `TaoMaMoi()` | Sinh mã phòng dạng `Pxxx`. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Phong` | Lưu thông tin phòng. |
| `Chi_Nhanh` | Join để hiển thị tên chi nhánh và chọn chi nhánh. |

## 7. Sequence để tự vẽ

### Thêm/sửa phòng

1. Quản lý mở `frmQuanLyPhong`, tab `Phòng`.
2. GUI gọi `LoadCombos()` và `LoadPhong()`.
3. Quản lý nhập/chỉnh thông tin phòng.
4. Quản lý bấm `Lưu phòng`.
5. `BtnLuuPhong_Click()` tạo `PhongDTO`.
6. GUI gọi `PhongBLL.Luu(dto)`.
7. BLL kiểm tra chi nhánh, tên phòng, khu vực, giới tính, loại phòng, sức chứa, giá thuê.
8. Nếu lỗi, BLL throw exception, GUI hiển thị lỗi.
9. Nếu hợp lệ, BLL gọi `PhongDAL.Luu(dto)`.
10. DAL kiểm tra `MaPhong`.
11. Nếu có phòng, DAL UPDATE `Phong`.
12. Nếu chưa có, DAL sinh mã mới và INSERT `Phong`.
13. DAL trả `MaPhong`.
14. GUI gọi `LoadPhong()` và hiển thị thành công.

### Ngừng sử dụng phòng

1. Quản lý chọn phòng trên `dgvPhong`.
2. GUI gọi `FillSelectedPhong()`.
3. Quản lý bấm `Xóa phòng`.
4. GUI gọi `PhongBLL.Xoa(_maPhong)`.
5. BLL kiểm tra mã phòng.
6. BLL gọi `PhongDAL.Xoa(maPhong)`.
7. DAL UPDATE `Phong.TrangThaiPhong = Ngừng sử dụng`.
8. GUI gọi `LoadPhong()` và `ClearPhong()`.

## 8. Ghi chú

- Xóa phòng là soft-delete, không xóa vật lý khỏi database.
- Khi lưu phòng, `SoLuongGiuong` được set bằng `SucChua`.

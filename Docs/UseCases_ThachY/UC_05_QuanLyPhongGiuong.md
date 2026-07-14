# UC 05 - Quản lý phòng/giường

## 1. Phạm vi use case

Quản lý thêm, sửa, xem danh sách và ngừng sử dụng phòng/giường. Code hiện tại triển khai phòng và giường trong cùng màn hình `frmQuanLyPhong`, gồm 2 tab: `Phòng` và `Giường trong phòng`.

## 2. GUI / Presentation layer

File: `UI/QuanTriHeThong/frmQuanLyPhong.cs`

### Control phần Phòng

| Control | Kiểu | Vai trò |
|---|---|---|
| `dgvPhong` | `DataGridView` | Danh sách phòng. |
| `cboChiNhanh` | `ComboBox` | Chọn chi nhánh. |
| `txtTenPhong` | `TextBox` | Tên phòng. |
| `txtKhuVuc` | `TextBox` | Khu vực. |
| `cboGioiTinh` | `ComboBox` | Giới tính/quy định phòng. |
| `cboLoaiPhong` | `ComboBox` | Loại phòng. |
| `nudSucChua` | `NumericUpDown` | Sức chứa. |
| `nudGiaPhong` | `NumericUpDown` | Giá thuê phòng. |
| `cboTrangThaiPhong` | `ComboBox` | Trạng thái phòng. |
| `btnLuuPhong` | `Button` | Lưu phòng. |
| `btnXoaPhong` | `Button` | Ngừng sử dụng phòng. |
| `btnMoiPhong` | `Button` | Clear form phòng. |

### Control phần Giường

| Control | Kiểu | Vai trò |
|---|---|---|
| `dgvGiuong` | `DataGridView` | Danh sách giường theo phòng. |
| `cboPhongGiuong` | `ComboBox` | Chọn phòng chứa giường. |
| `txtTenGiuong` | `TextBox` | Tên giường. |
| `nudGiaGiuong` | `NumericUpDown` | Giá thuê giường. |
| `cboTrangThaiGiuong` | `ComboBox` | Trạng thái giường. |
| `btnLuuGiuong` | `Button` | Lưu giường. |
| `btnXoaGiuong` | `Button` | Ngừng sử dụng giường. |
| `btnMoiGiuong` | `Button` | Clear form giường. |
| `lblTrangThai` | `Label` | Thông báo chung. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Dựng form và tab. |
| `CreatePhongLayout()` | Dựng tab phòng. |
| `CreateGiuongLayout()` | Dựng tab giường. |
| `LoadCombos()` | Nạp combobox chi nhánh, giới tính, loại phòng, trạng thái. |
| `LoadPhong()` | Nạp danh sách phòng và combobox phòng cho giường. |
| `LoadGiuong()` | Nạp danh sách giường theo phòng. |
| `FillSelectedPhong()` | Đổ dòng phòng đang chọn lên form. |
| `FillSelectedGiuong()` | Đổ dòng giường đang chọn lên form. |
| `ClearPhong()` | Reset form phòng. |
| `ClearGiuong()` | Reset form giường. |
| `BtnLuuPhong_Click(sender, e)` | Tạo `PhongDTO`, gọi `PhongBLL.Luu`. |
| `BtnXoaPhong_Click(sender, e)` | Gọi `PhongBLL.Xoa`. |
| `BtnLuuGiuong_Click(sender, e)` | Tạo `GiuongDTO`, gọi `GiuongBLL.Luu`. |
| `BtnXoaGiuong_Click(sender, e)` | Gọi `GiuongBLL.Xoa`. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/QuanTriHeThong/PhongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách phòng. |
| `LayDanhMucLoaiPhong()` | Lấy loại phòng. |
| `LayDanhMucGioiTinhQuyDinh()` | Lấy giới tính/quy định phòng. |
| `Luu(phong)` | Validate và lưu phòng. |
| `Xoa(maPhong)` | Validate và ngừng sử dụng phòng. |

File: `BLL/QuanTriHeThong/GiuongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSachTheoPhong(maPhong)` | Lấy giường theo phòng. |
| `Luu(giuong)` | Validate và lưu giường. |
| `Xoa(maGiuong)` | Validate và ngừng sử dụng giường. |

File: `BLL/QuanTriHeThong/ChiNhanhBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Nạp combobox chi nhánh. |

## 4. DAL - Data Access layer

File: `DAL/QuanTriHeThong/PhongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT phòng join chi nhánh. |
| `LayDanhMucLoaiPhong()` | SELECT DISTINCT loại phòng. |
| `LayDanhMucGioiTinhQuyDinh()` | SELECT DISTINCT giới tính. |
| `Luu(phong)` | UPDATE nếu có mã phòng, INSERT nếu thêm mới. |
| `Xoa(maPhong)` | UPDATE `TrangThaiPhong = Ngừng sử dụng`. |
| `TaoMaMoi()` | Sinh mã phòng. |

File: `DAL/QuanTriHeThong/GiuongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSachTheoPhong(maPhong)` | SELECT giường theo phòng. |
| `Luu(giuong)` | UPDATE nếu có mã giường, INSERT nếu thêm mới. |
| `Xoa(maGiuong)` | UPDATE `TrangThaiGiuong = Ngừng sử dụng`. |
| `TaoMaMoi(maPhong)` | Sinh mã giường. |

File: `DAL/QuanTriHeThong/ChiNhanhDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT chi nhánh cho combobox. |

## 5. DTO và Database

DTO:

| DTO | Vai trò |
|---|---|
| `PhongDTO` | Truyền dữ liệu phòng từ GUI xuống BLL/DAL. |
| `GiuongDTO` | Truyền dữ liệu giường từ GUI xuống BLL/DAL. |

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Phong` | Lưu phòng. |
| `Giuong` | Lưu giường. |
| `Chi_Nhanh` | Chọn chi nhánh và hiển thị tên chi nhánh. |

## 6. Sequence để vẽ

### Luồng quản lý phòng

1. Actor mở `frmQuanLyPhong`, tab `Phòng`.
2. GUI gọi `LoadCombos()` và `LoadPhong()`.
3. `LoadPhong()` gọi `PhongBLL.LayDanhSach()`.
4. BLL gọi `PhongDAL.LayDanhSach()`.
5. DAL SELECT DB và trả danh sách phòng.
6. GUI bind lên `dgvPhong`.
7. Actor nhập hoặc chọn phòng.
8. Nếu chọn dòng, GUI gọi `FillSelectedPhong()`.
9. Actor bấm `Lưu phòng`.
10. GUI tạo `PhongDTO`.
11. GUI gọi `PhongBLL.Luu(dto)`.
12. BLL validate dữ liệu phòng.
13. BLL gọi `PhongDAL.Luu(dto)`.
14. DAL UPDATE hoặc INSERT `Phong`.
15. GUI gọi `LoadPhong()` và hiển thị kết quả.

### Luồng ngừng sử dụng phòng

1. Actor chọn phòng.
2. Actor bấm `Xóa phòng`.
3. GUI gọi `PhongBLL.Xoa(_maPhong)`.
4. BLL gọi `PhongDAL.Xoa(maPhong)`.
5. DAL UPDATE trạng thái phòng sang `Ngừng sử dụng`.
6. GUI reload danh sách.

### Luồng quản lý giường

1. Actor mở tab `Giường trong phòng`.
2. Actor chọn phòng ở `cboPhongGiuong`.
3. GUI gọi `LoadGiuong()`.
4. GUI gọi `GiuongBLL.LayDanhSachTheoPhong(maPhong)`.
5. BLL gọi `GiuongDAL.LayDanhSachTheoPhong(maPhong)`.
6. DAL SELECT DB và trả danh sách giường.
7. Actor nhập hoặc chọn giường.
8. Actor bấm `Lưu giường`.
9. GUI tạo `GiuongDTO`.
10. GUI gọi `GiuongBLL.Luu(dto)`.
11. BLL validate dữ liệu giường.
12. BLL gọi `GiuongDAL.Luu(dto)`.
13. DAL UPDATE hoặc INSERT `Giuong`.
14. GUI gọi `LoadGiuong()` và hiển thị kết quả.

### Luồng ngừng sử dụng giường

1. Actor chọn giường.
2. Actor bấm `Xóa giường`.
3. GUI gọi `GiuongBLL.Xoa(_maGiuong)`.
4. BLL gọi `GiuongDAL.Xoa(maGiuong)`.
5. DAL UPDATE trạng thái giường sang `Ngừng sử dụng`.
6. GUI reload danh sách.

## 7. Ghi chú để vẽ 3-layer

- Đây là một UC chung nhưng có thể vẽ 2 nhánh trong cùng sơ đồ: `Phòng` và `Giường`.
- Xóa là soft-delete bằng trạng thái, không xóa vật lý.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình quản lý phòng/giường được chia thành hai tab rõ ràng: tab `Phòng` để quản lý thông tin phòng và tab `Giường trong phòng` để quản lý giường thuộc từng phòng. Cách chia này phù hợp với quan hệ dữ liệu cha - con giữa phòng và giường.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Nút `Thêm mới` ở từng tab giúp reset form, tránh việc sửa nhầm bản ghi đang chọn. Nút `Xóa` không xóa vật lý mà chỉ chuyển trạng thái sang `Ngừng sử dụng`, giúp dữ liệu vẫn có thể được đối soát về sau.

**Tính nhất quán:** Danh sách phòng và giường đều dùng `DataGridView`. Các giá trị danh mục như chi nhánh, giới tính, loại phòng, trạng thái dùng `ComboBox`. Các trường số như sức chứa và giá thuê dùng `NumericUpDown`.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Quản lý mở màn hình `Quản lý phòng/giường`.

**Bước 2:** Hệ thống nạp danh sách phòng và các danh mục cần thiết như chi nhánh, giới tính, loại phòng, trạng thái.

**Bước 3:** Ở tab `Phòng`, quản lý nhập thông tin phòng mới hoặc chọn một phòng trong bảng để chỉnh sửa.

**Bước 4:** Quản lý nhấn `Lưu phòng`. Hệ thống kiểm tra hợp lệ và thêm/cập nhật phòng.

**Bước 5:** Nếu cần ngừng sử dụng phòng, quản lý chọn phòng và nhấn `Xóa phòng`. Hệ thống cập nhật trạng thái phòng sang `Ngừng sử dụng`.

**Bước 6:** Ở tab `Giường trong phòng`, quản lý chọn phòng từ `cboPhongGiuong`.

**Bước 7:** Hệ thống nạp danh sách giường thuộc phòng đã chọn.

**Bước 8:** Quản lý nhập hoặc chỉnh thông tin giường rồi nhấn `Lưu giường`.

**Bước 9:** Nếu cần ngừng sử dụng giường, quản lý chọn giường và nhấn `Xóa giường`. Hệ thống cập nhật trạng thái giường sang `Ngừng sử dụng`.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TabControl` giúp tách hai nhóm nghiệp vụ phòng và giường nhưng vẫn giữ chung một màn hình quản lý. `ComboBox` dùng để chọn chi nhánh, phòng, giới tính, loại phòng và trạng thái. `NumericUpDown` dùng cho sức chứa và giá thuê. `DataGridView` giúp chọn bản ghi và quan sát danh sách.

**Logic bắt lỗi:** `PhongBLL.Luu()` kiểm tra chi nhánh, tên phòng, khu vực, giới tính, loại phòng, sức chứa và giá thuê. `GiuongBLL.Luu()` kiểm tra phòng, tên giường, giá thuê và trạng thái. Nếu dữ liệu sai, BLL throw lỗi và DAL không được gọi.

**Logic nghiệp vụ:** Khi lưu phòng, GUI tạo `PhongDTO`, BLL validate rồi gọi `PhongDAL.Luu()`. DAL quyết định UPDATE hoặc INSERT theo `MaPhong`. Khi lưu giường, GUI tạo `GiuongDTO`, BLL validate rồi gọi `GiuongDAL.Luu()`. Xóa phòng/giường là soft-delete: DAL chỉ cập nhật trạng thái sang `Ngừng sử dụng`.

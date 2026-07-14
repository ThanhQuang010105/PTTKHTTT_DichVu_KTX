# Feature 09 - Dashboard thống kê và menu theo quyền

## 1. Mục đích

Hiển thị Dashboard sau đăng nhập, cho phép người dùng mở các chức năng theo vai trò và xem các chỉ số thống kê tổng quan.

## 2. GUI - Presentation layer

File: `UI/Common/frmDashboard.cs`

### Thành phần giao diện

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `_nhanVien` | `NhanVienDTO` | Người dùng đăng nhập hiện tại. |
| `_baoCaoBLL` | `BaoCaoBLL` | BLL lấy thống kê. |
| `lblUser` | `Label` | Hiển thị họ tên và vai trò. |
| `menu` | `FlowLayoutPanel` | Chứa các nút chức năng. |
| `pnlStats` | `FlowLayoutPanel` | Chứa các card thống kê. |
| `btnRefresh` | `Button` | Làm mới thống kê. |

### Nút menu theo quyền

| Nút | Form mở | Role được xem |
|---|---|---|
| `Đăng ký thuê` | `frmDangKyThue` | `Sale`, `QuanLy`. |
| `Tra cứu phòng` | `frmTraCuuPhong` | `Sale`, `QuanLy`. |
| `Lập lịch xem phòng` | `frmLichXemPhong` | `Sale`, `QuanLy`. |
| `Quản lý phòng/giường` | `frmQuanLyPhong` | `QuanLy`. |
| `Quản lý chi nhánh` | `frmQuanLyChiNhanh` | `QuanLy`. |
| `Quản lý nhân viên` | `frmQuanLyNhanVien` | `QuanLy`. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `frmDashboard(nhanVien)` | Nhận user đăng nhập và gọi `LoadStats`. |
| `InitializeComponent()` | Dựng title, user label, menu, khu thống kê. |
| `AddMenuButton(menu, text, open, roles)` | Chỉ thêm nút nếu user có role phù hợp. |
| `IsInRole(roles)` | Kiểm tra role hiện tại. |
| `NormalizeRole(role)` | Chuẩn hóa role để bỏ dấu/khoảng trắng, so sánh ổn định. |
| `LoadStats()` | Gọi BLL lấy thống kê và tạo card. |
| `AddStat(title, value)` | Tạo một card thống kê. |

## 3. DTO liên quan

File: `DTO/NhanVienDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `HoTen` | Hiển thị tên người dùng. |
| `VaiTro` | Xác định quyền menu. |

## 4. BLL - Business Logic Layer

File: `BLL/TienIch/BaoCaoBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayThongKeDashboard()` | Gọi DAL lấy một dòng thống kê tổng quan. |

## 5. DAL - Data Access Layer

File: `DAL/TienIch/BaoCaoDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayThongKeDashboard()` | SELECT nhiều chỉ số bằng subquery. |

## 6. Bảng CSDL liên quan

| Bảng | Chỉ số sử dụng |
|---|---|
| `Phong` | Tổng phòng, phòng trống, phòng đang ở, phòng đặt cọc. |
| `Giuong` | Tổng giường, giường trống. |
| `Dang_ky_thue` | Phiếu đăng ký mới/chờ xử lý. |
| `Lich_xem_phong` | Số lịch hẹn xem phòng. |
| `Nhan_Vien` | Nhân viên đang làm. |

## 7. Sequence để tự vẽ

### Mở Dashboard và phân quyền menu

1. `frmDangNhap` đăng nhập thành công và mở `frmDashboard(nhanVien)`.
2. Dashboard lưu `_nhanVien`.
3. `InitializeComponent()` tạo menu.
4. Với từng chức năng, GUI gọi `AddMenuButton(menu, text, open, roles)`.
5. `AddMenuButton` gọi `IsInRole(roles)`.
6. `IsInRole` gọi `NormalizeRole(_nhanVien.VaiTro)`.
7. Nếu role hợp lệ, nút được thêm vào menu.
8. Nếu role không hợp lệ, nút không xuất hiện.

### Tải thống kê

1. Dashboard gọi `LoadStats()`.
2. GUI gọi `BaoCaoBLL.LayThongKeDashboard()`.
3. BLL gọi `BaoCaoDAL.LayThongKeDashboard()`.
4. DAL SELECT các chỉ số từ DB.
5. DAL trả `DataTable`.
6. GUI đọc dòng đầu tiên.
7. GUI gọi `AddStat` cho từng chỉ số.
8. Các card thống kê hiển thị trên `pnlStats`.

## 8. Ghi chú

- RBAC hiện được xử lý ở Presentation layer bằng việc ẩn/hiện nút menu.
- Role `Quản lý`, `QuanLy`, khác dấu hoặc có khoảng trắng vẫn được chuẩn hóa để so sánh.

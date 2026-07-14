# UC 03 - Tra cứu phòng/giường

## 1. Phạm vi use case

Nhân viên sale nhập bộ lọc để tra cứu danh sách phòng hoặc giường phù hợp. UC này chỉ đọc dữ liệu, không tạo phiếu đăng ký thuê.

## 2. GUI / Presentation layer

File: `UI/DangKyThue/frmTraCuuPhong.cs`

### Control

| Control | Kiểu | Vai trò |
|---|---|---|
| `cboHinhThucThue` | `ComboBox` | Chọn thuê nguyên phòng hoặc thuê giường. |
| `cboKhuVuc` | `ComboBox` | Khu vực, lấy từ DB. |
| `cboGioiTinh` | `ComboBox` | Giới tính/quy định phòng, lấy từ DB. |
| `cboLoaiPhong` | `ComboBox` | Loại phòng, lấy từ DB. |
| `nudSoNguoi` | `NumericUpDown` | Số người. |
| `nudGiaToiDa` | `NumericUpDown` | Giá tối đa. |
| `dtpNgayVao` | `DateTimePicker` | Ngày vào. |
| `txtTieuChi` | `TextBox` | Tiêu chí ưu tiên. |
| `btnTraCuu` | `Button` | Thực hiện tra cứu. |
| `btnLamMoi` | `Button` | Reset bộ lọc. |
| `dgvKetQua` | `DataGridView` | Hiển thị kết quả. |
| `lblTrangThai` | `Label` | Thông báo kết quả/lỗi. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Dựng giao diện. |
| `LoadDefaults()` | Nạp combobox và cấu hình control. |
| `ResetForm()` | Reset bộ lọc và bảng kết quả. |
| `BtnTraCuu_Click(sender, e)` | Tạo DTO, validate bộ lọc, gọi BLL tra cứu. |
| `TaoThongTinDangKyTuBoLoc()` | Tạo `PhieuDangKyThueDTO` từ bộ lọc. |

## 3. BLL/BAL - Business Logic layer

File: `BLL/DangKyThue/TraCuuPhongGiuongBLL.cs`

| Class/Hàm | Vai trò |
|---|---|
| `TraCuuPhongGiuongBLL` | BLL điều phối tra cứu. |
| `KiemTraBoLoc(thongTinDK)` | Kiểm tra bộ lọc. |
| `TraCuuPhongGiuongPhuHop(thongTinDK)` | Hàm chính, rẽ nhánh phòng/giường. |
| `TraCuuPhongGiuongKhaDung(tieuChi)` | Alias tên cũ. |
| `TraCuuPhongPhuHop(thongTinDK)` | Gọi `PhongBLL`. |
| `TraCuuGiuongPhuHop(thongTinDK)` | Gọi `GiuongBLL`. |
| `LayLoiBoLoc(tieuChi)` | Private validate. |
| `LaHinhThucThueHopLe(hinhThucThue)` | Kiểm tra hình thức thuê. |
| `LaThueNguyenPhong(hinhThucThue)` | Xác định nhánh phòng. |
| `TraCuuPhongGiuongResult` | Kết quả trả về GUI. |

BLL nhánh:

| Class/Hàm | Vai trò |
|---|---|
| `PhongBLL.TraCuuPhongPhuHop(tieuChi)` | Gọi DAL đọc phòng phù hợp. |
| `GiuongBLL.TraCuuGiuongPhuHop(tieuChi)` | Gọi DAL đọc giường phù hợp. |
| `ChiNhanhBLL.LayDanhMucKhuVuc()` | Nạp combobox khu vực. |
| `PhongBLL.LayDanhMucGioiTinhQuyDinh()` | Nạp combobox giới tính. |
| `PhongBLL.LayDanhMucLoaiPhong()` | Nạp combobox loại phòng. |

## 4. DAL - Data Access layer

| File/Hàm | Vai trò |
|---|---|
| `ChiNhanhDAL.LayDanhMucKhuVuc()` | SELECT khu vực từ `Chi_Nhanh`. |
| `PhongDAL.LayDanhMucGioiTinhQuyDinh()` | SELECT giới tính/quy định phòng. |
| `PhongDAL.LayDanhMucLoaiPhong()` | SELECT loại phòng. |
| `PhongDAL.DocDanhSachPhongTrong(tieuChi)` | SELECT phòng phù hợp theo bộ lọc. |
| `PhongDAL.TraCuuPhongKhaDung(tieuChi)` | Alias gọi `DocDanhSachPhongTrong`. |
| `GiuongDAL.DocDanhSachGiuongTrong(tieuChi)` | SELECT giường phù hợp theo bộ lọc. |
| `GiuongDAL.TraCuuGiuongKhaDung(tieuChi)` | Alias gọi `DocDanhSachGiuongTrong`. |

## 5. DTO và Database

DTO: `PhieuDangKyThueDTO` dùng như criteria tra cứu.

Result: `TraCuuPhongGiuongResult`

| Thuộc tính | Vai trò |
|---|---|
| `ThanhCong` | Kết quả validate/tra cứu. |
| `KetQua` | `DataTable` phòng/giường. |
| `ThongBao` | Thông báo cho GUI. |

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Chi_Nhanh` | Lọc khu vực và tên chi nhánh. |
| `Phong` | Lọc loại phòng, giới tính, trạng thái, giá phòng. |
| `Giuong` | Lọc giường trống, giá giường. |

## 6. Sequence để vẽ

1. Actor mở `frmTraCuuPhong`.
2. GUI gọi `LoadDefaults()`.
3. Actor nhập bộ lọc.
4. Actor bấm `Tra cứu`.
5. `BtnTraCuu_Click()` gọi `TaoThongTinDangKyTuBoLoc()`.
6. GUI gọi `TraCuuPhongGiuongBLL.KiemTraBoLoc(thongTinDK)`.
7. Nếu bộ lọc sai, BLL trả result thất bại, GUI hiển thị lỗi.
8. Nếu đúng, GUI gọi `TraCuuPhongGiuongBLL.TraCuuPhongGiuongPhuHop(thongTinDK)`.
9. BLL kiểm tra `HinhThucThue`.
10. Nếu `Thuê nguyên phòng`, BLL gọi `PhongBLL.TraCuuPhongPhuHop`.
11. `PhongBLL` gọi `PhongDAL.DocDanhSachPhongTrong`.
12. DAL SELECT DB và trả `DataTable`.
13. Nếu `Thuê giường`, BLL gọi `GiuongBLL.TraCuuGiuongPhuHop`.
14. `GiuongBLL` gọi `GiuongDAL.DocDanhSachGiuongTrong`.
15. DAL SELECT DB và trả `DataTable`.
16. BLL trả `TraCuuPhongGiuongResult`.
17. GUI bind `KetQua` lên `dgvKetQua`.
18. GUI hiển thị `ThongBao`.

## 7. Ghi chú để vẽ 3-layer

- UC này độc lập với UC `Đăng ký thuê`.
- Không có `DangKyThueDAL` trong UC này.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình tra cứu được thiết kế theo mô hình bộ lọc ở phía trên và kết quả ở phía dưới. Nhân viên sale chọn điều kiện tìm kiếm trước, sau đó bấm `Tra cứu` và đọc kết quả trong bảng. Luồng này phù hợp với thói quen lọc dữ liệu trong các màn hình nghiệp vụ.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Nút `Làm mới` giúp đưa bộ lọc về trạng thái mặc định và xóa kết quả cũ. Nhờ vậy nhân viên có thể bắt đầu lượt tra cứu mới mà không bị nhầm với dữ liệu tìm kiếm trước đó.

**Tính nhất quán:** Các tiêu chí dạng danh mục dùng `ComboBox`, dữ liệu số dùng `NumericUpDown`, ngày dùng `DateTimePicker`, kết quả dùng `DataGridView`. Nút `Tra cứu` là nút hành động chính và sử dụng màu primary.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Nhân viên sale mở màn hình `Tra cứu phòng/giường`.

**Bước 2:** Nhân viên chọn hình thức thuê, khu vực, giới tính, loại phòng, số người, giá tối đa, ngày vào và tiêu chí ưu tiên.

**Bước 3:** Nhân viên nhấn nút `Tra cứu`.

**Bước 4:** Hệ thống kiểm tra tính hợp lệ của bộ lọc. Nếu bộ lọc thiếu hoặc sai, hệ thống hiển thị lỗi.

**Bước 5:** Nếu bộ lọc hợp lệ, hệ thống xét `HinhThucThue`.

**Bước 6:** Nếu là `Thuê nguyên phòng`, hệ thống tra cứu danh sách phòng phù hợp.

**Bước 7:** Nếu là `Thuê giường`, hệ thống tra cứu danh sách giường phù hợp.

**Bước 8:** Hệ thống hiển thị kết quả lên `dgvKetQua`. Nếu không có kết quả, hệ thống hiển thị thông báo không có phòng/giường phù hợp.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `ComboBox` được dùng cho các tiêu chí có tập giá trị rõ ràng như hình thức thuê, khu vực, giới tính và loại phòng. `NumericUpDown` dùng cho số người và giá tối đa để tránh nhập chữ vào ô số. `DateTimePicker` chuẩn hóa ngày vào. `DataGridView` hiển thị kết quả dạng bảng, giúp nhân viên dễ quét mã phòng, tên phòng, giá thuê và trạng thái.

**Logic bắt lỗi:** `TraCuuPhongGiuongBLL.KiemTraBoLoc()` kiểm tra hình thức thuê, khu vực, giới tính, loại phòng, số người và giá tối đa. Nếu có lỗi, BLL trả `TraCuuPhongGiuongResult.TaoThatBai` và GUI không truy vấn dữ liệu.

**Logic nghiệp vụ:** `TraCuuPhongGiuongBLL.TraCuuPhongGiuongPhuHop()` đọc `HinhThucThue` để rẽ nhánh. Nếu thuê nguyên phòng, BLL gọi `PhongBLL.TraCuuPhongPhuHop()` rồi xuống `PhongDAL.DocDanhSachPhongTrong()`. Nếu thuê giường, BLL gọi `GiuongBLL.TraCuuGiuongPhuHop()` rồi xuống `GiuongDAL.DocDanhSachGiuongTrong()`. Kết quả được đóng gói vào `TraCuuPhongGiuongResult` và trả về GUI.

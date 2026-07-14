# UC 04 - Lập lịch xem phòng

## 1. Phạm vi use case

Nhân viên sale nhập mã đăng ký, hệ thống lấy thông tin phiếu, tra cứu phòng/giường phù hợp, sau đó nhân viên chọn một phòng/giường để lập lịch xem phòng.

## 2. GUI / Presentation layer

File: `UI/DangKyThue/frmLichXemPhong.cs`

### Control

| Control | Kiểu | Vai trò |
|---|---|---|
| `txtMaDangKy` | `TextBox` | Nhập mã phiếu đăng ký. |
| `btnTim` | `Button` | Tìm phiếu đăng ký. |
| `txtThongTinKhach` | `TextBox` | Hiển thị khách hàng, readonly. |
| `txtNhuCau` | `TextBox` | Hiển thị nhu cầu thuê, readonly. |
| `dgvKetQua` | `DataGridView` | Hiển thị phòng/giường phù hợp. |
| `dtpNgayGioHen` | `DateTimePicker` | Ngày giờ hẹn xem phòng. |
| `txtGhiChu` | `TextBox` | Ghi chú. |
| `btnLuu` | `Button` | Lưu lịch hẹn. |
| `btnLamMoi` | `Button` | Reset form. |
| `dgvLichHen` | `DataGridView` | Danh sách lịch hẹn đã lập. |
| `lblTrangThai` | `Label` | Thông báo. |

### Hàm/event

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Dựng giao diện. |
| `ResetForm()` | Reset thông tin và kết quả. |
| `LoadLichHen()` | Nạp danh sách lịch hẹn. |
| `BtnTim_Click(sender, e)` | Tìm phiếu đăng ký và tra cứu phòng/giường phù hợp. |
| `BtnLuu_Click(sender, e)` | Tạo `LichXemPhongDTO`, gọi BLL lưu lịch. |

## 3. BLL/BAL - Business Logic layer

BLL chính: `BLL/DangKyThue/LichXemPhongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách lịch hẹn. |
| `TaoLich(lich)` | Validate lịch và gọi DAL lưu. |

BLL dùng trong cùng UC:

| Class/Hàm | Vai trò |
|---|---|
| `DangKyThueBLL.LayChiTietDangKy(maDangKy)` | Lấy chi tiết phiếu đăng ký. |
| `DangKyThueBLL.TaoDtoTuDongChiTiet(row)` | Map chi tiết phiếu sang DTO. |
| `TraCuuPhongGiuongBLL.TraCuuPhongGiuongKhaDung(phieuDangKy)` | Tra cứu phòng/giường phù hợp theo nhu cầu phiếu. |

## 4. DAL - Data Access layer

DAL chính: `DAL/DangKyThue/LichXemPhongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT lịch hẹn join giường/phòng. |
| `TaoLich(lich)` | Sinh mã lịch, tìm giường nếu cần, insert lịch, cập nhật trạng thái đăng ký. |
| `TaoMaMoi()` | Sinh mã `Lxxx`. |

DAL dùng trong cùng UC:

| Class/Hàm | Vai trò |
|---|---|
| `DangKyThueDAL.LayChiTietDangKy(maDangKy)` | Đọc phiếu đăng ký. |
| `PhongDAL.DocDanhSachPhongTrong(tieuChi)` | Tra cứu phòng phù hợp nếu thuê nguyên phòng. |
| `GiuongDAL.DocDanhSachGiuongTrong(tieuChi)` | Tra cứu giường phù hợp nếu thuê giường. |

## 5. DTO và Database

DTO chính: `DTO/LichXemPhongDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaLich` | Mã lịch. |
| `MaDangKy` | Mã phiếu đăng ký. |
| `LoaiDoiTuong` | Loại kết quả chọn. |
| `MaPhong`, `MaGiuong` | Phòng/giường được chọn. |
| `NgayGioHen` | Ngày giờ hẹn. |
| `GhiChu`, `TrangThai` | Ghi chú và trạng thái. |

DTO dùng thêm: `PhieuDangKyThueDTO`

Bảng DB:

| Bảng | Vai trò |
|---|---|
| `Dang_ky_thue` | Lấy phiếu và cập nhật trạng thái `Đã hẹn xem phòng`. |
| `Khach_hang` | Lấy thông tin khách hàng. |
| `Phong`, `Giuong`, `Chi_Nhanh` | Tra cứu đối tượng phù hợp. |
| `Lich_xem_phong` | Lưu lịch hẹn. |

## 6. Sequence để vẽ

1. Actor mở `frmLichXemPhong`.
2. GUI gọi `LoadLichHen()`.
3. Actor nhập `MaDangKy`.
4. Actor bấm `Tìm kiếm`.
5. `BtnTim_Click()` gọi `DangKyThueBLL.LayChiTietDangKy(maDangKy)`.
6. BLL gọi `DangKyThueDAL.LayChiTietDangKy`.
7. DAL trả chi tiết phiếu.
8. Nếu không có phiếu, GUI báo lỗi.
9. Nếu có phiếu, GUI gọi `DangKyThueBLL.TaoDtoTuDongChiTiet(row)`.
10. GUI hiển thị khách hàng và nhu cầu.
11. GUI gọi `TraCuuPhongGiuongBLL.TraCuuPhongGiuongKhaDung(phieuDangKy)`.
12. BLL/DAL tra cứu phòng hoặc giường phù hợp.
13. GUI hiển thị kết quả lên `dgvKetQua`.
14. Actor chọn dòng phòng/giường.
15. Actor nhập ngày giờ hẹn và ghi chú.
16. Actor bấm `Lưu lịch hẹn`.
17. `BtnLuu_Click()` tạo `LichXemPhongDTO`.
18. GUI gọi `LichXemPhongBLL.TaoLich(lich)`.
19. BLL validate thông tin lịch.
20. BLL gọi `LichXemPhongDAL.TaoLich(lich)`.
21. DAL sinh mã lịch.
22. Nếu chưa có mã giường, DAL tìm giường trống trong phòng.
23. DAL insert `Lich_xem_phong`.
24. DAL update `Dang_ky_thue.TrangThai`.
25. DAL trả mã lịch.
26. GUI báo thành công và reload `dgvLichHen`.

## 7. Ghi chú để vẽ 3-layer

- UC này có 3 cụm BLL: đăng ký thuê, tra cứu phòng/giường, lịch xem phòng.
- Khi vẽ sequence, nên thể hiện phần tìm phiếu trước, phần tra cứu giữa, phần lưu lịch cuối.

## 8. Phần báo cáo bổ sung

### 8.1. Các tiêu chí đánh giá giao diện

**Quy trình và thói quen người dùng:** Màn hình được chia theo đúng trình tự làm việc của nhân viên sale: nhập mã đăng ký, xem thông tin khách hàng và nhu cầu, xem danh sách phòng/giường phù hợp, sau đó chọn thời gian hẹn và lưu lịch. Cách bố trí này giúp nhân viên kiểm tra lại thông tin trước khi tạo lịch.

**Tối thiểu hóa sự bất ngờ và có khả năng khôi phục:** Thông tin khách hàng và nhu cầu được đặt ở chế độ readonly để tránh sửa nhầm dữ liệu của phiếu đăng ký. Nút `Làm mới` giúp xóa phiếu đang xử lý và đưa màn hình về trạng thái ban đầu.

**Tính nhất quán:** Dữ liệu danh sách phòng/giường phù hợp và danh sách lịch hẹn đều dùng `DataGridView`. Thời gian hẹn dùng `DateTimePicker` để tránh sai định dạng. Nút `Lưu lịch hẹn` dùng màu primary vì đây là hành động chính.

### 8.2. Chi tiết từng bước thực hiện

**Bước 1:** Nhân viên nhập mã đăng ký vào `txtMaDangKy`.

**Bước 2:** Nhân viên nhấn `Tìm kiếm`.

**Bước 3:** Hệ thống tìm chi tiết phiếu đăng ký. Nếu không tồn tại, hệ thống hiển thị lỗi.

**Bước 4:** Nếu tìm thấy, hệ thống hiển thị thông tin khách hàng và nhu cầu thuê.

**Bước 5:** Hệ thống dùng nhu cầu trong phiếu để tra cứu phòng/giường phù hợp và hiển thị lên `dgvKetQua`.

**Bước 6:** Nhân viên chọn một phòng/giường trong danh sách kết quả.

**Bước 7:** Nhân viên chọn ngày giờ hẹn, nhập ghi chú nếu có.

**Bước 8:** Nhân viên nhấn `Lưu lịch hẹn`.

**Bước 9:** Hệ thống lưu lịch xem phòng, cập nhật trạng thái phiếu đăng ký và reload danh sách lịch hẹn.

### 8.3. Xử lý logic và thành phần điều khiển

**Lựa chọn thành phần điều khiển:** `TextBox` dùng để nhập mã đăng ký và ghi chú. Các ô thông tin khách hàng/nhu cầu là `TextBox` readonly để bảo vệ dữ liệu. `DataGridView` dùng để hiển thị danh sách phòng/giường và lịch hẹn. `DateTimePicker` dùng cho ngày giờ hẹn để đảm bảo dữ liệu thời gian hợp lệ.

**Logic bắt lỗi:** `LichXemPhongBLL.TaoLich()` kiểm tra phải có mã đăng ký, phải chọn phòng/giường hợp lệ và thời gian hẹn phải lớn hơn thời điểm hiện tại. Nếu sai, BLL throw lỗi để GUI hiển thị thông báo.

**Logic nghiệp vụ:** UC này phối hợp nhiều BLL. `DangKyThueBLL.LayChiTietDangKy()` lấy phiếu đăng ký, `TraCuuPhongGiuongBLL.TraCuuPhongGiuongKhaDung()` tìm phòng/giường phù hợp, sau đó `LichXemPhongBLL.TaoLich()` lưu lịch. DAL insert `Lich_xem_phong` và update `Dang_ky_thue.TrangThai = Đã hẹn xem phòng`.

# Feature 04 - Lập lịch xem phòng

## 1. Mục đích

Cho phép nhân viên sale tìm phiếu đăng ký thuê, xem danh sách phòng/giường phù hợp, chọn phòng/giường và tạo lịch hẹn xem phòng.

## 2. GUI - Presentation layer

File: `UI/DangKyThue/frmLichXemPhong.cs`

### Thành phần giao diện

| Nhóm | Thành phần | Kiểu | Vai trò |
|---|---|---|---|
| Tra cứu phiếu | `txtMaDangKy` | `TextBox` | Nhập mã đăng ký. |
| Tra cứu phiếu | `btnTim` | `Button` | Tìm phiếu đăng ký. |
| Tra cứu phiếu | `txtThongTinKhach` | `TextBox` | Hiển thị khách hàng, readonly. |
| Tra cứu phiếu | `txtNhuCau` | `TextBox` | Hiển thị nhu cầu thuê, readonly. |
| Kết quả phù hợp | `dgvKetQua` | `DataGridView` | Danh sách phòng/giường phù hợp. |
| Lịch hẹn | `dtpNgayGioHen` | `DateTimePicker` | Chọn ngày giờ hẹn. |
| Lịch hẹn | `txtGhiChu` | `TextBox` | Ghi chú lịch hẹn. |
| Lịch hẹn | `btnLuu` | `Button` | Lưu lịch hẹn. |
| Lịch hẹn | `btnLamMoi` | `Button` | Reset form. |
| Danh sách | `dgvLichHen` | `DataGridView` | Danh sách lịch hẹn đã lập. |
| Trạng thái | `lblTrangThai` | `Label` | Thông báo. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo màn hình. |
| `ResetForm()` | Reset thông tin phiếu, kết quả và ngày giờ hẹn. |
| `LoadLichHen()` | Nạp danh sách lịch hẹn từ BLL. |
| `BtnTim_Click(sender, e)` | Tìm phiếu đăng ký, map DTO, tra cứu phòng/giường phù hợp. |
| `BtnLuu_Click(sender, e)` | Kiểm tra phiếu và dòng chọn, tạo `LichXemPhongDTO`, gọi BLL lưu lịch. |
| `AddField(...)`, `InlineLabel(...)` | Hàm hỗ trợ dựng UI. |

## 3. DTO liên quan

File: `DTO/LichXemPhongDTO.cs`

| Thuộc tính | Vai trò |
|---|---|
| `MaLich` | Mã lịch. |
| `MaDangKy` | Mã phiếu đăng ký. |
| `LoaiDoiTuong` | Loại kết quả chọn: phòng/giường. |
| `MaPhong` | Phòng được chọn. |
| `MaGiuong` | Giường được chọn nếu có. |
| `NgayGioHen` | Thời gian hẹn xem phòng. |
| `GhiChu` | Ghi chú. |
| `TrangThai` | Trạng thái lịch. |

DTO dùng lại: `PhieuDangKyThueDTO`

## 4. BLL - Business Logic Layer

File: `BLL/DangKyThue/LichXemPhongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | Lấy danh sách lịch hẹn đã lập. |
| `TaoLich(lich)` | Validate thông tin lịch, gọi DAL tạo lịch. |

BLL dùng lại:

| BLL | Hàm | Vai trò |
|---|---|---|
| `DangKyThueBLL` | `LayChiTietDangKy(maDangKy)` | Lấy chi tiết phiếu đăng ký. |
| `DangKyThueBLL` | `TaoDtoTuDongChiTiet(row)` | Map phiếu đăng ký sang DTO. |
| `TraCuuPhongGiuongBLL` | `TraCuuPhongGiuongKhaDung(phieuDangKy)` | Tìm phòng/giường phù hợp với nhu cầu của phiếu. |

## 5. DAL - Data Access Layer

File: `DAL/DangKyThue/LichXemPhongDAL.cs`

| Hàm | Vai trò |
|---|---|
| `LayDanhSach()` | SELECT lịch xem phòng join `Giuong`, `Phong`. |
| `TaoLich(lich)` | Tạo mã lịch, xác định giường nếu chỉ có phòng, insert `Lich_xem_phong`, cập nhật trạng thái `Dang_ky_thue`. |
| `TaoMaMoi()` | Sinh mã `Lxxx`. |

DAL dùng lại:

| DAL | Hàm | Vai trò |
|---|---|---|
| `DangKyThueDAL` | `LayChiTietDangKy(maDangKy)` | Đọc chi tiết phiếu đăng ký. |
| `PhongDAL/GiuongDAL` | `DocDanhSach...Trong` | Tra cứu kết quả phù hợp qua `TraCuuPhongGiuongBLL`. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Dang_ky_thue` | Đọc phiếu đăng ký và cập nhật trạng thái `Đã hẹn xem phòng`. |
| `Khach_hang` | Lấy thông tin khách hàng theo phiếu. |
| `Phong`, `Giuong`, `Chi_Nhanh` | Tra cứu đối tượng phù hợp. |
| `Lich_xem_phong` | Lưu lịch hẹn xem phòng. |

## 7. Sequence để tự vẽ

1. Nhân viên sale mở `frmLichXemPhong`.
2. GUI gọi `LoadLichHen()` để nạp danh sách lịch hẹn.
3. Nhân viên nhập `MaDangKy` và bấm `Tìm kiếm`.
4. `BtnTim_Click()` gọi `DangKyThueBLL.LayChiTietDangKy(maDangKy)`.
5. BLL gọi `DangKyThueDAL.LayChiTietDangKy`.
6. Nếu không tìm thấy, GUI hiển thị lỗi.
7. Nếu tìm thấy, GUI gọi `DangKyThueBLL.TaoDtoTuDongChiTiet(row)`.
8. GUI hiển thị thông tin khách hàng và nhu cầu thuê.
9. GUI gọi `TraCuuPhongGiuongBLL.TraCuuPhongGiuongKhaDung(phieuDangKy)`.
10. BLL tra cứu phòng/giường phù hợp và trả danh sách.
11. GUI bind danh sách lên `dgvKetQua`.
12. Nhân viên chọn một dòng phòng/giường.
13. Nhân viên nhập ngày giờ hẹn và ghi chú.
14. Nhân viên bấm `Lưu lịch hẹn`.
15. `BtnLuu_Click()` tạo `LichXemPhongDTO`.
16. GUI gọi `LichXemPhongBLL.TaoLich(lich)`.
17. BLL validate mã đăng ký, đối tượng, phòng/giường, thời gian hẹn.
18. BLL gọi `LichXemPhongDAL.TaoLich(lich)`.
19. DAL sinh `MaLich`.
20. Nếu chưa có `MaGiuong`, DAL tìm giường trống đầu tiên của phòng.
21. DAL insert `Lich_xem_phong`.
22. DAL update `Dang_ky_thue.TrangThai = Đã hẹn xem phòng`.
23. DAL trả `MaLich`.
24. GUI hiển thị thành công và gọi `LoadLichHen()`.

# Feature 03 - Tra cứu phòng/giường phù hợp

## 1. Mục đích

Cho phép nhân viên sale nhập bộ lọc để xem danh sách phòng hoặc giường phù hợp. Feature này chỉ đọc dữ liệu, không tạo phiếu đăng ký thuê.

## 2. GUI - Presentation layer

File: `UI/DangKyThue/frmTraCuuPhong.cs`

### Thành phần giao diện

| Thành phần | Kiểu | Vai trò |
|---|---|---|
| `cboHinhThucThue` | `ComboBox` | Chọn `Thuê nguyên phòng` hoặc `Thuê giường`. |
| `cboKhuVuc` | `ComboBox` | Khu vực lấy từ `Chi_Nhanh.KhuVuc`. |
| `cboGioiTinh` | `ComboBox` | Giới tính/quy định phòng lấy từ `Phong.GioiTinhQuyDinh`. |
| `cboLoaiPhong` | `ComboBox` | Loại phòng lấy từ `Phong.LoaiPhong`. |
| `nudSoNguoi` | `NumericUpDown` | Số người. |
| `nudGiaToiDa` | `NumericUpDown` | Giá tối đa. |
| `dtpNgayVao` | `DateTimePicker` | Ngày vào dự kiến. |
| `txtTieuChi` | `TextBox` | Tiêu chí ưu tiên. |
| `btnTraCuu` | `Button` | Thực hiện tra cứu. |
| `btnLamMoi` | `Button` | Reset bộ lọc. |
| `dgvKetQua` | `DataGridView` | Hiển thị danh sách phòng/giường phù hợp. |
| `lblTrangThai` | `Label` | Hiển thị trạng thái tra cứu. |

### Hàm trong GUI

| Hàm | Vai trò |
|---|---|
| `InitializeComponent()` | Khởi tạo màn hình. |
| `LoadDefaults()` | Nạp combobox và cấu hình numeric/date. |
| `ResetForm()` | Reset bộ lọc và bảng kết quả. |
| `BtnTraCuu_Click(sender, e)` | Tạo DTO, kiểm tra bộ lọc, gọi BLL tra cứu, bind `DataGridView`. |
| `TaoThongTinDangKyTuBoLoc()` | Tạo `PhieuDangKyThueDTO` từ bộ lọc. |
| `AddFilter(...)` | Hàm hỗ trợ dựng layout filter. |

## 3. DTO và result

DTO dùng chung: `DTO/PhieuDangKyThueDTO.cs`

Các thuộc tính dùng cho tra cứu:

- `HinhThucThue`
- `KhuVucMongMuon`
- `GioiTinh`
- `LoaiPhong`
- `SoNguoiDuKien`
- `GiaToiDa`
- `NgayVaoDuKien`
- `TieuChiUuTien`

Result object: `TraCuuPhongGiuongResult` trong `BLL/DangKyThue/TraCuuPhongGiuongBLL.cs`

| Thuộc tính/hàm | Vai trò |
|---|---|
| `ThanhCong` | Kết quả kiểm tra/tra cứu. |
| `KetQua` | `DataTable` kết quả. |
| `ThongBao` | Thông báo hiển thị. |
| `TaoThanhCong(ketQua, thongBao)` | Tạo result thành công. |
| `TaoThatBai(thongBao)` | Tạo result thất bại. |

## 4. BLL - Business Logic Layer

File: `BLL/DangKyThue/TraCuuPhongGiuongBLL.cs`

| Hàm | Vai trò |
|---|---|
| `KiemTraBoLoc(thongTinDK)` | Kiểm tra bộ lọc trước khi tra cứu. |
| `TraCuuPhongGiuongPhuHop(thongTinDK)` | Hàm điều phối chính, rẽ nhánh theo hình thức thuê. |
| `TraCuuPhongGiuongKhaDung(tieuChi)` | Alias tên cũ, gọi lại `TraCuuPhongGiuongPhuHop`. |
| `TraCuuPhongPhuHop(thongTinDK)` | Gọi `PhongBLL.TraCuuPhongPhuHop`. |
| `TraCuuGiuongPhuHop(thongTinDK)` | Gọi `GiuongBLL.TraCuuGiuongPhuHop`. |
| `LayLoiBoLoc(tieuChi)` | Hàm private gom lỗi bộ lọc. |
| `LaHinhThucThueHopLe(hinhThucThue)` | Kiểm tra hình thức thuê. |
| `LaThueNguyenPhong(hinhThucThue)` | Xác định nhánh tra cứu phòng. |

BLL danh mục và nhánh:

| BLL | Hàm | Vai trò |
|---|---|---|
| `ChiNhanhBLL` | `LayDanhMucKhuVuc()` | Nạp combobox khu vực. |
| `PhongBLL` | `LayDanhMucGioiTinhQuyDinh()` | Nạp combobox giới tính. |
| `PhongBLL` | `LayDanhMucLoaiPhong()` | Nạp combobox loại phòng. |
| `PhongBLL` | `TraCuuPhongPhuHop(tieuChi)` | Gọi DAL đọc danh sách phòng phù hợp. |
| `GiuongBLL` | `TraCuuGiuongPhuHop(tieuChi)` | Gọi DAL đọc danh sách giường phù hợp. |

## 5. DAL - Data Access Layer

| File | Hàm | Vai trò |
|---|---|---|
| `DAL/QuanTriHeThong/ChiNhanhDAL.cs` | `LayDanhMucKhuVuc()` | Đọc khu vực đang hoạt động. |
| `DAL/QuanTriHeThong/PhongDAL.cs` | `LayDanhMucLoaiPhong()` | Đọc loại phòng. |
| `DAL/QuanTriHeThong/PhongDAL.cs` | `LayDanhMucGioiTinhQuyDinh()` | Đọc giới tính/quy định phòng. |
| `DAL/QuanTriHeThong/PhongDAL.cs` | `DocDanhSachPhongTrong(tieuChi)` | SELECT phòng phù hợp theo khu vực, giới tính, loại phòng, giá, số giường trống. |
| `DAL/QuanTriHeThong/PhongDAL.cs` | `TraCuuPhongKhaDung(tieuChi)` | Alias gọi `DocDanhSachPhongTrong`. |
| `DAL/QuanTriHeThong/GiuongDAL.cs` | `DocDanhSachGiuongTrong(tieuChi)` | SELECT giường trống phù hợp theo phòng, chi nhánh, giá. |
| `DAL/QuanTriHeThong/GiuongDAL.cs` | `TraCuuGiuongKhaDung(tieuChi)` | Alias gọi `DocDanhSachGiuongTrong`. |

## 6. Bảng CSDL liên quan

| Bảng | Vai trò |
|---|---|
| `Chi_Nhanh` | Lọc khu vực và hiển thị tên chi nhánh. |
| `Phong` | Lọc loại phòng, giới tính, trạng thái, giá phòng. |
| `Giuong` | Lọc trạng thái giường trống và giá giường. |

## 7. Sequence để tự vẽ

1. Nhân viên sale mở `frmTraCuuPhong`.
2. GUI gọi `LoadDefaults()` để nạp combobox.
3. Nhân viên nhập/chọn bộ lọc.
4. Nhân viên bấm `Tra cứu`.
5. `BtnTraCuu_Click()` gọi `TaoThongTinDangKyTuBoLoc()`.
6. GUI gọi `TraCuuPhongGiuongBLL.KiemTraBoLoc(thongTinDK)`.
7. Nếu bộ lọc sai, BLL trả result thất bại, GUI hiển thị lỗi.
8. Nếu bộ lọc đúng, GUI gọi `TraCuuPhongGiuongBLL.TraCuuPhongGiuongPhuHop(thongTinDK)`.
9. BLL kiểm tra hình thức thuê.
10. Nếu `Thuê nguyên phòng`, BLL gọi `PhongBLL.TraCuuPhongPhuHop`.
11. `PhongBLL` gọi `PhongDAL.DocDanhSachPhongTrong`.
12. DAL SELECT `Phong` + `Chi_Nhanh` + `Giuong`, trả `DataTable`.
13. Nếu `Thuê giường`, BLL gọi `GiuongBLL.TraCuuGiuongPhuHop`.
14. `GiuongBLL` gọi `GiuongDAL.DocDanhSachGiuongTrong`.
15. DAL SELECT `Giuong` + `Phong` + `Chi_Nhanh`, trả `DataTable`.
16. BLL đóng gói `TraCuuPhongGiuongResult`.
17. GUI bind `result.KetQua` lên `dgvKetQua`.
18. GUI hiển thị `result.ThongBao`.

## 8. Ghi chú về Stored Procedure

- `DBConnection` có hỗ trợ gọi stored procedure qua `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar`.
- Tuy nhiên code hiện tại của feature này đang dùng SQL text qua `ExecuteSqlQuery`.
- Nếu báo cáo yêu cầu đúng “Tra cứu phòng bằng Stored Procedure”, cần bổ sung stored procedure trong database và đổi `PhongDAL.DocDanhSachPhongTrong`, `GiuongDAL.DocDanhSachGiuongTrong` sang gọi `ExecuteQuery`.

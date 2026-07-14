# Mục lục tài liệu feature - Thạch Ý

Module: **Đăng ký thuê & Quản trị hệ thống**

Các file dưới đây được viết theo code hiện tại để hỗ trợ vẽ lại 3-layer và sequence.

| STT | Feature | File |
|---|---|---|
| 1 | Đăng nhập và phân quyền RBAC | `Docs/Feature_01_DangNhap_PhanQuyen_RBAC.md` |
| 2 | Tạo đăng ký thuê | `Docs/Feature_02_DangKyThue.md` |
| 3 | Tra cứu phòng/giường phù hợp | `Docs/Feature_03_TraCuuPhongGiuong.md` |
| 4 | Lập lịch xem phòng | `Docs/Feature_04_LapLichXemPhong.md` |
| 5 | CRUD Phòng | `Docs/Feature_05_CRUDPhong.md` |
| 6 | CRUD Giường | `Docs/Feature_06_CRUDGiuong.md` |
| 7 | CRUD Chi nhánh | `Docs/Feature_07_CRUDChiNhanh.md` |
| 8 | CRUD Nhân viên | `Docs/Feature_08_CRUDNhanVien.md` |
| 9 | Dashboard thống kê và menu theo quyền | `Docs/Feature_09_DashboardThongKe.md` |
| 10 | Hash mật khẩu | `Docs/Feature_10_HashMatKhau.md` |

## Cách dùng để vẽ

Trong mỗi file, dùng các mục sau:

1. **GUI - Presentation layer**: lấy class form, control, event/hàm.
2. **BLL - Business Logic Layer**: lấy class BLL và các hàm nghiệp vụ.
3. **DAL - Data Access Layer**: lấy class DAL, hàm truy vấn/lưu dữ liệu.
4. **Bảng CSDL liên quan**: lấy các bảng đưa vào tầng database.
5. **Sequence để tự vẽ**: chuyển các bước thành sequence diagram.

## Ghi chú lệch code/đặc tả cần nhớ

- Feature `Tra cứu phòng/giường phù hợp` hiện dùng SQL text trong DAL, chưa gọi stored procedure dù `DBConnection` có hỗ trợ stored procedure.
- Feature `Hash mật khẩu` hiện hash khi đăng nhập, nhưng khi lưu nhân viên mới/chỉnh mật khẩu thì `NhanVienBLL` chưa hash trước khi lưu.
- Các thao tác xóa phòng, giường, chi nhánh, nhân viên đều là soft-delete/khóa trạng thái, không xóa vật lý.

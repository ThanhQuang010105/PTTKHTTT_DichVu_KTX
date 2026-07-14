# Mục lục 3-layer theo Use Case - Thạch Ý

Module: **Đăng ký thuê & Quản trị hệ thống**

Bộ tài liệu này chia đúng theo **Use Case** trong phân công, không chia theo feature cài đặt phụ.

| STT | Use Case | File |
|---|---|---|
| 1 | Đăng nhập | `Docs/UseCases_ThachY/UC_01_DangNhap.md` |
| 2 | Đăng ký thuê | `Docs/UseCases_ThachY/UC_02_DangKyThue.md` |
| 3 | Tra cứu phòng/giường | `Docs/UseCases_ThachY/UC_03_TraCuuPhongGiuong.md` |
| 4 | Lập lịch xem phòng | `Docs/UseCases_ThachY/UC_04_LapLichXemPhong.md` |
| 5 | Quản lý phòng/giường | `Docs/UseCases_ThachY/UC_05_QuanLyPhongGiuong.md` |
| 6 | Quản lý chi nhánh | `Docs/UseCases_ThachY/UC_06_QuanLyChiNhanh.md` |
| 7 | Quản lý nhân viên | `Docs/UseCases_ThachY/UC_07_QuanLyNhanVien.md` |

## Cách dùng để vẽ lại 3-layer

Trong mỗi file:

1. Lấy mục **GUI / Presentation layer** để vẽ lớp màn hình, control và event.
2. Lấy mục **BLL/BAL - Business Logic layer** để vẽ lớp xử lý nghiệp vụ.
3. Lấy mục **DAL - Data Access layer** để vẽ lớp truy xuất CSDL.
4. Lấy mục **DTO và Database** để thêm object truyền dữ liệu và bảng liên quan.
5. Lấy mục **Sequence để vẽ** để dựng sequence diagram.

Ghi chú: trong code đặt tên là `BLL`; nếu báo cáo gọi là `BAL` thì xem hai tên này là cùng tầng Business Logic.

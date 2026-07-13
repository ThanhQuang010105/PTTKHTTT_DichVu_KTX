using System;
using HomeStayDorm.DAL.DatCoc;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DatCoc
{
    public class HoSoCuTru
    {
        private HoSoCuTruDB db = new HoSoCuTruDB();

        public string LuuHoSoCuTru(HoSoCuTruDTO thongTinKhachHang, int inMemoryCount, int soGiuongCoc)
        {
            if (!KiemTraBatBuoc(thongTinKhachHang.hoTen, thongTinKhachHang.cccd, thongTinKhachHang.sdt, thongTinKhachHang.queQuan, thongTinKhachHang.gioiTinh))
            {
                return "Thiếu thông tin";
            }

            int currentDbCount = db.CountNguoiOByDatCoc(thongTinKhachHang.maDatCoc);
            int totalCount = currentDbCount + inMemoryCount;

            if (!KiemTraGioiHanNguoiO(totalCount, soGiuongCoc))
            {
                return "Vượt giới hạn";
            }

            // Theo thiết kế, có thể insert thẳng vào DB hoặc giữ ở memory rồi insert 1 lượt.
            // Để tuân thủ đúng Sequence Diagram (LuuHoSoCuTru gọi ngay InsertHoSoCuTru),
            // ta sẽ insert trực tiếp vào DB ngay khi người dùng nhấn Thêm vào danh sách (hoặc Gửi hồ sơ).
            // Dựa vào UI "Xóa", nếu lưu trực tiếp thì nút Xóa phải gọi DB. 
            // Nếu lưu vào memory, thì InsertHoSoCuTru sẽ được gọi trong vòng lặp ở GUI.
            // Ta sẽ viết hàm này hỗ trợ cho việc insert từng khách hàng.
            
            bool success = db.InsertHoSoCuTru(thongTinKhachHang);
            if (success)
            {
                return "Thành công";
            }
            return "Lỗi CSDL";
        }

        public bool KiemTraBatBuoc(string hoTen, string cccd, string sdt, string queQuan, string gioiTinh)
        {
            if (string.IsNullOrEmpty(hoTen) || 
                string.IsNullOrEmpty(cccd) || 
                string.IsNullOrEmpty(sdt) || 
                string.IsNullOrEmpty(queQuan) || 
                string.IsNullOrEmpty(gioiTinh))
            {
                return false;
            }
            return true;
        }

        public bool KiemTraGioiHanNguoiO(int currentTotal, int maxAllowed)
        {
            // Kiểm tra xem số người đã có cộng thêm 1 (nếu chuẩn bị thêm) có vượt số giường cọc không
            // Nhưng currentTotal ở đây được tính là tổng số chuẩn bị thêm + đã có trong DB
            if (currentTotal >= maxAllowed)
            {
                return false;
            }
            return true;
        }

        public System.Collections.Generic.List<HoSoCuTruDTO> GetHoSoCuTruByDatCoc(string maDatCoc)
        {
            return db.GetHoSoCuTruByDatCoc(maDatCoc);
        }

        public bool DeleteHoSoCuTru(int maKH)
        {
            return db.DeleteHoSoCuTru(maKH);
        }

        public bool UpdateTrangThaiPhieuDatCoc(string maDatCoc, string trangThai)
        {
            return db.UpdateTrangThaiPhieuDatCoc(maDatCoc, trangThai);
        }
    }
}

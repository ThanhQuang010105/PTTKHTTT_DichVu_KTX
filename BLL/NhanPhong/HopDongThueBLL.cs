using System;
using System.Collections.Generic;
using HomeStayDorm.DTO;
using HomeStayDorm.DAL;

namespace HomeStayDorm.BLL
{
    public class HopDongThueBLL
    {
        private HopDongThueDAL dal = new HopDongThueDAL();

        public List<HoSoCuTru_DTO> GetHoSoDaDuyet()
        {
            return dal.GetHoSoDaDuyet();
        }

        public ChiTietHoSo_DTO LayChiTietHoSo(string maHoSo)
        {
            return dal.GetChiTietHoSo(maHoSo);
        }

        public List<DichVu_DTO> GetDanhSachDichVu()
        {
            return dal.GetDanhSachDichVu();
        }

        public bool XuLyTaoHopDong(HopDongThue_DTO hd, List<string> selectedServices)
        {
            // Validate Logic cơ bản dựa trên DTO mới
            if (hd == null || string.IsNullOrEmpty(hd.MaKH) || string.IsNullOrEmpty(hd.MaPhong))
            {
                return false;
            }

            if (hd.ThoiHanThang <= 0)
            {
                return false; // Hợp đồng phải có thời hạn
            }

            // Thực thi ghi xuống DB
            bool isInserted = dal.InsertHopDong(hd);
            if (isInserted)
            {
                // Nếu tạo HĐ thành công thì thêm các dịch vụ đi kèm
                foreach (var maDV in selectedServices)
                {
                    dal.InsertHopDongDichVu(hd.MaHopDong, maDV);
                }
                return true;
            }
            return false;
        }
    }
}
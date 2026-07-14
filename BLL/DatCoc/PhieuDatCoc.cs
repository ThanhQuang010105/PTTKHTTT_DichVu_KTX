using System;
using System.Collections.Generic;
using HomeStayDorm.DAL.DatCoc;
using HomeStayDorm.DTO;

namespace HomeStayDorm.BLL.DatCoc
{
    public class PhieuDatCoc
    {
        private PhieuDatCocDB db = new PhieuDatCocDB();

        public List<PhieuDatCocDTO> TimKiemPhieuDatCoc(string maPhieu, string sdt, string cccd, string trangThai)
        {
            return db.GetPhieuDatCocByFilter(maPhieu, sdt, cccd, trangThai);
        }

        public bool KiemTraDieuKienLapHoSo(string maPhieu)
        {
            string trangThai = db.GetStatusByMaPhieu(maPhieu);
            if (string.IsNullOrEmpty(trangThai))
            {
                return false;
            }

            if (trangThai == "Đã hủy / Quá hạn")
            {
                return false;
            }

            return true;
        }
    }
}

using System;

namespace HomeStayDorm.DTO
{
    public class HoSoCuTruDTO
    {
        public int STT { get; set; }
        public string maKH { get; set; }
        public string hoTen { get; set; }
        public string cccd { get; set; }
        public string sdt { get; set; }
        public string gioiTinh { get; set; }
        public string queQuan { get; set; }
        public string maDatCoc { get; set; }
        public bool isNew { get; set; } = true;
    }
}

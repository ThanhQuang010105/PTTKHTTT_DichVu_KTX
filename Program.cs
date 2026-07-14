using System;
using System.Windows.Forms;
using HomeStayDorm.UI.QuanTriHeThong;

namespace HomeStayDorm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();


            // Application.Run(new UI.frmDoiSoatCoc());
            // Application.Run(new UI.frmTaoYeuCauTraPhong());

            // Application.Run(new HomeStayDorm.GUI.DatCoc.FrmTraCuuDatCoc());


            // Application.Run(new frmDangNhap());
            Application.Run(new UI.frmThongTinKhachHang());


        }
    }
}

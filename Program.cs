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
            Application.Run(new frmDangNhap());
        }
    }
}

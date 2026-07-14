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

            // Điểm vào duy nhất: màn hình đăng nhập
            // Sau khi đăng nhập, frmDangNhap sẽ mở frmDashboard
            Application.Run(new frmDangNhap());
        }
    }
}

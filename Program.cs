using System;
using System.Windows.Forms;

namespace HomeStayDorm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Application.Run(new UI.Common.frmMain());
        }
    }
}

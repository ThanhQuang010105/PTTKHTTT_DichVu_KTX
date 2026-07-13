using System.Data;
using HomeStayDorm.DAL.TienIch;

namespace HomeStayDorm.BLL.TienIch
{
    public class BaoCaoBLL
    {
        private readonly BaoCaoDAL _baoCaoDAL = new BaoCaoDAL();

        public DataTable LayThongKeDashboard()
        {
            return _baoCaoDAL.LayThongKeDashboard();
        }
    }
}

using System.Data;

namespace HomeStayDorm.DAL.TienIch
{
    public class BaoCaoDAL
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable LayThongKeDashboard()
        {
            return _db.ExecuteQuery("sp_Dashboard_ThongKe");
        }
    }
}

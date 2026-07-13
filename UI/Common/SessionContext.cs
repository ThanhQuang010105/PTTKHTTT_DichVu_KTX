using HomeStayDorm.DTO;

namespace HomeStayDorm.UI.Common
{
    public static class SessionContext
    {
        public static NhanVienDTO? CurrentUser { get; set; }
    }
}

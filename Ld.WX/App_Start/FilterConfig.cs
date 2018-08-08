using System.Web;
using System.Web.Mvc;

namespace Ld.WX
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Ywl.Web.Mvc.HandleErrorAttribute());
        }
    }
}

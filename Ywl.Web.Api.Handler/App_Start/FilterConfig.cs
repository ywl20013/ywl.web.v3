using System.Web;
using System.Web.Mvc;

namespace Ywl.Web.Api.Handler
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

using SEMS.WebSite.Filters;
using System.Web;
using System.Web.Mvc;

namespace SEMS.WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // 异常统一结果返回
            filters.Add(new ExceptionFilter());

            filters.Add(new AuthorizeAttribute());
        }
    }
}

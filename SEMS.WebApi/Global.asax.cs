using SEMS.DataAccess.AutoMapper;
using SEMS.Infrastructure.Logging;
using SEMS.WebApi.Filters.Logging;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SEMS.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapper.MapperRegister();
            LoggingInitialize();
        }
        private static void LoggingInitialize()
        {
            Log4NetLoggerAdapter adapter = new Log4NetLoggerAdapter();
            LogManager.AddLoggerAdapter(adapter);
        }
    }
}

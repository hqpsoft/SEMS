[assembly: WebActivator.PostApplicationStartMethod(typeof(SEMS.WebApi.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace SEMS.WebApi.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using System.Linq;
    using Mehdime.Entity;
    using Concretes;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IDbContextScopeFactory>(() => new DbContextScopeFactory(), Lifestyle.Scoped);//ע��EF

            var serviceAssembly = typeof(CompanySvc).Assembly;

            var registrations =
                from type in serviceAssembly.GetExportedTypes()
                where type.Namespace == "SEMS.Concretes" && type.GetInterfaces().Any()
                select new { Service = type.GetInterfaces().First(), Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation, Lifestyle.Scoped);
            }
        }
    }
}
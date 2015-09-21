[assembly: WebActivator.PostApplicationStartMethod(typeof(SEMS.WebSite.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SEMS.WebSite.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Mehdime.Entity;
    using Abstracts;
    using System.Linq;
    using Concretes;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
             container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IDbContextScopeFactory>(() => new DbContextScopeFactory(), Lifestyle.Scoped);//×¢²áEF

            // container.Register<ICompanySvc, CompanySvc>(Lifestyle.Scoped);

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
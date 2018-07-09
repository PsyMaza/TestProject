using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Concrete.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace DanfossProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();

			container.RegisterType<IWaterMetersService, WaterMetersService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
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

			container.RegisterType<IBuildingsService, BuildingsService>();
			container.RegisterType<IWaterMetersService, WaterMetersService>();
			


			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
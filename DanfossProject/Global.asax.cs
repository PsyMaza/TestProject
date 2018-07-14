using System.Web.Http;

namespace DanfossProject
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start ()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			UnityConfig.RegisterComponents();
			AutoMapperConfig.Initialize();
		}
	}
}

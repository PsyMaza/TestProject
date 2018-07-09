using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DanfossProject
{
	public static class WebApiConfig
	{
		public static void Register (HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			//config.Routes.MapHttpRoute(
			//	name: null,
			//	routeTemplate: "api/WaterMeters/UpdateBySerialNumber/{serialNumber}"
			//);

			config.Routes.MapHttpRoute(
				name: null,
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}

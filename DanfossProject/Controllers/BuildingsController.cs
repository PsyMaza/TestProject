using DanfossProject.Data.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DanfossProject.Controllers
{
    public class BuildingsController : ApiController
    {
		private IBuildingsService _repository;

		public BuildingsController(IBuildingsService repository)
		{
			_repository = repository;
		}
    }
}

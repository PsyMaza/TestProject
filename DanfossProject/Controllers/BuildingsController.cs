using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DanfossProject.Controllers
{
    public class BuildingsController : ApiController
    {
		private IBuildingsService _buildingsService;

		public BuildingsController(IBuildingsService service)
		{
			_buildingsService = service;
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<BuildingReturnModel>))]
		public async Task<IEnumerable<BuildingReturnModel>> GetAll()
		{
			return await _buildingsService.GetAll();
		}

		[HttpGet]
		[ResponseType(typeof(BuildingReturnModel))]
		public async Task<IHttpActionResult> GetById([FromUri]int id)
		{
			BuildingReturnModel waterMeter = await _buildingsService.GetById(id);

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpGet]
		[ResponseType(typeof(BuildingReturnModel))]
		public async Task<IHttpActionResult> GetBuildingWithMinConsumption()
		{
			BuildingReturnModel waterMeter = await _buildingsService.GetBuildingWithMinConsumption();

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpGet]
		[ResponseType(typeof(BuildingReturnModel))]
		public async Task<IHttpActionResult> GetBuildingWithMaxConsumption()
		{
			BuildingReturnModel waterMeter = await _buildingsService.GetBuildingWithMaxConsumption();

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpPost]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Add(BuildingCreateModel building)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Response result = await _buildingsService.Add(building);

			if (result.Ok) return StatusCode(HttpStatusCode.Created);
			else return BadRequest(result.Message);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateById(int id, BuildingUpdateModel building)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != building.Id || id == 0)
			{
				return BadRequest();
			}

			Response result = await _buildingsService.UpdateById(id, building);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		[HttpDelete]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Delete(int id)
		{
			Response result = await _buildingsService.Delete(id);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_buildingsService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

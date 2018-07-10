using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models.Entities;
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
		[ResponseType(typeof(IEnumerable<BuildingModel>))]
		public async Task<IEnumerable<BuildingModel>> GetAll()
		{
			return await _buildingsService.GetAll();
		}

		[HttpGet]
		[ResponseType(typeof(BuildingModel))]
		public async Task<IHttpActionResult> GetById([FromUri]int id)
		{
			BuildingModel waterMeter = await _buildingsService.GetById(id);

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpGet]
		[ResponseType(typeof(BuildingModel))]
		public async Task<IHttpActionResult> GetBuildingWithMinConsumption()
		{
			BuildingModel waterMeter = await _buildingsService.GetBuildingWithMinConsumption();

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpGet]
		[ResponseType(typeof(BuildingModel))]
		public async Task<IHttpActionResult> GetBuildingWithMaxConsumption()
		{
			BuildingModel waterMeter = await _buildingsService.GetBuildingWithMaxConsumption();

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpPost]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Add(BuildingModel building)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bool result = await _buildingsService.Add(building);

			if (result) return StatusCode(HttpStatusCode.Created);
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateById(int id, BuildingModel building)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != building.Id || id == 0)
			{
				return BadRequest();
			}

			bool result = await _buildingsService.UpdateById(id, building);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpDelete]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Delete(int id)
		{
			bool result = await _buildingsService.Delete(id);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}
	}
}

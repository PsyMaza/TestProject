using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DanfossProject.Data;
using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;

namespace DanfossProject.Controllers
{
	public class WaterMetersController : ApiController
	{
		private IWaterMetersService _waterMetersService;

		public WaterMetersController(IWaterMetersService service)
		{
			_waterMetersService = service;
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<WaterMeterReturnModel>))]
		public async Task<IEnumerable<WaterMeterReturnModel>> GetAll()
		{
			return await _waterMetersService.GetAll();
		}

		[HttpGet]
		[ResponseType(typeof(WaterMeterReturnModel))]
		public async Task<IHttpActionResult> GetById(int id)
		{
			WaterMeterReturnModel waterMeter = await _waterMetersService.GetById(id);

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpPost]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Add(WaterMeterCreateModel waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Response result = await _waterMetersService.Add(waterMeter);

			if (result.Ok) return StatusCode(HttpStatusCode.Created);
			else return BadRequest(result.Message);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateById(int id, WaterMeterUpdateModel waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != waterMeter.Id)
			{
				return BadRequest();
			}

			Response result = await _waterMetersService.UpdateById(id, waterMeter);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		[HttpPut]
		[Route("api/WaterMeters/UpdateBySerialNumber/{serialNumber}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateBySerialNumber(string serialNumber, WaterMeterUpdateModel waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Response result = await _waterMetersService.UpdateBySerialNumber(serialNumber, waterMeter);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateByBuildingId(int id, WaterMeterUpdateModel waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Response result = await _waterMetersService.UpdateByBuildingId(id, waterMeter);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		[HttpDelete]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Delete(int id)
		{
			Response result = await _waterMetersService.Delete(id);

			if (result.Ok) return Ok();
			else return BadRequest(result.Message);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_waterMetersService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
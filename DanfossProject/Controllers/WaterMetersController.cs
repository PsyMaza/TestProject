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
using DanfossProject.Data.Models.Entities;

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
		[ResponseType(typeof(IEnumerable<WaterMeter>))]
		public async Task<IEnumerable<WaterMeter>> GetAll()
		{
			return await _waterMetersService.GetAll();
		}

		[HttpGet]
		[ResponseType(typeof(WaterMeter))]
		public async Task<IHttpActionResult> GetById([FromUri]int id)
		{
			WaterMeter waterMeter = await _waterMetersService.GetById(id);

			if (waterMeter is null)
			{
				return StatusCode(HttpStatusCode.NoContent);
			}

			return Ok(waterMeter);
		}

		[HttpPost]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Add(WaterMeter waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bool result = await _waterMetersService.Add(waterMeter);

			if (result) return StatusCode(HttpStatusCode.Created);
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateById(int id, WaterMeter waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != waterMeter.Id)
			{
				return BadRequest();
			}

			bool result = await _waterMetersService.UpdateById(id, waterMeter);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpPut]
		[Route("api/WaterMeters/UpdateBySerialNumber/{serialNumber}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateBySerialNumber(string serialNumber, WaterMeter waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bool result = await _waterMetersService.UpdateBySerialNumber(serialNumber, waterMeter);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> UpdateByBuildingId(int id, WaterMeter waterMeter)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bool result = await _waterMetersService.UpdateByBuildingId(id, waterMeter);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		[HttpDelete]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> Delete(int id)
		{
			bool result = await _waterMetersService.Delete(id);

			if (result) return Ok();
			else return StatusCode(HttpStatusCode.Forbidden);
		}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}

		//private bool WaterMeterExists(int id)
		//{
		//    return db.WaterMeters.Count(e => e.Id == id) > 0;
		//}
	}
}
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
		
        public async Task<IEnumerable<WaterMeter>> GetWaterMeters()
        {
            return await _waterMetersService.GetAll();
        }
		
        [ResponseType(typeof(WaterMeter))]
        public async Task<IHttpActionResult> GetWaterMeter([FromUri]int id)
        {
			WaterMeter waterMeter = await _waterMetersService.GetById(id);

            if (waterMeter is null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(waterMeter);
        }
				
		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> WaterMeterById(int id, WaterMeter waterMeter)
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

		//// POST: api/WaterMeters
		//[ResponseType(typeof(WaterMeter))]
		//public IHttpActionResult PostWaterMeter(WaterMeter waterMeter)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    db.WaterMeters.Add(waterMeter);
		//    db.SaveChanges();

		//    return CreatedAtRoute("DefaultApi", new { id = waterMeter.Id }, waterMeter);
		//}

		//// DELETE: api/WaterMeters/5
		//[ResponseType(typeof(WaterMeter))]
		//public IHttpActionResult DeleteWaterMeter(int id)
		//{
		//    WaterMeter waterMeter = db.WaterMeters.Find(id);
		//    if (waterMeter == null)
		//    {
		//        return NotFound();
		//    }

		//    db.WaterMeters.Remove(waterMeter);
		//    db.SaveChanges();

		//    return Ok(waterMeter);
		//}

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
using DanfossProject.Data.Models;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Abstract.Services
{
	public interface IWaterMetersService : IDisposable
	{
		Task<WaterMeterReturnModel> GetById(int id);
		Task<IEnumerable<WaterMeterReturnModel>> GetAll();
		Task<Response> Add(WaterMeterCreateModel waterMeter);
		Task<Response> UpdateById(int id, WaterMeterUpdateModel waterMeter);
		Task<Response> UpdateBySerialNumber(string serialNumber, WaterMeterUpdateModel waterMeter);
		Task<Response> UpdateByBuildingId(int buildingId, WaterMeterUpdateModel waterMeter);
		Task<Response> Delete(int id);
	}
}

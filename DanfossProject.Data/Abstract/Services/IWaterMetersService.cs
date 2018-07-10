using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Abstract.Services
{
	public interface IWaterMetersService
	{
		Task<WaterMeterModel> GetById(int id);
		Task<IEnumerable<WaterMeterModel>> GetAll();
		Task<bool> Add(WaterMeterModel waterMeter);
		Task<bool> UpdateById(int id, WaterMeterModel waterMeter);
		Task<bool> UpdateBySerialNumber(string serialNumber, WaterMeterModel waterMeter);
		Task<bool> UpdateByBuildingId(int buildingId, WaterMeterModel waterMeter);
		Task<bool> Delete(int id);
	}
}

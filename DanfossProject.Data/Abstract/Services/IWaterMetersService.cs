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
		Task<WaterMeter> GetById(int id);
		Task<IEnumerable<WaterMeter>> GetAll();
		Task<bool> Add(WaterMeter waterMeter);
		Task<bool> UpdateById(int id, WaterMeter waterMeter);
		Task<bool> UpdateBySerialNumber(string serialNumber, WaterMeter waterMeter);
		Task<bool> UpdateByBuildingId(int buildingId, WaterMeter waterMeter);
		Task<bool> Delete(int id);
	}
}

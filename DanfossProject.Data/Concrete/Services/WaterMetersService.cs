using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Concrete.Services
{
	public class WaterMetersService : IWaterMetersService, IDisposable
	{
		private readonly BaseDatabaseContext _dbContext;

		public WaterMetersService(BaseDatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<WaterMeter> GetById(int id)
		{
				return await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == id);
		}

		public async Task<IEnumerable<WaterMeter>> GetAll()
		{
			return await _dbContext.WaterMeters.ToListAsync();
		}

		public async Task<bool> Add(WaterMeter waterMeter)
		{
				WaterMeter overlap = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == waterMeter.Id);

				if (overlap != null) return false;

				try
				{
					_dbContext.WaterMeters.Add(waterMeter);

					await _dbContext.SaveChangesAsync();

					return true;
				}
				catch
				{
					return false;
				}
			
		}

		public async Task<bool> Delete(int id)
		{
			WaterMeter target = await _dbContext.WaterMeters.FindAsync(id);

			if (target is null) return false;

			try
			{
				_dbContext.WaterMeters.Remove(target);

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> UpdateById(int id, WaterMeter waterMeter)
		{
			WaterMeter target = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == id);

			return await UpdateWaterMeterValues(target, waterMeter);
		}

		public async Task<bool> UpdateBySerialNumber(string serialNumber, WaterMeter waterMeter)
		{
			WaterMeter target = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.SerialNumber.Equals(serialNumber));

			return await UpdateWaterMeterValues(target, waterMeter);
		}

		public async Task<bool> UpdateByBuildingId(int buildingId, WaterMeter waterMeter)
		{
			WaterMeter target = await _dbContext.Buildings
				.Include(b => b.WaterMeter)
				.Where(b => b.Id == buildingId)
				.Select(b => b.WaterMeter)
				.FirstOrDefaultAsync();

			return await UpdateWaterMeterValues(target, waterMeter);
		}

		private async Task<bool> UpdateWaterMeterValues (WaterMeter oldValue, WaterMeter newValue)
		{
			try
			{
				_dbContext.Entry(oldValue).CurrentValues.SetValues(newValue);

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public void Dispose()
		{
			_dbContext.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

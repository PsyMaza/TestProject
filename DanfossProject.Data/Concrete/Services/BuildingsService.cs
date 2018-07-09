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
	public class BuildingsService : IBuildingsService
	{
		private readonly BaseDatabaseContext _dbContext;

		public BuildingsService(BaseDatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Building> GetById(int id)
		{
			return await _dbContext.Buildings.Include(b => b.WaterMeter).FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task<IEnumerable<Building>> GetAll()
		{
			return await _dbContext.Buildings.Include(b => b.WaterMeter).ToListAsync();
		}

		public async Task<Building> GetBuildingWithMaxConsumption()
		{
			return await _dbContext.Buildings
				.Include(b => b.WaterMeter)
				.FirstOrDefaultAsync(b => b.WaterMeter.CounterValue == _dbContext.Buildings.Max(x => x.WaterMeter.CounterValue));
		}

		public async Task<Building> GetBuildingWithMinConsumption()
		{
			return await _dbContext.Buildings.Include(b => b.WaterMeter).FirstOrDefaultAsync(b => b.WaterMeter.CounterValue == _dbContext.Buildings.Min(x => x.WaterMeter.CounterValue));
		}

		public async Task<bool> Add(Building building)
		{
			Building overlap = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == building.Id);

			if (overlap != null) return false;

			try
			{
				_dbContext.Buildings.Add(building);

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> UpdateById(int id, Building building)
		{
			Building target = await _dbContext.Buildings.Include(b => b.WaterMeter).FirstOrDefaultAsync(w => w.Id == id);

			if (target is null) return false;

			try
			{
				_dbContext.Entry(target).CurrentValues.SetValues(building);

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}

		private async Task<bool> UpdateBuildingValues(Building oldValue, Building newValue)
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

		public async Task<bool> Delete(int id)
		{
			Building target = await _dbContext.Buildings.FindAsync(id);

			if (target is null) return false;

			try
			{
				_dbContext.Buildings.Remove(target);

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}

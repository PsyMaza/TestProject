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
	public class BuildingsService : IBuildingsService, IDisposable
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
			try
			{
				_dbContext.Entry(building).State = EntityState.Modified;

				await _dbContext.SaveChangesAsync();

				return true;
			}
			catch (Exception e)
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

		public void Dispose()
		{
			_dbContext.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

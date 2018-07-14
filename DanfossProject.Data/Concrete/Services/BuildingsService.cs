using AutoMapper;
using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;
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

		public async Task<BuildingReturnModel> GetById(int id)
		{
			BuildingModel building = await _dbContext.Buildings.Include(b => b.WaterMeter).FirstOrDefaultAsync(b => b.Id == id);

			return Mapper.Map<BuildingModel, BuildingReturnModel>(building);
		}

		public async Task<IEnumerable<BuildingReturnModel>> GetAll()
		{
			List<BuildingModel> buildings = await _dbContext.Buildings.Include(b => b.WaterMeter).ToListAsync();

			return Mapper.Map<IEnumerable<BuildingModel>, List<BuildingReturnModel>>(buildings);
		}

		public async Task<BuildingReturnModel> GetBuildingWithMaxConsumption()
		{
			BuildingModel building = await _dbContext.Buildings
				.Include(b => b.WaterMeter)
				.FirstOrDefaultAsync(b => b.WaterMeter.CounterValue == _dbContext.Buildings.Max(x => x.WaterMeter.CounterValue));

			return Mapper.Map<BuildingModel, BuildingReturnModel>(building);
		}

		public async Task<BuildingReturnModel> GetBuildingWithMinConsumption()
		{
			BuildingModel building = await _dbContext.Buildings.Include(b => b.WaterMeter).FirstOrDefaultAsync(b => b.WaterMeter.CounterValue == _dbContext.Buildings.Min(x => x.WaterMeter.CounterValue));

			return Mapper.Map<BuildingModel, BuildingReturnModel>(building);
		}

		public async Task<Response> Add(BuildingCreateModel building)
		{
			building.AddressHashCode = building.Address.GetHashCode();

			BuildingModel convertBuilding = Mapper.Map<BuildingCreateModel, BuildingModel>(building);			

			BuildingModel overlapId = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == convertBuilding.Id);

			if (overlapId != null)
				return new Response
				{
					Message = "Такой Id уже используется."
				};

			BuildingModel overlapAddress = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.AddressHashCode == convertBuilding.AddressHashCode);

			if (overlapAddress != null)
				return new Response
				{
					Message = "Такой адрес уже используется."
				};

			if (building.WaterMeterId != 0)
			{
				BuildingModel overlapWaterMeterId = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.WaterMeterId == building.WaterMeterId);

				if (overlapWaterMeterId != null) return new Response
				{
					Message = $"Счетчик с таким s/n уже установлен по адресу {overlapWaterMeterId.Address.ToString()}"
				};
			}


			try
			{
				_dbContext.Buildings.Add(convertBuilding);

				await _dbContext.SaveChangesAsync();

				return new Response { Ok = true };
			}
			catch
			{
				return new Response { Message = "Произошла ошибка при добавлении. Пожалуйсте повторите попытку." };
			}
		}

		public async Task<Response> UpdateById(int id, BuildingUpdateModel building)
		{
			building.AddressHashCode = building.Address.GetHashCode();

			if (building.WaterMeterId == 0)
			{
				building.WaterMeterId = null;
			}
			else
			{
				BuildingModel overlapWaterMeterId = await _dbContext.Buildings.FirstOrDefaultAsync(b => b.WaterMeterId == building.WaterMeterId);

				if (overlapWaterMeterId != null) return new Response {
					Message = $"Счетчик с таким s/n уже установлен по адресу {overlapWaterMeterId.Address.ToString()}"
				};
			}

			BuildingModel convertBuilding = Mapper.Map<BuildingUpdateModel, BuildingModel>(building);

			try
			{
				_dbContext.Entry(convertBuilding).State = EntityState.Modified;

				await _dbContext.SaveChangesAsync();

				return new Response { Ok = true };
			}
			catch
			{
				return new Response { Message = "Произошла ошибка при добавлении. Пожалуйсте повторите попытку." };
			}
		}

		public async Task<Response> Delete(int id)
		{
			BuildingModel target = await _dbContext.Buildings.FindAsync(id);

			if (target is null) return new Response { Message = "Строение не найдено." };

			try
			{
				_dbContext.Buildings.Remove(target);

				await _dbContext.SaveChangesAsync();

				return new Response { Ok = true };
			}
			catch
			{
				return new Response { Message = "Произошла ошибка при удалении. Пожалуйсте повторите попытку." };
			}
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}

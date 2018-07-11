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
	public class WaterMetersService : IWaterMetersService, IDisposable
	{
		private readonly BaseDatabaseContext _dbContext;

		public WaterMetersService(BaseDatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<WaterMeterReturnModel> GetById(int id)
		{
			WaterMeterModel waterMeter = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == id);

			return Mapper.Map<WaterMeterModel, WaterMeterReturnModel>(waterMeter);
		}

		public async Task<IEnumerable<WaterMeterReturnModel>> GetAll()
		{
			List<WaterMeterModel> waterMeter = await _dbContext.WaterMeters.ToListAsync();

			return Mapper.Map<IEnumerable<WaterMeterModel>, List<WaterMeterReturnModel>>(waterMeter);
		}

		public async Task<Response> Add(WaterMeterCreateModel waterMeter)
		{
			WaterMeterModel convertWaterMeter = Mapper.Map<WaterMeterCreateModel, WaterMeterModel>(waterMeter);

			WaterMeterModel overlap = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == waterMeter.Id);

			if (overlap != null) return new Response
			{
				Message = "Такой Id уже используется."
			};

			try
			{
				_dbContext.WaterMeters.Add(convertWaterMeter);

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
			WaterMeterModel target = await _dbContext.WaterMeters.FindAsync(id);

			if (target is null) return new Response { Message = "Счетчик не найден." };

			try
			{
				_dbContext.WaterMeters.Remove(target);

				await _dbContext.SaveChangesAsync();

				return new Response { Ok = true };
			}
			catch
			{
				return new Response { Message = "Произошла ошибка при удалении. Пожалуйсте повторите попытку." };
			}
		}

		public async Task<Response> UpdateById(int id, WaterMeterUpdateModel waterMeter)
		{
			WaterMeterModel targetWaterMeter = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.Id == id);

			if (targetWaterMeter is null) return new Response { Message = "Счетчик не найден." };

			WaterMeterModel convertWaterMeter = Mapper.Map<WaterMeterUpdateModel, WaterMeterModel>(waterMeter);

			return await UpdateWaterMeterValues(targetWaterMeter, convertWaterMeter);
		}

		public async Task<Response> UpdateBySerialNumber(string serialNumber, WaterMeterUpdateModel waterMeter)
		{
			WaterMeterModel targetWaterMeter = await _dbContext.WaterMeters.FirstOrDefaultAsync(w => w.SerialNumber.Equals(serialNumber));

			if (targetWaterMeter is null) return new Response { Message = "Счетчик не найден." };

			WaterMeterModel convertWaterMeter = Mapper.Map<WaterMeterUpdateModel, WaterMeterModel>(waterMeter);

			return await UpdateWaterMeterValues(targetWaterMeter, convertWaterMeter);
		}

		public async Task<Response> UpdateByBuildingId(int buildingId, WaterMeterUpdateModel waterMeter)
		{
			WaterMeterModel targetWaterMeter = await _dbContext.Buildings
				.Include(b => b.WaterMeter)
				.Where(b => b.Id == buildingId)
				.Select(b => b.WaterMeter)
				.FirstOrDefaultAsync();

			if (targetWaterMeter is null) return new Response { Message = "Счетчик не найден." };

			WaterMeterModel convertWaterMeter = Mapper.Map<WaterMeterUpdateModel, WaterMeterModel>(waterMeter);

			return await UpdateWaterMeterValues(targetWaterMeter, convertWaterMeter);
		}

		private async Task<Response> UpdateWaterMeterValues(WaterMeterModel oldValue, WaterMeterModel newValue)
		{
			try
			{
				_dbContext.Entry(oldValue).CurrentValues.SetValues(newValue);

				await _dbContext.SaveChangesAsync();

				return new Response { Ok = true };
			}
			catch
			{
				return new Response { Message = "Произошла ошибка при обновлении. Пожалуйсте повторите попытку." };
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

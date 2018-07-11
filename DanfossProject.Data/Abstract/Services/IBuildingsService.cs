using DanfossProject.Data.Models;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanfossProject.Data.Abstract.Services
{
	public interface IBuildingsService : IDisposable
	{		
		Task<BuildingReturnModel> GetById(int id);
		Task<IEnumerable<BuildingReturnModel>> GetAll();
		Task<BuildingReturnModel> GetBuildingWithMinConsumption();
		Task<BuildingReturnModel> GetBuildingWithMaxConsumption();
		Task<Response> Add(BuildingCreateModel building);
		Task<Response> UpdateById(int id, BuildingUpdateModel building);
		Task<Response> Delete(int id);
	}
}

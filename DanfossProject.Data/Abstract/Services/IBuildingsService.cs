using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Abstract.Services
{
	public interface IBuildingsService
	{		
		Task<BuildingModel> GetById(int id);
		Task<IEnumerable<BuildingModel>> GetAll();
		Task<BuildingModel> GetBuildingWithMinConsumption();
		Task<BuildingModel> GetBuildingWithMaxConsumption();
		Task<bool> Add(BuildingModel building);
		Task<bool> UpdateById(int id, BuildingModel building);
		Task<bool> Delete(int id);
	}
}

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
		Task<Building> GetById(int id);
		Task<IEnumerable<Building>> GetAll();
		Task<Building> GetBuildingWithMinConsumption();
		Task<Building> GetBuildingWithMaxConsumption();
		Task<bool> Add(Building building);
		Task<bool> UpdateById(int id, Building building);
		Task<bool> Delete(int id);
	}
}

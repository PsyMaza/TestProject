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
		Task<Building> Get(int id);
		Task<IEnumerable<Building>> GetAll();
		Task<Building> GetBuildingWithMinConsumption();
		Task<Building> GetBuildingWithMaxConsumption();
		Task<bool> Add(Building building);
		Task<bool> Update(Building building);
		Task<bool> Delete(int id);
	}
}

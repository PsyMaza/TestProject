using DanfossProject.Data.Abstract.Services;
using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Concrete.Services
{
	class BuildingsService : IBuildingsService
	{
		public Task<bool> Add(Building building)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Building> Get(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Building>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Building> GetBuildingWithMaxConsumption()
		{
			throw new NotImplementedException();
		}

		public Task<Building> GetBuildingWithMinConsumption()
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(Building building)
		{
			throw new NotImplementedException();
		}
	}
}

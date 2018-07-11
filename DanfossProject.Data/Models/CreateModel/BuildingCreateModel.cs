using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject.Data.Models.CreateModel
{
	public class BuildingCreateModel
	{
		public int Id { get; set; }

		public AddressModel Address { get; set; }

		public int AddressHashCode { get; set; }

		public string Company { get; set; }

		public int? WaterMeterId { get; set; }
	}
}

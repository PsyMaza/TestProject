using DanfossProject.Data.Models.Entities;

namespace DanfossProject.Data.Models.UpdateModel
{
	public class BuildingUpdateModel
	{
		public int Id { get; set; }

		public AddressModel Address { get; set; }
		
		public int AddressHashCode { get; set; }

		public string Company { get; set; }

		public int? WaterMeterId { get; set; }
	}
}

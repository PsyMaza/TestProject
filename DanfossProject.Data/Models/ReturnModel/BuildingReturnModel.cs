using DanfossProject.Data.Models.Entities;

namespace DanfossProject.Data.Models.ReturnModel
{
	public class BuildingReturnModel
	{
		public int Id { get; set; }

		public AddressModel Address { get; set; }

		public string Company { get; set; }

		public WaterMeterModel WaterMeter { get; set; }
	}
}

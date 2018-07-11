using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanfossProject.Data.Models.Entities
{
	public class BuildingModel
    {
		[Key]
		public int Id { get; set; }
		
		public AddressModel Address { get; set; }

		[Required]
		[Index(IsUnique = true)]
		public int AddressHashCode { get; set; }

		public string Company { get; set; }		

		public int? WaterMeterId { get; set; }
		
		public WaterMeterModel WaterMeter { get; set; }
	}
}

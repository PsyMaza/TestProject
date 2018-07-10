using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DanfossProject.Data.Models.Entities
{
    public class BuildingModel
    {
		[Key]
		public int Id { get; set; }
		
		public AddressModel Address { get; set; }
		
		public string Company { get; set; }

		public virtual WaterMeterModel WaterMeter { get; set; }
	}
}

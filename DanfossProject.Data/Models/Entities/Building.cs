using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DanfossProject.Data.Models.Entities
{
    public class Building
    {
		[Key]
		public int Id { get; set; }
		
		public Address Address { get; set; }
		
		public string Company { get; set; }

		public WaterMeter WaterMeter { get; set; }
	}
}

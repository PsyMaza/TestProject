using DanfossProject.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DanfossProject.Data
{
	public class BaseDatabaseContext : DbContext
	{
		public BaseDatabaseContext()
			: base("DanfossDB")
		{}

		public DbSet<Building> Buildings { get; set; }

		public DbSet<WaterMeter> WaterMeters { get; set; }
		
	}
}

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
		{
			
		}

		public DbSet<BuildingModel> Buildings { get; set; }

		public DbSet<WaterMeterModel> WaterMeters { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WaterMeterModel>()
				.HasOptional(w => w.Building)
				.WithOptionalPrincipal(b => b.WaterMeter);

			base.OnModelCreating(modelBuilder);
		}
	}
}

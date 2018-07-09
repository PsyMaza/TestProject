namespace DanfossProject.Data.Migrations
{
	using DanfossProject.Data.Models.Entities;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DanfossProject.Data.BaseDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BaseDatabaseContext context)
        {
			using (BaseDatabaseContext db = new BaseDatabaseContext())
			{
				db.Buildings.Add(new Building
				{
					Address = new Address
					{
						Country = "Россия",
						City = "Лешково",
						Street = "",
						HouseNumber = "217",
						ZIPCode = 143581
					},
					Company = "ДАНФОСС"
				});

				db.Buildings.Add(new Building {
					Address = new Address
					{
						Country = "Россия",
						City = "Москва",
						Street = "Пятницкая",
						HouseNumber = "12/2",
						ZIPCode = 117997
					},
					Company = "Ингосстрах"
				});

				db.Buildings.Add(new Building
				{
					Address = new Address
					{
						Country = "Россия",
						City = "Москва",
						Street = "8-Марта",
						HouseNumber = "10/14",
						ZIPCode = 127083
					},
					Company = "ВымпелКом"
				});

				db.Buildings.Add(new Building
				{
					Address = new Address
					{
						Country = "Россия",
						City = "Москва",
						Street = "4-я Тверская-Ямская",
						HouseNumber = "2/11",
						ZIPCode = 100006
					}
				});

				

				db.Buildings.Add(new Building
				{
					Address = new Address
					{
						Country = "Россия",
						City = "Москва",
						Street = "Ленинградское шоссе",
						HouseNumber = "130/1",
						ZIPCode = 100043
					}
				});

				db.SaveChanges();
			}
        }
    }
}

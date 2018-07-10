namespace DanfossProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address_Country = c.String(nullable: false),
                        Address_ZIPCode = c.Int(nullable: false),
                        Address_City = c.String(nullable: false),
                        Address_Street = c.String(),
                        Address_HouseNumber = c.String(nullable: false),
                        Company = c.String(),
                        WaterMeter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WaterMeterModels", t => t.WaterMeter_Id)
                .Index(t => t.WaterMeter_Id);
            
            CreateTable(
                "dbo.WaterMeterModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(nullable: false, maxLength: 15),
                        CounterValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuildingModels", "WaterMeter_Id", "dbo.WaterMeterModels");
            DropIndex("dbo.BuildingModels", new[] { "WaterMeter_Id" });
            DropTable("dbo.WaterMeterModels");
            DropTable("dbo.BuildingModels");
        }
    }
}

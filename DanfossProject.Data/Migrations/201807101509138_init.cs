namespace DanfossProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address_Country = c.String(nullable: false),
                        Address_ZIPCode = c.Int(nullable: false),
                        Address_City = c.String(nullable: false),
                        Address_Street = c.String(),
                        Address_HouseNumber = c.String(nullable: false),
                        Company = c.String(),
                        WaterMeterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WaterMeters", t => t.WaterMeterId)
                .Index(t => t.WaterMeterId);
            
            CreateTable(
                "dbo.WaterMeters",
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
            DropForeignKey("dbo.Buildings", "WaterMeterId", "dbo.WaterMeters");
            DropIndex("dbo.Buildings", new[] { "WaterMeterId" });
            DropTable("dbo.WaterMeters");
            DropTable("dbo.Buildings");
        }
    }
}

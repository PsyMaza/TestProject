namespace DanfossProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddressHashCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuildingModels", "AddressHashCode", c => c.Int(nullable: false));
            CreateIndex("dbo.BuildingModels", "AddressHashCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.BuildingModels", new[] { "AddressHashCode" });
            DropColumn("dbo.BuildingModels", "AddressHashCode");
        }
    }
}

namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDBFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypeTechnologies", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.TypeTechnologies", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.TypeTechnologies", new[] { "Order_OrderId" });
            DropIndex("dbo.TypeTechnologies", new[] { "Service_ServiceId" });
            CreateIndex("dbo.Orders", "TypeId");
            CreateIndex("dbo.Services", "TypeId");
            AddForeignKey("dbo.Orders", "TypeId", "dbo.TypeTechnologies", "TypeId", cascadeDelete: true);
            AddForeignKey("dbo.Services", "TypeId", "dbo.TypeTechnologies", "TypeId", cascadeDelete: true);
            DropColumn("dbo.TypeTechnologies", "Order_OrderId");
            DropColumn("dbo.TypeTechnologies", "Service_ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypeTechnologies", "Service_ServiceId", c => c.Int());
            AddColumn("dbo.TypeTechnologies", "Order_OrderId", c => c.Int());
            DropForeignKey("dbo.Services", "TypeId", "dbo.TypeTechnologies");
            DropForeignKey("dbo.Orders", "TypeId", "dbo.TypeTechnologies");
            DropIndex("dbo.Services", new[] { "TypeId" });
            DropIndex("dbo.Orders", new[] { "TypeId" });
            CreateIndex("dbo.TypeTechnologies", "Service_ServiceId");
            CreateIndex("dbo.TypeTechnologies", "Order_OrderId");
            AddForeignKey("dbo.TypeTechnologies", "Service_ServiceId", "dbo.Services", "ServiceId");
            AddForeignKey("dbo.TypeTechnologies", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}

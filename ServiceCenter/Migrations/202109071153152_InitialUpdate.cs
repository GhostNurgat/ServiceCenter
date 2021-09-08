namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeTechnologies",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        Order_OrderId = c.Int(),
                        Service_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Services", t => t.Service_ServiceId)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Service_ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        TypeId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            AddColumn("dbo.Orders", "TypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Technology");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Technology", c => c.String(nullable: false));
            DropForeignKey("dbo.TypeTechnologies", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.TypeTechnologies", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.TypeTechnologies", new[] { "Service_ServiceId" });
            DropIndex("dbo.TypeTechnologies", new[] { "Order_OrderId" });
            DropColumn("dbo.Orders", "TypeId");
            DropTable("dbo.Services");
            DropTable("dbo.TypeTechnologies");
        }
    }
}

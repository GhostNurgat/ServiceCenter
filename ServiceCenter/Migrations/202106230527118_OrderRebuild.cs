namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderRebuild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.Orders", "ClientId");
            AddForeignKey("dbo.Orders", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "ClientId");
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "OrderId", "dbo.Clients", "ClientId");
        }
    }
}

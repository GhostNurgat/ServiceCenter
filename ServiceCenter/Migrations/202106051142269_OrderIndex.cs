namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "OrderId" });
            CreateIndex("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "OrderId" });
            CreateIndex("dbo.Orders", "OrderId");
        }
    }
}

namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 60),
                        Technology = c.String(nullable: false),
                        BrandName = c.String(nullable: false),
                        TechnologyName = c.String(nullable: false, maxLength: 65),
                        Phone = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        Service = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Guarantee = c.String(nullable: false, maxLength: 3),
                        StaffId = c.Int(nullable: false),
                        DateOrder = c.DateTime(nullable: false),
                        Done = c.Boolean(nullable: false),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.OrderId)
                .ForeignKey("dbo.StaffMembers", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        Position = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StaffId", "dbo.StaffMembers");
            DropForeignKey("dbo.Orders", "OrderId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "StaffId" });
            DropIndex("dbo.Orders", new[] { "OrderId" });
            DropTable("dbo.StaffMembers");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}

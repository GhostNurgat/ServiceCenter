namespace ServiceCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
            AddColumn("dbo.Orders", "Technology", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "BrandName", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "TechnologyName", c => c.String(nullable: false, maxLength: 65));
            AddColumn("dbo.StaffMembers", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.StaffMembers", "PositionId");
            AddForeignKey("dbo.StaffMembers", "PositionId", "dbo.Positions", "PositionId", cascadeDelete: true);
            DropColumn("dbo.Clients", "Technology");
            DropColumn("dbo.Clients", "BrandName");
            DropColumn("dbo.Clients", "TechnologyName");
            DropColumn("dbo.StaffMembers", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffMembers", "Position", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Clients", "TechnologyName", c => c.String(nullable: false, maxLength: 65));
            AddColumn("dbo.Clients", "BrandName", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Technology", c => c.String(nullable: false));
            DropForeignKey("dbo.StaffMembers", "PositionId", "dbo.Positions");
            DropIndex("dbo.StaffMembers", new[] { "PositionId" });
            DropColumn("dbo.StaffMembers", "PositionId");
            DropColumn("dbo.Orders", "TechnologyName");
            DropColumn("dbo.Orders", "BrandName");
            DropColumn("dbo.Orders", "Technology");
            DropTable("dbo.Positions");
        }
    }
}

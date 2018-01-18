namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                        Result = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        Order_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.ServiceId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Order_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ProcessResults", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ProcessResults", "ServiceId", "dbo.Services");
            DropIndex("dbo.Services", new[] { "Order_Id" });
            DropIndex("dbo.ProcessResults", new[] { "Order_Id" });
            DropIndex("dbo.ProcessResults", new[] { "ServiceId" });
            DropTable("dbo.Services");
            DropTable("dbo.ProcessResults");
            DropTable("dbo.Orders");
        }
    }
}

namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ProcessResults", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProcessResults", new[] { "Order_Id" });
            DropIndex("dbo.Services", new[] { "Order_Id" });
            RenameColumn(table: "dbo.ProcessResults", name: "Order_Id", newName: "OrderId");
            CreateTable(
                "dbo.OrdersServices",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ServiceId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ServiceId);
            
            AlterColumn("dbo.ProcessResults", "OrderId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProcessResults", "OrderId");
            AddForeignKey("dbo.ProcessResults", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.Services", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Order_Id", c => c.Guid());
            DropForeignKey("dbo.ProcessResults", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.OrdersServices", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrdersServices", new[] { "ServiceId" });
            DropIndex("dbo.OrdersServices", new[] { "OrderId" });
            DropIndex("dbo.ProcessResults", new[] { "OrderId" });
            AlterColumn("dbo.ProcessResults", "OrderId", c => c.Guid());
            DropTable("dbo.OrdersServices");
            RenameColumn(table: "dbo.ProcessResults", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.Services", "Order_Id");
            CreateIndex("dbo.ProcessResults", "Order_Id");
            AddForeignKey("dbo.ProcessResults", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Services", "Order_Id", "dbo.Orders", "Id");
        }
    }
}

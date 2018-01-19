namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "LastUpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "LastUpdateDate");
            DropColumn("dbo.Orders", "CreateDate");
        }
    }
}

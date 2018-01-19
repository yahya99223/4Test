namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OriginalText", c => c.String(maxLength: 150));
            AddColumn("dbo.Orders", "FinalResult", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FinalResult");
            DropColumn("dbo.Orders", "OriginalText");
        }
    }
}

namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Notifications", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Notifications");
        }
    }
}

namespace EF.Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId, name: "IX_Person_CompanyId")
                .Index(t => new { t.CompanyId, t.Email }, unique: true, name: "UQ_Person_Email")
                .Index(t => new { t.CompanyId, t.Name }, unique: true, name: "UQ_Person_Name")
                .Index(t => t.Name, name: "IX_Person_Name")
                .Index(t => t.Email, name: "IX_Person_Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "CompanyId", "dbo.Companies");
            DropIndex("dbo.People", "IX_Person_Email");
            DropIndex("dbo.People", "IX_Person_Name");
            DropIndex("dbo.People", "UQ_Person_Name");
            DropIndex("dbo.People", "UQ_Person_Email");
            DropIndex("dbo.People", "IX_Person_CompanyId");
            DropTable("dbo.People");
            DropTable("dbo.Companies");
        }
    }
}

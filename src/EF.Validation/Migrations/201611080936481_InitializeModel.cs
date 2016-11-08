namespace EF.Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeModel : DbMigration
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
                        ManagerId = c.Guid(),
                        Name = c.String(maxLength: 30),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.ManagerId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => new { t.CompanyId, t.Email }, unique: true, name: "UQ_Person_Email")
                .Index(t => new { t.CompanyId, t.Name }, unique: true, name: "UQ_Person_Name")
                .Index(t => t.ManagerId)
                .Index(t => t.Name, name: "IX_Person_Name")
                .Index(t => t.Email, name: "IX_Person_Email");
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        CountryId = c.Guid(nullable: false),
                        CityId = c.Guid(nullable: false),
                        PersonId = c.Guid(),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.CountryId, t.CityId })
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.Name, unique: true, name: "UQ_Address_Name");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Offices", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", "ManagerId", "dbo.People");
            DropIndex("dbo.Offices", "UQ_Address_Name");
            DropIndex("dbo.Offices", new[] { "PersonId" });
            DropIndex("dbo.People", "IX_Person_Email");
            DropIndex("dbo.People", "IX_Person_Name");
            DropIndex("dbo.People", new[] { "ManagerId" });
            DropIndex("dbo.People", "UQ_Person_Name");
            DropIndex("dbo.People", "UQ_Person_Email");
            DropTable("dbo.Offices");
            DropTable("dbo.People");
            DropTable("dbo.Companies");
        }
    }
}

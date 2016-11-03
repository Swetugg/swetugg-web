namespace Swetugg.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    // Dessa skapas genom att man kör det här Powershell-commandot (lämpligtvis via Tools->Nuget Package Manager->Package Manager Console)
    // Add-Migration {Migreringsnamn}
    // I det här fallet valde jag då InvoiceRequest som namn helt enkelt
    // För att sedan applicera den på sin databas kör man:
    // Update-Database
    public partial class InvoiceRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        InvoiceReciver = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 250),
                        CompanyName = c.String(nullable: false, maxLength: 250),
                        OrganisationNumber = c.String(nullable: false, maxLength: 13),
                        InvoiceAddress = c.String(nullable: false),
                        PostNumber = c.String(nullable: false, maxLength: 6),
                        PostCity = c.String(nullable: false, maxLength: 250),
                        InvoiceNote = c.String(maxLength: 250),
                        InvoiceEmail = c.String(nullable: false),
                        NumberOfTickets = c.Int(nullable: false),
                        Note = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvoiceRequests");
        }
    }
}

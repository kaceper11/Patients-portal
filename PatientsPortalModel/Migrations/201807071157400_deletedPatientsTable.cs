namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedPatientsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "UserId", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropTable("dbo.Patients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Pesel = c.String(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Patients", "UserId");
            AddForeignKey("dbo.Patients", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}

namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filesTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileContent = c.Binary(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            DropTable("dbo.Files");
        }
    }
}

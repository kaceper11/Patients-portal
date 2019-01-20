namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateExaminationsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Examinations", "PatientId", "dbo.Patients");
            DropIndex("dbo.Examinations", new[] { "PatientId" });
            AddColumn("dbo.Examinations", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Examinations", "UserId");
            AddForeignKey("dbo.Examinations", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
            DropColumn("dbo.Examinations", "PatientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Examinations", "PatientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Examinations", "UserId", "dbo.Users");
            DropIndex("dbo.Examinations", new[] { "UserId" });
            DropColumn("dbo.Examinations", "UserId");
            CreateIndex("dbo.Examinations", "PatientId");
            AddForeignKey("dbo.Examinations", "PatientId", "dbo.Patients", "Id", cascadeDelete: false);
        }
    }
}

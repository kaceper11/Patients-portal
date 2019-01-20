namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedFileModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationFiles", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExaminationFiles", "DoctorId");
            AddForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.ExaminationFiles", new[] { "DoctorId" });
            DropColumn("dbo.ExaminationFiles", "DoctorId");
        }
    }
}

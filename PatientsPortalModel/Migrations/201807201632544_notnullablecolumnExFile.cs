namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnullablecolumnExFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users");
            DropIndex("dbo.ExaminationFiles", new[] { "UserId" });
            DropIndex("dbo.ExaminationFiles", new[] { "DoctorId" });
            AlterColumn("dbo.ExaminationFiles", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExaminationFiles", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExaminationFiles", "UserId");
            CreateIndex("dbo.ExaminationFiles", "DoctorId");
            AddForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.ExaminationFiles", new[] { "DoctorId" });
            DropIndex("dbo.ExaminationFiles", new[] { "UserId" });
            AlterColumn("dbo.ExaminationFiles", "DoctorId", c => c.Int());
            AlterColumn("dbo.ExaminationFiles", "UserId", c => c.Int());
            CreateIndex("dbo.ExaminationFiles", "DoctorId");
            CreateIndex("dbo.ExaminationFiles", "UserId");
            AddForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors", "Id");
        }
    }
}

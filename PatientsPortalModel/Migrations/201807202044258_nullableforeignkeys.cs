namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableforeignkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Examinations", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Examinations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Problems", "UserId", "dbo.Users");
            DropForeignKey("dbo.WorkHours", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.ExaminationFiles", new[] { "UserId" });
            DropIndex("dbo.ExaminationFiles", new[] { "DoctorId" });
            DropIndex("dbo.Examinations", new[] { "UserId" });
            DropIndex("dbo.Examinations", new[] { "DoctorId" });
            DropIndex("dbo.Problems", new[] { "UserId" });
            DropIndex("dbo.WorkHours", new[] { "DoctorId" });
            AlterColumn("dbo.ExaminationFiles", "UserId", c => c.Int());
            AlterColumn("dbo.ExaminationFiles", "DoctorId", c => c.Int());
            AlterColumn("dbo.Examinations", "UserId", c => c.Int());
            AlterColumn("dbo.Examinations", "DoctorId", c => c.Int());
            AlterColumn("dbo.Problems", "UserId", c => c.Int());
            AlterColumn("dbo.WorkHours", "DoctorId", c => c.Int());
            CreateIndex("dbo.ExaminationFiles", "UserId");
            CreateIndex("dbo.ExaminationFiles", "DoctorId");
            CreateIndex("dbo.Examinations", "UserId");
            CreateIndex("dbo.Examinations", "DoctorId");
            CreateIndex("dbo.Problems", "UserId");
            CreateIndex("dbo.WorkHours", "DoctorId");
            AddForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors", "Id");
            AddForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Examinations", "DoctorId", "dbo.Doctors", "Id");
            AddForeignKey("dbo.Examinations", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Problems", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.WorkHours", "DoctorId", "dbo.Doctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkHours", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Problems", "UserId", "dbo.Users");
            DropForeignKey("dbo.Examinations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Examinations", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.WorkHours", new[] { "DoctorId" });
            DropIndex("dbo.Problems", new[] { "UserId" });
            DropIndex("dbo.Examinations", new[] { "DoctorId" });
            DropIndex("dbo.Examinations", new[] { "UserId" });
            DropIndex("dbo.ExaminationFiles", new[] { "DoctorId" });
            DropIndex("dbo.ExaminationFiles", new[] { "UserId" });
            AlterColumn("dbo.WorkHours", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Problems", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Examinations", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Examinations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExaminationFiles", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExaminationFiles", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkHours", "DoctorId");
            CreateIndex("dbo.Problems", "UserId");
            CreateIndex("dbo.Examinations", "DoctorId");
            CreateIndex("dbo.Examinations", "UserId");
            CreateIndex("dbo.ExaminationFiles", "DoctorId");
            CreateIndex("dbo.ExaminationFiles", "UserId");
            AddForeignKey("dbo.WorkHours", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Problems", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Examinations", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Examinations", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ExaminationFiles", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.ExaminationFiles", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: false);
        }
    }
}

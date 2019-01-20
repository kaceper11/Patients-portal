namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "FirstName", c => c.String());
            AlterColumn("dbo.Doctors", "LastName", c => c.String());
            AlterColumn("dbo.Doctors", "Specialization", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "MobileNumber", c => c.String());
            AlterColumn("dbo.Users", "Pesel", c => c.String());
            AlterColumn("dbo.Examinations", "ExaminationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Examinations", "ExaminationName", c => c.String());
            AlterColumn("dbo.Examinations", "Result", c => c.String());
            AlterColumn("dbo.Examinations", "Norm", c => c.String());
            AlterColumn("dbo.Examinations", "Description", c => c.String());
            AlterColumn("dbo.Units", "Value", c => c.String());
            AlterColumn("dbo.Problems", "Subject", c => c.String());
            AlterColumn("dbo.Problems", "ProblemDate", c => c.DateTime());
            AlterColumn("dbo.Problems", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Problems", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Problems", "ProblemDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Problems", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.Units", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.Examinations", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Examinations", "Norm", c => c.String(nullable: false));
            AlterColumn("dbo.Examinations", "Result", c => c.String(nullable: false));
            AlterColumn("dbo.Examinations", "ExaminationName", c => c.String(nullable: false));
            AlterColumn("dbo.Examinations", "ExaminationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "Pesel", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Specialization", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "FirstName", c => c.String(nullable: false));
        }
    }
}

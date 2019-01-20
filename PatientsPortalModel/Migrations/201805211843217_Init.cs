namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Specialization = c.String(nullable: false),
                        MondayStartTime = c.Int(nullable: false),
                        MondayEndTime = c.Int(nullable: false),
                        TuesdayStartTime = c.Int(nullable: false),
                        TuesdayEndTime = c.Int(nullable: false),
                        WednesdayStartTime = c.Int(nullable: false),
                        WednesdayEndTime = c.Int(nullable: false),
                        ThursdayStartTime = c.Int(nullable: false),
                        ThursdayEndTime = c.Int(nullable: false),
                        FridayStartTime = c.Int(nullable: false),
                        FridayEndTime = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(),
                        MobileNumber = c.String(nullable: false),
                        Pesel = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        ExaminationDate = c.DateTime(nullable: false),
                        ExaminationName = c.String(nullable: false),
                        Result = c.String(nullable: false),
                        UnitId = c.Int(nullable: false),
                        Norm = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: false)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: false)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.UnitId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        ProblemDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        DoctorId = c.Int(nullable: false),
                        Specialization = c.String(),
                        IsReserved = c.Boolean(nullable: false),
                        VisitDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        VisitTime = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "UserId", "dbo.Users");
            DropForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Problems", "UserId", "dbo.Users");
            DropForeignKey("dbo.Examinations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Examinations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "UserId", "dbo.Users");
            DropForeignKey("dbo.Examinations", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Visits", new[] { "DoctorId" });
            DropIndex("dbo.Visits", new[] { "UserId" });
            DropIndex("dbo.Problems", new[] { "UserId" });
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.Examinations", new[] { "UnitId" });
            DropIndex("dbo.Examinations", new[] { "DoctorId" });
            DropIndex("dbo.Examinations", new[] { "PatientId" });
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Visits");
            DropTable("dbo.Problems");
            DropTable("dbo.Units");
            DropTable("dbo.Patients");
            DropTable("dbo.Examinations");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Doctors");
        }
    }
}

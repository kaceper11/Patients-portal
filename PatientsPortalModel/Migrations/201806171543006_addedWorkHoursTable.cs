namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedWorkHoursTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        WorkDate = c.DateTime(nullable: false),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
            DropColumn("dbo.Doctors", "MondayStartTime");
            DropColumn("dbo.Doctors", "MondayEndTime");
            DropColumn("dbo.Doctors", "TuesdayStartTime");
            DropColumn("dbo.Doctors", "TuesdayEndTime");
            DropColumn("dbo.Doctors", "WednesdayStartTime");
            DropColumn("dbo.Doctors", "WednesdayEndTime");
            DropColumn("dbo.Doctors", "ThursdayStartTime");
            DropColumn("dbo.Doctors", "ThursdayEndTime");
            DropColumn("dbo.Doctors", "FridayStartTime");
            DropColumn("dbo.Doctors", "FridayEndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "FridayEndTime", c => c.Int());
            AddColumn("dbo.Doctors", "FridayStartTime", c => c.Int());
            AddColumn("dbo.Doctors", "ThursdayEndTime", c => c.Int());
            AddColumn("dbo.Doctors", "ThursdayStartTime", c => c.Int());
            AddColumn("dbo.Doctors", "WednesdayEndTime", c => c.Int());
            AddColumn("dbo.Doctors", "WednesdayStartTime", c => c.Int());
            AddColumn("dbo.Doctors", "TuesdayEndTime", c => c.Int());
            AddColumn("dbo.Doctors", "TuesdayStartTime", c => c.Int());
            AddColumn("dbo.Doctors", "MondayEndTime", c => c.Int());
            AddColumn("dbo.Doctors", "MondayStartTime", c => c.Int());
            DropForeignKey("dbo.WorkHours", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.WorkHours", new[] { "DoctorId" });
            DropTable("dbo.WorkHours");
        }
    }
}

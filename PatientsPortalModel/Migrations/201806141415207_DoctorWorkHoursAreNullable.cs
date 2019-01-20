namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorWorkHoursAreNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "MondayStartTime", c => c.Int());
            AlterColumn("dbo.Doctors", "MondayEndTime", c => c.Int());
            AlterColumn("dbo.Doctors", "TuesdayStartTime", c => c.Int());
            AlterColumn("dbo.Doctors", "TuesdayEndTime", c => c.Int());
            AlterColumn("dbo.Doctors", "WednesdayStartTime", c => c.Int());
            AlterColumn("dbo.Doctors", "WednesdayEndTime", c => c.Int());
            AlterColumn("dbo.Doctors", "ThursdayStartTime", c => c.Int());
            AlterColumn("dbo.Doctors", "ThursdayEndTime", c => c.Int());
            AlterColumn("dbo.Doctors", "FridayStartTime", c => c.Int());
            AlterColumn("dbo.Doctors", "FridayEndTime", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "FridayEndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "FridayStartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "ThursdayEndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "ThursdayStartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "WednesdayEndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "WednesdayStartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "TuesdayEndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "TuesdayStartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "MondayEndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "MondayStartTime", c => c.Int(nullable: false));
        }
    }
}

namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeDatesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.WorkHours", "WorkDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkHours", "WorkDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}

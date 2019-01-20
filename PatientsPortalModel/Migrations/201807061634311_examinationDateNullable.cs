namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examinationDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Examinations", "ExaminationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Examinations", "ExaminationDate", c => c.DateTime(nullable: false));
        }
    }
}

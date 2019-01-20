namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCOlumnCommnetToExaminationFilesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExaminationFiles", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExaminationFiles", "Comment");
        }
    }
}

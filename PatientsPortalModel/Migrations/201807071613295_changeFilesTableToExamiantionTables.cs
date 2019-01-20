namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFilesTableToExamiantionTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "ExaminationFiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ExaminationFiles", newName: "Files");
        }
    }
}

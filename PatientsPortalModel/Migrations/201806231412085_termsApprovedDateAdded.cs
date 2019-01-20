namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class termsApprovedDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TermsApprovedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TermsApprovedDate");
        }
    }
}

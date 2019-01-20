namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class termTableAddedUserModified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "TermsApproved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "ActivationCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ActivationCode", c => c.Guid(nullable: false));
            DropColumn("dbo.Users", "TermsApproved");
            DropTable("dbo.Terms");
        }
    }
}

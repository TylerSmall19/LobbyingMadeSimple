namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeStampsToIssues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "UpdatedAt");
            DropColumn("dbo.Issues", "CreatedAt");
        }
    }
}

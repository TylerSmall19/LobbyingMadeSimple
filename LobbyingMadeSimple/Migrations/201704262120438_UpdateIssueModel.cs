namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIssueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "VoteCountNeeded", c => c.Int(nullable: false));
            DropColumn("dbo.Issues", "IsApprovedForFunding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "IsApprovedForFunding", c => c.Boolean(nullable: false));
            DropColumn("dbo.Issues", "VoteCountNeeded");
        }
    }
}

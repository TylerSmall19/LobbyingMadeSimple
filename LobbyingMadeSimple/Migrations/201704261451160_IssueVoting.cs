namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IssueVoting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IsApprovedForFunding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Issues", "UpvoteCount", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "DownVoteCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "DownVoteCount");
            DropColumn("dbo.Issues", "UpvoteCount");
            DropColumn("dbo.Issues", "IsApprovedForFunding");
        }
    }
}

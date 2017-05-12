namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateIssueModelToRemoveSomeProperties : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Issues", "IsApproved");
            DropColumn("dbo.Issues", "UpvoteCount");
            DropColumn("dbo.Issues", "DownVoteCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "DownVoteCount", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "UpvoteCount", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "IsApproved", c => c.Boolean(nullable: false));
        }
    }
}

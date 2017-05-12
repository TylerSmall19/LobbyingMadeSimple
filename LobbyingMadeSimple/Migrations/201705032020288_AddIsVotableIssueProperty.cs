namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsVotableIssueProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IsVotableIssue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IsVotableIssue");
        }
    }
}

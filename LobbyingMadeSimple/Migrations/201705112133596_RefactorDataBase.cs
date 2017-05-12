namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RefactorDataBase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "IssueID", "dbo.Issues");
            DropForeignKey("dbo.Contributions", "IssueID", "dbo.Issues");
            DropPrimaryKey("dbo.Issues");
            DropColumn("dbo.Issues", "IssueID");
            AddColumn("dbo.Issues", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Issues", "Id");
            AddForeignKey("dbo.Votes", "IssueID", "dbo.Issues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contributions", "IssueID", "dbo.Issues", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contributions", "IssueID", "dbo.Issues");
            DropForeignKey("dbo.Votes", "IssueID", "dbo.Issues");
            DropPrimaryKey("dbo.Issues");
            DropColumn("dbo.Issues", "Id");
            AddColumn("dbo.Issues", "IssueID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Issues", "IssueID");
            AddForeignKey("dbo.Contributions", "IssueID", "dbo.Issues", "IssueID", cascadeDelete: true);
            AddForeignKey("dbo.Votes", "IssueID", "dbo.Issues", "IssueID", cascadeDelete: true);
        }
    }
}

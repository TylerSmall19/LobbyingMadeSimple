namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Vote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteID = c.Int(nullable: false, identity: true),
                        AuthorID = c.String(nullable: false, maxLength: 128),
                        IssueID = c.Int(nullable: false),
                        IsUpvote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VoteID)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.Issues", t => t.IssueID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.IssueID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "IssueID", "dbo.Issues");
            DropForeignKey("dbo.Votes", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Votes", new[] { "IssueID" });
            DropIndex("dbo.Votes", new[] { "AuthorID" });
            DropTable("dbo.Votes");
        }
    }
}

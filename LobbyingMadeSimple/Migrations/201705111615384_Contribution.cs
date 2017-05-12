namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Contribution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contributions",
                c => new
                    {
                        ContributionID = c.Int(nullable: false, identity: true),
                        IssueID = c.Int(nullable: false),
                        AuthorID = c.String(maxLength: 128),
                        Amount = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContributionID)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID)
                .ForeignKey("dbo.Issues", t => t.IssueID, cascadeDelete: true)
                .Index(t => t.IssueID)
                .Index(t => t.AuthorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contributions", "IssueID", "dbo.Issues");
            DropForeignKey("dbo.Contributions", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Contributions", new[] { "AuthorID" });
            DropIndex("dbo.Contributions", new[] { "IssueID" });
            DropTable("dbo.Contributions");
        }
    }
}

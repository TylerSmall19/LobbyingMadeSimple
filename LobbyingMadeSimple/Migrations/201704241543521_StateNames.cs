namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class StateNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        ShortDescription = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 60),
                        LongDescription = c.String(nullable: false),
                        IsFederalIssue = c.Boolean(nullable: false),
                        StateAbbrev = c.String(),
                        AuthorID = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "Author_Id" });
            DropTable("dbo.Issues");
        }
    }
}

namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "Author_Id" });
            AddColumn("dbo.AspNetUsers", "StateName", c => c.String());
            DropTable("dbo.Issues");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.IssueID);
            
            DropColumn("dbo.AspNetUsers", "StateName");
            CreateIndex("dbo.Issues", "Author_Id");
            AddForeignKey("dbo.Issues", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

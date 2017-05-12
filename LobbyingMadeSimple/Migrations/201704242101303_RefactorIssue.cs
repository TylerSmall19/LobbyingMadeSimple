namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RefactorIssue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "AuthorId" });
            AddColumn("dbo.Issues", "IsStateIssue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Issues", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Issues", "AuthorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "Author_Id");
            AddForeignKey("dbo.Issues", "Author_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Issues", "IsFederalIssue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "IsFederalIssue", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Issues", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "Author_Id" });
            AlterColumn("dbo.Issues", "AuthorID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Issues", "Author_Id");
            DropColumn("dbo.Issues", "IsStateIssue");
            CreateIndex("dbo.Issues", "AuthorId");
            AddForeignKey("dbo.Issues", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}

namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ContributionAnotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contributions", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Contributions", new[] { "AuthorID" });
            AlterColumn("dbo.Contributions", "AuthorID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Contributions", "AuthorID");
            AddForeignKey("dbo.Contributions", "AuthorID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contributions", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Contributions", new[] { "AuthorID" });
            AlterColumn("dbo.Contributions", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contributions", "AuthorID");
            AddForeignKey("dbo.Contributions", "AuthorID", "dbo.AspNetUsers", "Id");
        }
    }
}

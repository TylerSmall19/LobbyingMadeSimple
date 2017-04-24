namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorIdToString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Issues", new[] { "Author_Id" });
            DropColumn("dbo.Issues", "AuthorID");
            RenameColumn(table: "dbo.Issues", name: "Author_Id", newName: "AuthorID");
            AlterColumn("dbo.Issues", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "AuthorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Issues", new[] { "AuthorID" });
            AlterColumn("dbo.Issues", "AuthorID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Issues", name: "AuthorID", newName: "Author_Id");
            AddColumn("dbo.Issues", "AuthorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "Author_Id");
        }
    }
}

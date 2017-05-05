namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LetUpdatedAtBeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Issues", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Issues", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}

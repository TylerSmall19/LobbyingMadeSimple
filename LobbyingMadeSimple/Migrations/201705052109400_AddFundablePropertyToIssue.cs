namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFundablePropertyToIssue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IsFundable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IsFundable");
        }
    }
}

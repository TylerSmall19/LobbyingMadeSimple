namespace LobbyingMadeSimple.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddApprovalProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IsApproved");
        }
    }
}

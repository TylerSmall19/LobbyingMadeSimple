namespace LobbyingMadeSimple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFundingGoalsAndAmounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "FundingGoal", c => c.Double(nullable: false));
            AddColumn("dbo.Issues", "FundingRaised", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "FundingRaised");
            DropColumn("dbo.Issues", "FundingGoal");
        }
    }
}

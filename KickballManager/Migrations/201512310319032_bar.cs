namespace KickballManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bar : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lineups", name: "Team_TeamID", newName: "TeamID");
            RenameIndex(table: "dbo.Lineups", name: "IX_Team_TeamID", newName: "IX_TeamID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lineups", name: "IX_TeamID", newName: "IX_Team_TeamID");
            RenameColumn(table: "dbo.Lineups", name: "TeamID", newName: "Team_TeamID");
        }
    }
}

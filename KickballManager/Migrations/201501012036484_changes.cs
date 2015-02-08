namespace KickballManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Players", "TeamID");
            AddForeignKey("dbo.Players", "TeamID", "dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TeamID", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamID" });
        }
    }
}

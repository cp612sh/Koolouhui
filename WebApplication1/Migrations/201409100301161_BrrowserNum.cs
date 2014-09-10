namespace Koo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrrowserNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "BrowseNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "BrowseNum");
        }
    }
}

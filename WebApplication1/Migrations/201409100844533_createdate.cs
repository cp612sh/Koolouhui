namespace Koo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "CreateDate");
        }
    }
}

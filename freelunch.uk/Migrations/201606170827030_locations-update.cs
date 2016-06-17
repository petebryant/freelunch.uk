namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationsupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String());
        }
    }
}

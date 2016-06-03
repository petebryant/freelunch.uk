namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredtext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "Text", c => c.String());
        }
    }
}

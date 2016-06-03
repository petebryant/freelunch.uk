namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredfileds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "URL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "URL", c => c.String());
        }
    }
}

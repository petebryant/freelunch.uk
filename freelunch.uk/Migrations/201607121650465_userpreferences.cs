namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpreferences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ReceiveMFLMailing = c.Boolean(nullable: false),
                        ReceivePartnerMailing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPreferences");
        }
    }
}

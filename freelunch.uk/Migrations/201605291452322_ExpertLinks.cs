namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        URL = c.String(),
                        LinkType = c.Int(nullable: false),
                        Expert_ExpertId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Experts", t => t.Expert_ExpertId)
                .Index(t => t.Expert_ExpertId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "Expert_ExpertId", "dbo.Experts");
            DropIndex("dbo.Links", new[] { "Expert_ExpertId" });
            DropTable("dbo.Links");
        }
    }
}

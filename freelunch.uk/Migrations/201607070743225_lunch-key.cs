namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lunchkey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lunches",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Website = c.String(),
                        Image = c.String(),
                        ContactName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Audience = c.String(),
                        AudienceNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Lunch_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lunches", t => t.Lunch_Id)
                .Index(t => t.Lunch_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "Lunch_Id", "dbo.Lunches");
            DropIndex("dbo.Topics", new[] { "Lunch_Id" });
            DropTable("dbo.Topics");
            DropTable("dbo.Lunches");
        }
    }
}

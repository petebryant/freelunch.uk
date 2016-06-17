namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Specialist_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialists", t => t.Specialist_UserId)
                .Index(t => t.Specialist_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "Specialist_UserId", "dbo.Specialists");
            DropIndex("dbo.Locations", new[] { "Specialist_UserId" });
            DropTable("dbo.Locations");
        }
    }
}

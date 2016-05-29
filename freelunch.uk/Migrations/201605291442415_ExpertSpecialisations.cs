namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertSpecialisations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialisations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(),
                        Description = c.String(),
                        Expert_ExpertId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Experts", t => t.Expert_ExpertId)
                .Index(t => t.Expert_ExpertId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specialisations", "Expert_ExpertId", "dbo.Experts");
            DropIndex("dbo.Specialisations", new[] { "Expert_ExpertId" });
            DropTable("dbo.Specialisations");
        }
    }
}

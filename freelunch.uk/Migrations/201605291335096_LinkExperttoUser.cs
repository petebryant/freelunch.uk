namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkExperttoUser : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Experts");
            AddColumn("dbo.Experts", "ExpertId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Experts", "ExpertId");
            CreateIndex("dbo.Experts", "ExpertId");
            AddForeignKey("dbo.Experts", "ExpertId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Experts", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Experts", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Experts", "ExpertId", "dbo.AspNetUsers");
            DropIndex("dbo.Experts", new[] { "ExpertId" });
            DropPrimaryKey("dbo.Experts");
            DropColumn("dbo.Experts", "ExpertId");
            AddPrimaryKey("dbo.Experts", "Id");
        }
    }
}

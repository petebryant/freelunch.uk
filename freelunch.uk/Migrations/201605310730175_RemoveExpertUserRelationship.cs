namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExpertUserRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Experts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Experts", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Experts", "UserId");
            AddForeignKey("dbo.Experts", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

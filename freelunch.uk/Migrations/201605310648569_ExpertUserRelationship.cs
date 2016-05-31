namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertUserRelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Links", name: "Expert_ExpertId", newName: "Expert_UserId");
            RenameColumn(table: "dbo.Specialisations", name: "Expert_ExpertId", newName: "Expert_UserId");
            RenameColumn(table: "dbo.Experts", name: "ExpertId", newName: "UserId");
            RenameIndex(table: "dbo.Experts", name: "IX_ExpertId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Links", name: "IX_Expert_ExpertId", newName: "IX_Expert_UserId");
            RenameIndex(table: "dbo.Specialisations", name: "IX_Expert_ExpertId", newName: "IX_Expert_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Specialisations", name: "IX_Expert_UserId", newName: "IX_Expert_ExpertId");
            RenameIndex(table: "dbo.Links", name: "IX_Expert_UserId", newName: "IX_Expert_ExpertId");
            RenameIndex(table: "dbo.Experts", name: "IX_UserId", newName: "IX_ExpertId");
            RenameColumn(table: "dbo.Experts", name: "UserId", newName: "ExpertId");
            RenameColumn(table: "dbo.Specialisations", name: "Expert_UserId", newName: "Expert_ExpertId");
            RenameColumn(table: "dbo.Links", name: "Expert_UserId", newName: "Expert_ExpertId");
        }
    }
}

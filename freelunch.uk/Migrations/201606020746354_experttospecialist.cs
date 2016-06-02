namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class experttospecialist : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Experts", newName: "Specialists");
            RenameColumn(table: "dbo.Links", name: "Expert_UserId", newName: "Specialist_UserId");
            RenameColumn(table: "dbo.Specialisations", name: "Expert_UserId", newName: "Specialist_UserId");
            RenameIndex(table: "dbo.Links", name: "IX_Expert_UserId", newName: "IX_Specialist_UserId");
            RenameIndex(table: "dbo.Specialisations", name: "IX_Expert_UserId", newName: "IX_Specialist_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Specialisations", name: "IX_Specialist_UserId", newName: "IX_Expert_UserId");
            RenameIndex(table: "dbo.Links", name: "IX_Specialist_UserId", newName: "IX_Expert_UserId");
            RenameColumn(table: "dbo.Specialisations", name: "Specialist_UserId", newName: "Expert_UserId");
            RenameColumn(table: "dbo.Links", name: "Specialist_UserId", newName: "Expert_UserId");
            RenameTable(name: "dbo.Specialists", newName: "Experts");
        }
    }
}

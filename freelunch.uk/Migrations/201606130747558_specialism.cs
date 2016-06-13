namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specialism : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Specialisations", newName: "Specialism");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Specialism", newName: "Specialisations");
        }
    }
}

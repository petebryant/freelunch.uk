namespace freelunch.uk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userlunch : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "Lunch_Id", "dbo.Lunches");
            RenameColumn(table: "dbo.Topics", name: "Lunch_Id", newName: "Lunch_UserId");
            RenameIndex(table: "dbo.Topics", name: "IX_Lunch_Id", newName: "IX_Lunch_UserId");
            DropPrimaryKey("dbo.Lunches");
            AddColumn("dbo.Lunches", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Lunches", "UserId");
            AddForeignKey("dbo.Topics", "Lunch_UserId", "dbo.Lunches", "UserId");
            DropColumn("dbo.Lunches", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lunches", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Topics", "Lunch_UserId", "dbo.Lunches");
            DropPrimaryKey("dbo.Lunches");
            DropColumn("dbo.Lunches", "UserId");
            AddPrimaryKey("dbo.Lunches", "Id");
            RenameIndex(table: "dbo.Topics", name: "IX_Lunch_UserId", newName: "IX_Lunch_Id");
            RenameColumn(table: "dbo.Topics", name: "Lunch_UserId", newName: "Lunch_Id");
            AddForeignKey("dbo.Topics", "Lunch_Id", "dbo.Lunches", "Id");
        }
    }
}

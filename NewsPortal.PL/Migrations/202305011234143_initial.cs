namespace NewsPortal.PL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateEnter", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateExit", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateExit");
            DropColumn("dbo.AspNetUsers", "DateEnter");
        }
    }
}

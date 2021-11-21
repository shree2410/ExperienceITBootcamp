namespace ExperienceITBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategrade : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Grade", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Grade", c => c.String());
        }
    }
}

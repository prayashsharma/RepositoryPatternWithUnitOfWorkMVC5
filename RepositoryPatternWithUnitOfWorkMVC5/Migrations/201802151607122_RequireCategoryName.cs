namespace RepositoryPatternWithUnitOfWorkMVC5.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RequireCategoryName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Category", "Name", c => c.String());
        }
    }
}
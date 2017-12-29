namespace soby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "CategoryId" });
            DropTable("dbo.Categories");
        }
    }
}

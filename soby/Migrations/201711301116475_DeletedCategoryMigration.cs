namespace soby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedCategoryMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "CategoryId" });
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Categories", "CategoryId", "dbo.Categories", "Id");
        }
    }
}

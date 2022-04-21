namespace recrutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobContent = c.String(),
                        JobImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobModels", "CategoryId", "dbo.CategoryModels");
            DropIndex("dbo.JobModels", new[] { "CategoryId" });
            DropTable("dbo.JobModels");
        }
    }
}

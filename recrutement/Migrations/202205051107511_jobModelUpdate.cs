namespace recrutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobModels", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.JobModels", "UserId");
            AddForeignKey("dbo.JobModels", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.JobModels", new[] { "UserId" });
            DropColumn("dbo.JobModels", "UserId");
        }
    }
}

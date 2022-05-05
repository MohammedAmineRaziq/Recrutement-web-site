namespace recrutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobApplicationModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobApplicationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        CV = c.String(),
                        JobId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobModels", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JobId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplicationModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplicationModels", "JobId", "dbo.JobModels");
            DropIndex("dbo.JobApplicationModels", new[] { "UserId" });
            DropIndex("dbo.JobApplicationModels", new[] { "JobId" });
            DropTable("dbo.JobApplicationModels");
        }
    }
}

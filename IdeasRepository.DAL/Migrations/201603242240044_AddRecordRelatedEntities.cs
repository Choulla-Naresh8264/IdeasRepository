namespace IdeasRepository.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecordRelatedEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Author = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        TextBody = c.String(),
                        RecordTypeId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecordTypes", t => t.RecordTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.RecordTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RecordTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Records", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Records", "RecordTypeId", "dbo.RecordTypes");
            DropIndex("dbo.Records", new[] { "UserId" });
            DropIndex("dbo.Records", new[] { "RecordTypeId" });
            DropTable("dbo.RecordTypes");
            DropTable("dbo.Records");
        }
    }
}

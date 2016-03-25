namespace IdeasRepository.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecordDeletedState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "IsDeleted");
        }
    }
}

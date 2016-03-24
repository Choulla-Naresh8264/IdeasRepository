namespace IdeasRepository.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
    }
}

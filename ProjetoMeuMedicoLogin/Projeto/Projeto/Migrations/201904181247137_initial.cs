namespace Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Senha", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "Senha", c => c.String());
            AlterColumn("dbo.Cliente", "Email", c => c.String());
        }
    }
}

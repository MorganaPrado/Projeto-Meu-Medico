namespace Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.CidadeId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadeId);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        MedicoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                        EspecialidadeId = c.Int(nullable: false),
                        CidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicoId)
                .ForeignKey("dbo.Cidade", t => t.CidadeId, cascadeDelete: true)
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeId, cascadeDelete: true)
                .Index(t => t.EspecialidadeId)
                .Index(t => t.CidadeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medico", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.Medico", "CidadeId", "dbo.Cidade");
            DropIndex("dbo.Medico", new[] { "CidadeId" });
            DropIndex("dbo.Medico", new[] { "EspecialidadeId" });
            DropTable("dbo.Medico");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Cliente");
            DropTable("dbo.Cidade");
        }
    }
}

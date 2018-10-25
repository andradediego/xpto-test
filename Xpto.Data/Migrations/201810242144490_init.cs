namespace Xpto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        CodReferencia = c.Int(nullable: false),
                        Nome = c.String(maxLength: 250),
                        Sobrenome = c.String(maxLength: 250),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 250),
                        Sexo = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        CodReferencia = c.Int(nullable: false),
                        Nome = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            CreateTable(
                "dbo.ProdutosClientes",
                c => new
                    {
                        Produtos_ProdutoId = c.Int(nullable: false),
                        Clientes_ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Produtos_ProdutoId, t.Clientes_ClienteId })
                .ForeignKey("dbo.Produtos", t => t.Produtos_ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.Clientes_ClienteId, cascadeDelete: true)
                .Index(t => t.Produtos_ProdutoId)
                .Index(t => t.Clientes_ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutosClientes", "Clientes_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.ProdutosClientes", "Produtos_ProdutoId", "dbo.Produtos");
            DropIndex("dbo.ProdutosClientes", new[] { "Clientes_ClienteId" });
            DropIndex("dbo.ProdutosClientes", new[] { "Produtos_ProdutoId" });
            DropTable("dbo.ProdutosClientes");
            DropTable("dbo.Produtos");
            DropTable("dbo.Clientes");
        }
    }
}

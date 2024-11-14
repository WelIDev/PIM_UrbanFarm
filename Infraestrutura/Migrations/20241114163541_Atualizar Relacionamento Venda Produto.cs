using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AtualizarRelacionamentoVendaProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remover a tabela VendaProduto se já existir (se não estiver corretamente configurada)
            migrationBuilder.DropTable(
                name: "VendaProduto");

            // Recriar a tabela VendaProduto com as chaves estrangeiras corretas
            migrationBuilder.CreateTable(
                name: "VendaProduto",
                columns: table => new
                {
                    IdVenda = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaProduto", x => new { x.IdVenda, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_VendaProduto_Vendas_IdVenda",
                        column: x => x.IdVenda,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendaProduto_Produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Criar índices para as chaves estrangeiras
            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_IdVenda",
                table: "VendaProduto",
                column: "IdVenda");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_IdProduto",
                table: "VendaProduto",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverter as alterações feitas na Up
            migrationBuilder.DropTable(name: "VendaProduto");
        }
    }
}

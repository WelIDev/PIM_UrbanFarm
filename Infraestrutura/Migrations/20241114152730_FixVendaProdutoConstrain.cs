using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class FixVendaProdutoConstrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remover as chaves estrangeiras existentes corretamente
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Produtos_ProdutoId",
                table: "VendaProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Vendas_VendaId",
                table: "VendaProduto");

            // Remover a chave primária anterior
            migrationBuilder.DropPrimaryKey(
                name: "PK_VendaProduto",
                table: "VendaProduto");

            // Renomear as colunas corretamente (verifique se elas existem primeiro)
            migrationBuilder.RenameColumn(
                name: "VendaId", // Coluna original
                table: "VendaProduto",
                newName: "IdVenda"); // Novo nome da coluna

            migrationBuilder.RenameColumn(
                name: "ProdutoId", // Coluna original
                table: "VendaProduto",
                newName: "IdProduto"); // Novo nome da coluna

            // Atualizar a coluna de Quantidade para não ter valor padrão
            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "VendaProduto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            // Adicionar a coluna ValorTotal com valor padrão
            migrationBuilder.AddColumn<double>(
                name: "ValorTotal",
                table: "VendaProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            // Adicionar a chave primária composta
            migrationBuilder.AddPrimaryKey(
                name: "PK_VendaProduto",
                table: "VendaProduto",
                columns: new[]
                {
                    "IdVenda", "IdProduto"
                }); // Chave primária composta com as novas colunas renomeadas

            // Criar índice de VendaId (Agora deve funcionar, pois a coluna foi renomeada corretamente)
            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_VendaId",
                table: "VendaProduto",
                column: "IdVenda"); // Índice usando o nome correto da coluna

            // Recriar as chaves estrangeiras corretamente
            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_ProdutoId",
                table: "VendaProduto",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_VendaId",
                table: "VendaProduto",
                column: "IdVenda",
                principalTable: "Vendas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverter as mudanças caso a migração precise ser revertida
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_ProdutoId",
                table: "VendaProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_VendaId",
                table: "VendaProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendaProduto",
                table: "VendaProduto");

            migrationBuilder.DropIndex(
                name: "IX_VendaProduto_VendaId",
                table: "VendaProduto");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "VendaProduto");

            // Restaurar os nomes das colunas
            migrationBuilder.RenameColumn(
                name: "IdVenda",
                table: "VendaProduto",
                newName: "VendaId");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "VendaProduto",
                newName: "ProdutoId");

            // Restaurar a chave primária original
            migrationBuilder.AddPrimaryKey(
                name: "PK_VendaProduto",
                table: "VendaProduto",
                columns: new[] { "VendaId", "ProdutoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Produtos_ProdutoId",
                table: "VendaProduto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Vendas_VendaId",
                table: "VendaProduto",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id");
        }
    }
}

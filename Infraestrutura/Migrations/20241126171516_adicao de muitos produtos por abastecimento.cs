using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class adicaodemuitosprodutosporabastecimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbastecimentosEstoque_Produtos_ProdutoId",
                table: "AbastecimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_AbastecimentosEstoque_ProdutoId",
                table: "AbastecimentosEstoque");

            migrationBuilder.DropColumn(
                name: "Custo",
                table: "AbastecimentosEstoque");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "AbastecimentosEstoque");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "AbastecimentosEstoque");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "VendaProduto",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "ItensAbastecimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbastecimentoEstoqueId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Custo = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensAbastecimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensAbastecimento_AbastecimentosEstoque_AbastecimentoEstoqueId",
                        column: x => x.AbastecimentoEstoqueId,
                        principalTable: "AbastecimentosEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensAbastecimento_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensAbastecimento_AbastecimentoEstoqueId",
                table: "ItensAbastecimento",
                column: "AbastecimentoEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensAbastecimento_ProdutoId",
                table: "ItensAbastecimento",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensAbastecimento");

            migrationBuilder.AlterColumn<double>(
                name: "ValorTotal",
                table: "VendaProduto",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Produtos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<double>(
                name: "Custo",
                table: "AbastecimentosEstoque",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "AbastecimentosEstoque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "AbastecimentosEstoque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AbastecimentosEstoque_ProdutoId",
                table: "AbastecimentosEstoque",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbastecimentosEstoque_Produtos_ProdutoId",
                table: "AbastecimentosEstoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

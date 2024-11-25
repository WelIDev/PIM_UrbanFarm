using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class colocandocascataemvenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Vendas_IdVenda",
                table: "VendaProduto");
            
            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Vendas_IdVenda",
                table: "VendaProduto",
                column: "IdVenda",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Vendas_IdVenda",
                table: "VendaProduto");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Vendas");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Vendas_IdVenda",
                table: "VendaProduto",
                column: "IdVenda",
                principalTable: "Vendas",
                principalColumn: "Id");
        }
    }
}

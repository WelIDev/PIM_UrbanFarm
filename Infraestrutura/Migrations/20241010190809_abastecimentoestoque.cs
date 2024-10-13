using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class abastecimentoestoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoCompra_Clientes_ClienteId",
                table: "HistoricoCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_HistoricoCompra_HistoricoCompraId",
                table: "Vendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoCompra",
                table: "HistoricoCompra");

            migrationBuilder.RenameTable(
                name: "HistoricoCompra",
                newName: "HistoricoCompras");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoCompra_ClienteId",
                table: "HistoricoCompras",
                newName: "IX_HistoricoCompras_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoCompras",
                table: "HistoricoCompras",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AbastecimentosEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FornecedorId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbastecimentosEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbastecimentosEstoque_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbastecimentosEstoque_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbastecimentosEstoque_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbastecimentosEstoque_FornecedorId",
                table: "AbastecimentosEstoque",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_AbastecimentosEstoque_ProdutoId",
                table: "AbastecimentosEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_AbastecimentosEstoque_UsuarioId",
                table: "AbastecimentosEstoque",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoCompras_Clientes_ClienteId",
                table: "HistoricoCompras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_HistoricoCompras_HistoricoCompraId",
                table: "Vendas",
                column: "HistoricoCompraId",
                principalTable: "HistoricoCompras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoCompras_Clientes_ClienteId",
                table: "HistoricoCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_HistoricoCompras_HistoricoCompraId",
                table: "Vendas");

            migrationBuilder.DropTable(
                name: "AbastecimentosEstoque");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoCompras",
                table: "HistoricoCompras");

            migrationBuilder.RenameTable(
                name: "HistoricoCompras",
                newName: "HistoricoCompra");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoCompras_ClienteId",
                table: "HistoricoCompra",
                newName: "IX_HistoricoCompra_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoCompra",
                table: "HistoricoCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoCompra_Clientes_ClienteId",
                table: "HistoricoCompra",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_HistoricoCompra_HistoricoCompraId",
                table: "Vendas",
                column: "HistoricoCompraId",
                principalTable: "HistoricoCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

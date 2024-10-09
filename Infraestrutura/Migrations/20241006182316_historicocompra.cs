using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class historicocompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Vendas",
                newName: "HistoricoCompraId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                newName: "IX_Vendas_HistoricoCompraId");

            migrationBuilder.CreateTable(
                name: "HistoricoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoCompra_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoCompra_ClienteId",
                table: "HistoricoCompra",
                column: "ClienteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_HistoricoCompra_HistoricoCompraId",
                table: "Vendas",
                column: "HistoricoCompraId",
                principalTable: "HistoricoCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_HistoricoCompra_HistoricoCompraId",
                table: "Vendas");

            migrationBuilder.DropTable(
                name: "HistoricoCompra");

            migrationBuilder.RenameColumn(
                name: "HistoricoCompraId",
                table: "Vendas",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_HistoricoCompraId",
                table: "Vendas",
                newName: "IX_Vendas_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

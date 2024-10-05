using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comissoes_Vendedores_VendedorId",
                table: "Comissoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Metas_MetaId",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_EnderecoId",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_MetaId",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Comissoes_VendedorId",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "MetaId",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Comissoes");

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Metas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_EnderecoId",
                table: "Vendedores",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metas_VendedorId",
                table: "Metas",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metas_Vendedores_VendedorId",
                table: "Metas",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metas_Vendedores_VendedorId",
                table: "Metas");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_EnderecoId",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Metas_VendedorId",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Metas");

            migrationBuilder.AddColumn<int>(
                name: "MetaId",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Comissoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_EnderecoId",
                table: "Vendedores",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_MetaId",
                table: "Vendedores",
                column: "MetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissoes_VendedorId",
                table: "Comissoes",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comissoes_Vendedores_VendedorId",
                table: "Comissoes",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Metas_MetaId",
                table: "Vendedores",
                column: "MetaId",
                principalTable: "Metas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

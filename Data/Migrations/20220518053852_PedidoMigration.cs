using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Inkasign.Data.Migrations
{
    public partial class PedidoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    PagoId = table.Column<int>(type: "integer", nullable: false),
                    archivo = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_pedido_t_pago_PagoId",
                        column: x => x.PagoId,
                        principalTable: "t_pago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_detalle_pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_detalle_pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_detalle_pedido_t_pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "t_pedido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_detalle_pedido_t_producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "t_producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_detalle_pedido_PedidoId",
                table: "t_detalle_pedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_detalle_pedido_ProductoId",
                table: "t_detalle_pedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_pedido_PagoId",
                table: "t_pedido",
                column: "PagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_detalle_pedido");

            migrationBuilder.DropTable(
                name: "t_pedido");
        }
    }
}

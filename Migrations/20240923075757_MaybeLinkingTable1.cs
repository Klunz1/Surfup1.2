using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfsupEmil.Migrations
{
    /// <inheritdoc />
    public partial class MaybeLinkingTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surfboards_Orders_OrderId",
                table: "Surfboards");

            migrationBuilder.DropIndex(
                name: "IX_Surfboards_OrderId",
                table: "Surfboards");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Surfboards");

            migrationBuilder.CreateTable(
                name: "OrderSurfboard",
                columns: table => new
                {
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    SurfboardsSurfboardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSurfboard", x => new { x.OrdersOrderId, x.SurfboardsSurfboardId });
                    table.ForeignKey(
                        name: "FK_OrderSurfboard_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSurfboard_Surfboards_SurfboardsSurfboardId",
                        column: x => x.SurfboardsSurfboardId,
                        principalTable: "Surfboards",
                        principalColumn: "SurfboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSurfboard_SurfboardsSurfboardId",
                table: "OrderSurfboard",
                column: "SurfboardsSurfboardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSurfboard");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Surfboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surfboards_OrderId",
                table: "Surfboards",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surfboards_Orders_OrderId",
                table: "Surfboards",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}

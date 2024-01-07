using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class TakeAwayListOfProduct_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakeAwayProduct_Order_OrderId",
                table: "TakeAwayProduct");

            migrationBuilder.DropIndex(
                name: "IX_TakeAwayProduct_OrderId",
                table: "TakeAwayProduct");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "TakeAwayProduct");

            migrationBuilder.CreateTable(
                name: "OrderTakeAwayProduct",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTakeAwayProduct", x => new { x.OrderId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderTakeAwayProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTakeAwayProduct_TakeAwayProduct_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "TakeAwayProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTakeAwayProduct_ProductsId",
                table: "OrderTakeAwayProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTakeAwayProduct");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "TakeAwayProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TakeAwayProduct_OrderId",
                table: "TakeAwayProduct",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakeAwayProduct_Order_OrderId",
                table: "TakeAwayProduct",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

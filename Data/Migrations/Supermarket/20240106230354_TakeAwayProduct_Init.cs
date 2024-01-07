using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class TakeAwayProduct_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

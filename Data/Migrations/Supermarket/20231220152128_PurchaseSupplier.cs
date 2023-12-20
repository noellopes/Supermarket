using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class PurchaseSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_PurchaseSupplier_PurchaseSupplierId",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_PurchaseSupplierId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "PurchaseSupplierId",
                table: "Supplier");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "PurchaseSupplier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseSupplier_SupplierId",
                table: "PurchaseSupplier",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseSupplier_Supplier_SupplierId",
                table: "PurchaseSupplier",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseSupplier_Supplier_SupplierId",
                table: "PurchaseSupplier");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseSupplier_SupplierId",
                table: "PurchaseSupplier");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "PurchaseSupplier");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseSupplierId",
                table: "Supplier",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_PurchaseSupplierId",
                table: "Supplier",
                column: "PurchaseSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_PurchaseSupplier_PurchaseSupplierId",
                table: "Supplier",
                column: "PurchaseSupplierId",
                principalTable: "PurchaseSupplier",
                principalColumn: "PurchaseSupplierId");
        }
    }
}

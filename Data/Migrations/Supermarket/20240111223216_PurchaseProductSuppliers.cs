using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class PurchaseProductSuppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "PurchaseProductSupplier",
                columns: table => new
                {
                    PurchaseProductSupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseSupplierId = table.Column<int>(type: "int", nullable: false),
                    AskedQuantity = table.Column<int>(type: "int", nullable: false),
                    DeliveredQuantity = table.Column<int>(type: "int", nullable: false),
                    EstimateDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProductSupplier", x => x.PurchaseProductSupplierId);
                    table.ForeignKey(
                        name: "FK_PurchaseProductSupplier_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProductSupplier_PurchaseSupplier_PurchaseSupplierId",
                        column: x => x.PurchaseSupplierId,
                        principalTable: "PurchaseSupplier",
                        principalColumn: "PurchaseSupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductSupplier_ProductId",
                table: "PurchaseProductSupplier",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductSupplier_PurchaseSupplierId",
                table: "PurchaseProductSupplier",
                column: "PurchaseSupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "PurchaseProductSupplier",
                columns: table => new
                {
                    PurchaseProductSupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseSupplierId = table.Column<int>(type: "int", nullable: false),
                    AskedQuantity = table.Column<int>(type: "int", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredQuantity = table.Column<int>(type: "int", nullable: false),
                    EstimateDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProductSupplier", x => x.PurchaseProductSupplierId);
                    table.ForeignKey(
                        name: "FK_PurchaseProductSupplier_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProductSupplier_PurchaseSupplier_PurchaseSupplierId",
                        column: x => x.PurchaseSupplierId,
                        principalTable: "PurchaseSupplier",
                        principalColumn: "PurchaseSupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductSupplier_ProductId",
                table: "PurchaseProductSupplier",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductSupplier_PurchaseSupplierId",
                table: "PurchaseProductSupplier",
                column: "PurchaseSupplierId");
        }
    }
}

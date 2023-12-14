using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_EmployeeId",
                table: "Issues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProductId",
                table: "Issues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SupplierId",
                table: "Issues",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Funcionarios_EmployeeId",
                table: "Issues",
                column: "EmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Product_ProductId",
                table: "Issues",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Supplier_SupplierId",
                table: "Issues",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Funcionarios_EmployeeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Product_ProductId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Supplier_SupplierId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Issues_EmployeeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ProductId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_SupplierId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Issues");
        }
    }
}

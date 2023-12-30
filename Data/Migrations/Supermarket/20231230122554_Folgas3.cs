using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class Folgas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folga_Employee_FuncionarioId",
                table: "Folga");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "Folga",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Folga_FuncionarioId",
                table: "Folga",
                newName: "IX_Folga_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folga_Employee_EmployeeId",
                table: "Folga",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folga_Employee_EmployeeId",
                table: "Folga");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Folga",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Folga_EmployeeId",
                table: "Folga",
                newName: "IX_Folga_FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folga_Employee_FuncionarioId",
                table: "Folga",
                column: "FuncionarioId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

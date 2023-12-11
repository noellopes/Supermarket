using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class addedEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacaoFuncionarios_Employee_EmployeeId",
                table: "AvaliacaoFuncionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Card_Employee_EmployeeId",
                table: "Meal_Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Funcionarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacaoFuncionarios_Funcionarios_EmployeeId",
                table: "AvaliacaoFuncionarios",
                column: "EmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Card_Funcionarios_EmployeeId",
                table: "Meal_Card",
                column: "EmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacaoFuncionarios_Funcionarios_EmployeeId",
                table: "AvaliacaoFuncionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Card_Funcionarios_EmployeeId",
                table: "Meal_Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "Employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacaoFuncionarios_Employee_EmployeeId",
                table: "AvaliacaoFuncionarios",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Card_Employee_EmployeeId",
                table: "Meal_Card",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

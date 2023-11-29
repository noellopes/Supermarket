using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class EmployeeEvaluationChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "avaliacaoFuncionarios",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "avaliacaoFuncionarios");
        }
    }
}

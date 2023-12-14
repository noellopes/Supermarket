using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class mergedFromAvaliacaoFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluations_Funcionarios_EmployeeId",
                table: "EmployeeEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_MealCard_Funcionarios_EmployeeId",
                table: "MealCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "Employee");

            migrationBuilder.RenameColumn(
                name: "Hora_Almoco_Padrao",
                table: "Employee",
                newName: "Standard_Lunch_Hour");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.CreateTable(
                name: "SubsidySetup",
                columns: table => new
                {
                    SubsidySetupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorasMinTrabalhadas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorSubsidioDiario = table.Column<float>(type: "real", nullable: false),
                    DataPagamentoMensal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidySetup", x => x.SubsidySetupId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluations_Employee_EmployeeId",
                table: "EmployeeEvaluations",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealCard_Employee_EmployeeId",
                table: "MealCard",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluations_Employee_EmployeeId",
                table: "EmployeeEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_MealCard_Employee_EmployeeId",
                table: "MealCard");

            migrationBuilder.DropTable(
                name: "SubsidySetup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "Standard_Lunch_Hour",
                table: "Funcionarios",
                newName: "Hora_Almoco_Padrao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluations_Funcionarios_EmployeeId",
                table: "EmployeeEvaluations",
                column: "EmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealCard_Funcionarios_EmployeeId",
                table: "MealCard",
                column: "EmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

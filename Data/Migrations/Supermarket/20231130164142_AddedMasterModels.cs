using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class AddedMasterModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_avaliacaoFuncionarios",
                table: "avaliacaoFuncionarios");

            migrationBuilder.RenameTable(
                name: "avaliacaoFuncionarios",
                newName: "AvaliacaoFuncionarios");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AvaliacaoFuncionarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvaliacaoFuncionarios",
                table: "AvaliacaoFuncionarios",
                column: "EmployeeEvaluationId");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Employee_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Employee_Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Employee_Phone = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    Employee_NIF = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    Employee_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Employee_Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Admission_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Termination_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard_Check_In_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard_Check_Out_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora_Almoco_Padrao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard_Lunch_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Time_Bank = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Meal_Card",
                columns: table => new
                {
                    Card_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal_Card", x => x.Card_Id);
                    table.ForeignKey(
                        name: "FK_Meal_Card_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card_Movement",
                columns: table => new
                {
                    Movement_Id = table.Column<int>(type: "int", nullable: false),
                    Card_Id = table.Column<int>(type: "int", nullable: false),
                    Movement_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card_Movement", x => new { x.Card_Id, x.Movement_Id });
                    table.ForeignKey(
                        name: "FK_Card_Movement_Meal_Card_Card_Id",
                        column: x => x.Card_Id,
                        principalTable: "Meal_Card",
                        principalColumn: "Card_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoFuncionarios_EmployeeId",
                table: "AvaliacaoFuncionarios",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_Card_EmployeeId",
                table: "Meal_Card",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacaoFuncionarios_Employee_EmployeeId",
                table: "AvaliacaoFuncionarios",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacaoFuncionarios_Employee_EmployeeId",
                table: "AvaliacaoFuncionarios");

            migrationBuilder.DropTable(
                name: "Card_Movement");

            migrationBuilder.DropTable(
                name: "Meal_Card");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvaliacaoFuncionarios",
                table: "AvaliacaoFuncionarios");

            migrationBuilder.DropIndex(
                name: "IX_AvaliacaoFuncionarios_EmployeeId",
                table: "AvaliacaoFuncionarios");

            migrationBuilder.RenameTable(
                name: "AvaliacaoFuncionarios",
                newName: "avaliacaoFuncionarios");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "avaliacaoFuncionarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_avaliacaoFuncionarios",
                table: "avaliacaoFuncionarios",
                column: "EmployeeEvaluationId");
        }
    }
}

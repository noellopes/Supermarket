using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class Teste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card_Movement");

            migrationBuilder.DropTable(
                name: "ConfigSubsidio");

            migrationBuilder.DropTable(
                name: "Meal_Card");

            migrationBuilder.CreateTable(
                name: "MealCard",
                columns: table => new
                {
                    MealCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealCard", x => x.MealCardId);
                    table.ForeignKey(
                        name: "FK_MealCard_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardMovement",
                columns: table => new
                {
                    CardMovementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movement_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardMovement", x => x.CardMovementId);
                    table.ForeignKey(
                        name: "FK_CardMovement_MealCard_MealCardId",
                        column: x => x.MealCardId,
                        principalTable: "MealCard",
                        principalColumn: "MealCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardMovement_MealCardId",
                table: "CardMovement",
                column: "MealCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MealCard_EmployeeId",
                table: "MealCard",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardMovement");

            migrationBuilder.DropTable(
                name: "MealCard");

            migrationBuilder.CreateTable(
                name: "ConfigSubsidio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPagamentoMensal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorasMinTrabalhadas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valorSubsidioDiario = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigSubsidio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meal_Card",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Card_Id = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal_Card", x => new { x.EmployeeId, x.Card_Id });
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
                    Card_Id = table.Column<int>(type: "int", nullable: false),
                    Movement_Id = table.Column<int>(type: "int", nullable: false),
                    Meal_CardEmployeeId = table.Column<int>(type: "int", nullable: false),
                    Meal_CardCard_Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Movement_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card_Movement", x => new { x.Card_Id, x.Movement_Id });
                    table.ForeignKey(
                        name: "FK_Card_Movement_Meal_Card_Meal_CardEmployeeId_Meal_CardCard_Id",
                        columns: x => new { x.Meal_CardEmployeeId, x.Meal_CardCard_Id },
                        principalTable: "Meal_Card",
                        principalColumns: new[] { "EmployeeId", "Card_Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_Movement_Meal_CardEmployeeId_Meal_CardCard_Id",
                table: "Card_Movement",
                columns: new[] { "Meal_CardEmployeeId", "Meal_CardCard_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_Card_EmployeeId",
                table: "Meal_Card",
                column: "EmployeeId",
                unique: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Employee_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Employee_Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Employee_Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Employee_NIF = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Employee_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Employee_Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Admission_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Termination_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Standard_Check_In_Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standard_Check_Out_Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standard_Lunch_Hour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standard_Lunch_Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Time_Bank = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Folga",
                columns: table => new
                {
                    FolgaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folga", x => x.FolgaId);
                });

            migrationBuilder.CreateTable(
                name: "SubsidySetup",
                columns: table => new
                {
                    SubsidySetupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorasMinTrabalhadas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valorSubsidioDiario = table.Column<float>(type: "real", nullable: false),
                    DataPagamentoMensal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidySetup", x => x.SubsidySetupId);
                });

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
                name: "Folga");

            migrationBuilder.DropTable(
                name: "SubsidySetup");

            migrationBuilder.DropTable(
                name: "MealCard");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}

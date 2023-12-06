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
            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyFinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Departments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id_Departments = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Departments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description_Departments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id_Departments);
                    table.ForeignKey(
                        name: "FK_Departments_Schedule_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrintDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketNumber = table.Column<int>(type: "int", nullable: false),
                    TicketState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentsId_Departments = table.Column<int>(type: "int", nullable: false),
                    ClientCard = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Departments_DepartmentsId_Departments",
                        column: x => x.DepartmentsId_Departments,
                        principalTable: "Departments",
                        principalColumn: "Id_Departments",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ScheduleID",
                table: "Departments",
                column: "ScheduleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DepartmentsId_Departments",
                table: "Tickets",
                column: "DepartmentsId_Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
    }
}

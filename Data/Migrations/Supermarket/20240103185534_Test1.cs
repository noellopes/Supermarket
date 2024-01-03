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
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ReserveDepartment_EmployeeId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "ReserveDepartment");

            migrationBuilder.DropColumn(
                name: "CheckInCoordenates",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "DayBalance",
                table: "Ponto");

            migrationBuilder.RenameColumn(
                name: "CheckOutCoordenates",
                table: "Ponto",
                newName: "RealCheckOutTime");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "EmployeeSchedule",
                newName: "EmployeeScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "LunchStartTime",
                table: "Ponto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "LunchEndTime",
                table: "Ponto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutTime",
                table: "Ponto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "CheckInTime",
                table: "Ponto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ExtraHours",
                table: "Ponto",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Employee_Time_Bank",
                table: "Employee",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CardMovement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SubsidyCalculation",
                columns: table => new
                {
                    SubsidyCalculationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PontoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidyCalculation", x => x.SubsidyCalculationId);
                    table.ForeignKey(
                        name: "FK_SubsidyCalculation_Ponto_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Ponto",
                        principalColumn: "PontoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ponto_EmployeeId",
                table: "Ponto",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedule_EmployeeId",
                table: "EmployeeSchedule",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyCalculation_PontoId",
                table: "SubsidyCalculation",
                column: "PontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchedule_Employee_EmployeeId",
                table: "EmployeeSchedule",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ponto_Employee_EmployeeId",
                table: "Ponto",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchedule_Employee_EmployeeId",
                table: "EmployeeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Ponto_Employee_EmployeeId",
                table: "Ponto");

            migrationBuilder.DropTable(
                name: "SubsidyCalculation");

            migrationBuilder.DropIndex(
                name: "IX_Ponto_EmployeeId",
                table: "Ponto");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSchedule_EmployeeId",
                table: "EmployeeSchedule");

            migrationBuilder.DropColumn(
                name: "ExtraHours",
                table: "Ponto");

            migrationBuilder.RenameColumn(
                name: "RealCheckOutTime",
                table: "Ponto",
                newName: "CheckOutCoordenates");

            migrationBuilder.RenameColumn(
                name: "EmployeeScheduleId",
                table: "EmployeeSchedule",
                newName: "ScheduleId");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "LunchStartTime",
                table: "Ponto",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "LunchEndTime",
                table: "Ponto",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckOutTime",
                table: "Ponto",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckInTime",
                table: "Ponto",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CheckInCoordenates",
                table: "Ponto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DayBalance",
                table: "Ponto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Employee_Time_Bank",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CardMovement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReserveDepartment",
                columns: table => new
                {
                    ReserveDepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReserveId = table.Column<int>(type: "int", nullable: false),
                    NumeroDeFunc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveDepartment", x => x.ReserveDepartmentId);
                    table.ForeignKey(
                        name: "FK_ReserveDepartment_Reserve_ReserveId",
                        column: x => x.ReserveId,
                        principalTable: "Reserve",
                        principalColumn: "ReserveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReserveDepartment_ReserveId",
                table: "ReserveDepartment",
                column: "ReserveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_ReserveDepartment_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                principalTable: "ReserveDepartment",
                principalColumn: "ReserveDepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

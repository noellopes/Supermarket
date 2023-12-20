using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class FolgasFuncionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gestor",
                table: "Folga");

            migrationBuilder.RenameColumn(
                name: "Motivo",
                table: "Folga",
                newName: "motivo");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Folga",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "motivo",
                table: "Folga",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataResultado",
                table: "Folga",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Folga",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GestorId",
                table: "Folga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folga_FuncionarioId",
                table: "Folga",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folga_Employee_FuncionarioId",
                table: "Folga",
                column: "FuncionarioId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folga_Employee_FuncionarioId",
                table: "Folga");

            migrationBuilder.DropIndex(
                name: "IX_Folga_FuncionarioId",
                table: "Folga");

            migrationBuilder.DropColumn(
                name: "DataResultado",
                table: "Folga");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Folga");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Folga");

            migrationBuilder.RenameColumn(
                name: "motivo",
                table: "Folga",
                newName: "Motivo");

            migrationBuilder.AlterColumn<string>(
                name: "Motivo",
                table: "Folga",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Folga",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gestor",
                table: "Folga",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

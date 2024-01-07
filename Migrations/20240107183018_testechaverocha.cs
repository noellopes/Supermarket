using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Migrations
{
    /// <inheritdoc />
    public partial class testechaverocha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Departments_DepartmentsIDDepartments",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "DepartmentsIDDepartments",
                table: "Tickets",
                newName: "DepartmentIDDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_DepartmentsIDDepartments",
                table: "Tickets",
                newName: "IX_Tickets_DepartmentIDDepartments");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IDDepartments",
                table: "Tickets",
                column: "IDDepartments");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Departments_DepartmentIDDepartments",
                table: "Tickets",
                column: "DepartmentIDDepartments",
                principalTable: "Departments",
                principalColumn: "IDDepartments");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Departments_IDDepartments",
                table: "Tickets",
                column: "IDDepartments",
                principalTable: "Departments",
                principalColumn: "IDDepartments",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Departments_DepartmentIDDepartments",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Departments_IDDepartments",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_IDDepartments",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "DepartmentIDDepartments",
                table: "Tickets",
                newName: "DepartmentsIDDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_DepartmentIDDepartments",
                table: "Tickets",
                newName: "IX_Tickets_DepartmentsIDDepartments");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Departments_DepartmentsIDDepartments",
                table: "Tickets",
                column: "DepartmentsIDDepartments",
                principalTable: "Departments",
                principalColumn: "IDDepartments");
        }
    }
}

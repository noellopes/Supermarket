using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class ReserveDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Reserve",
                columns: table => new
                {
                    ReserveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserve", x => x.ReserveId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ReserveDepartment_EmployeeId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "ReserveDepartment");

            migrationBuilder.DropTable(
                name: "Reserve");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

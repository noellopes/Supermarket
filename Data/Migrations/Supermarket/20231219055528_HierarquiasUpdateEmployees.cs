using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class HierarquiasUpdateEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuncaoId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HierarquiaId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hierarquias",
                columns: table => new
                {
                    HierarquiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarquiaNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NivelHierarquico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hierarquias", x => x.HierarquiaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FuncaoId",
                table: "Employee",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_HierarquiaId",
                table: "Employee",
                column: "HierarquiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Funcao_FuncaoId",
                table: "Employee",
                column: "FuncaoId",
                principalTable: "Funcao",
                principalColumn: "FuncaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Hierarquias_HierarquiaId",
                table: "Employee",
                column: "HierarquiaId",
                principalTable: "Hierarquias",
                principalColumn: "HierarquiaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Funcao_FuncaoId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Hierarquias_HierarquiaId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Hierarquias");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FuncaoId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_HierarquiaId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "FuncaoId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "HierarquiaId",
                table: "Employee");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class Test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Movement_Meal_Card_Meal_CardEmployeeId_Meal_CardCard_Id",
                table: "Card_Movement");

            migrationBuilder.DropTable(
                name: "Meal_Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_Movement_Meal_CardEmployeeId_Meal_CardCard_Id",
                table: "Card_Movement");

            migrationBuilder.DropColumn(
                name: "Meal_CardCard_Id",
                table: "Card_Movement");

            migrationBuilder.RenameColumn(
                name: "Meal_CardEmployeeId",
                table: "Card_Movement",
                newName: "MealCardId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Card_Movement_MealCardId",
                table: "Card_Movement",
                column: "MealCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MealCard_EmployeeId",
                table: "MealCard",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Movement_MealCard_MealCardId",
                table: "Card_Movement",
                column: "MealCardId",
                principalTable: "MealCard",
                principalColumn: "MealCardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Movement_MealCard_MealCardId",
                table: "Card_Movement");

            migrationBuilder.DropTable(
                name: "MealCard");

            migrationBuilder.DropIndex(
                name: "IX_Card_Movement_MealCardId",
                table: "Card_Movement");

            migrationBuilder.RenameColumn(
                name: "MealCardId",
                table: "Card_Movement",
                newName: "Meal_CardEmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "Meal_CardCard_Id",
                table: "Card_Movement",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Card_Movement_Meal_CardEmployeeId_Meal_CardCard_Id",
                table: "Card_Movement",
                columns: new[] { "Meal_CardEmployeeId", "Meal_CardCard_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_Card_EmployeeId",
                table: "Meal_Card",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Movement_Meal_Card_Meal_CardEmployeeId_Meal_CardCard_Id",
                table: "Card_Movement",
                columns: new[] { "Meal_CardEmployeeId", "Meal_CardCard_Id" },
                principalTable: "Meal_Card",
                principalColumns: new[] { "EmployeeId", "Card_Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

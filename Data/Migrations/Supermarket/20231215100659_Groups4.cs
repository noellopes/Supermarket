using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class Groups4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_ClientCard_ClientCardId",
                table: "ProductDiscount");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscount_ClientCardId",
                table: "ProductDiscount");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientCard",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "ClientCard",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCard_ClientId",
                table: "ClientCard",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCard_Client_ClientId",
                table: "ClientCard",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_ClientCard_ProductId",
                table: "ProductDiscount",
                column: "ProductId",
                principalTable: "ClientCard",
                principalColumn: "ClientCardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCard_Client_ClientId",
                table: "ClientCard");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_ClientCard_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_ClientCard_ClientId",
                table: "ClientCard");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientCard");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ClientCard");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ClientCardId",
                table: "ProductDiscount",
                column: "ClientCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_ClientCard_ClientCardId",
                table: "ProductDiscount",
                column: "ClientCardId",
                principalTable: "ClientCard",
                principalColumn: "ClientCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

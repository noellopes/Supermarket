using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Data.Migrations.Supermarket
{
    /// <inheritdoc />
    public partial class AddlinkNtoNbetweenFuncaoandGrupoProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcao_GrupoProjeto_GrupoProjetoProjetoId",
                table: "Funcao");

            migrationBuilder.DropIndex(
                name: "IX_Funcao_GrupoProjetoProjetoId",
                table: "Funcao");

            migrationBuilder.DropColumn(
                name: "GrupoProjetoProjetoId",
                table: "Funcao");

            migrationBuilder.CreateTable(
                name: "FuncaoGrupoProjeto",
                columns: table => new
                {
                    FuncaoId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncaoGrupoProjeto", x => new { x.FuncaoId, x.ProjetoId });
                    table.ForeignKey(
                        name: "FK_FuncaoGrupoProjeto_Funcao_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcao",
                        principalColumn: "FuncaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncaoGrupoProjeto_GrupoProjeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "GrupoProjeto",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncaoGrupoProjeto_ProjetoId",
                table: "FuncaoGrupoProjeto",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncaoGrupoProjeto");

            migrationBuilder.AddColumn<int>(
                name: "GrupoProjetoProjetoId",
                table: "Funcao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcao_GrupoProjetoProjetoId",
                table: "Funcao",
                column: "GrupoProjetoProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcao_GrupoProjeto_GrupoProjetoProjetoId",
                table: "Funcao",
                column: "GrupoProjetoProjetoId",
                principalTable: "GrupoProjeto",
                principalColumn: "ProjetoId");
        }
    }
}

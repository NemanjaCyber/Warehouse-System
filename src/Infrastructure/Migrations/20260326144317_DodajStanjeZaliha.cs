using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DodajStanjeZaliha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StanjaZaliha_Artikli_ArtikalId",
                table: "StanjaZaliha");

            migrationBuilder.DropForeignKey(
                name: "FK_StanjaZaliha_Lokacije_LokacijaId",
                table: "StanjaZaliha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StanjaZaliha",
                table: "StanjaZaliha");

            migrationBuilder.RenameTable(
                name: "StanjaZaliha",
                newName: "StanjeZaliha");

            migrationBuilder.RenameColumn(
                name: "TrenutnaKolicina",
                table: "StanjeZaliha",
                newName: "Kolicina");

            migrationBuilder.RenameIndex(
                name: "IX_StanjaZaliha_LokacijaId",
                table: "StanjeZaliha",
                newName: "IX_StanjeZaliha_LokacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_StanjaZaliha_ArtikalId",
                table: "StanjeZaliha",
                newName: "IX_StanjeZaliha_ArtikalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StanjeZaliha",
                table: "StanjeZaliha",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StanjeZaliha_Artikli_ArtikalId",
                table: "StanjeZaliha",
                column: "ArtikalId",
                principalTable: "Artikli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StanjeZaliha_Lokacije_LokacijaId",
                table: "StanjeZaliha",
                column: "LokacijaId",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StanjeZaliha_Artikli_ArtikalId",
                table: "StanjeZaliha");

            migrationBuilder.DropForeignKey(
                name: "FK_StanjeZaliha_Lokacije_LokacijaId",
                table: "StanjeZaliha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StanjeZaliha",
                table: "StanjeZaliha");

            migrationBuilder.RenameTable(
                name: "StanjeZaliha",
                newName: "StanjaZaliha");

            migrationBuilder.RenameColumn(
                name: "Kolicina",
                table: "StanjaZaliha",
                newName: "TrenutnaKolicina");

            migrationBuilder.RenameIndex(
                name: "IX_StanjeZaliha_LokacijaId",
                table: "StanjaZaliha",
                newName: "IX_StanjaZaliha_LokacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_StanjeZaliha_ArtikalId",
                table: "StanjaZaliha",
                newName: "IX_StanjaZaliha_ArtikalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StanjaZaliha",
                table: "StanjaZaliha",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StanjaZaliha_Artikli_ArtikalId",
                table: "StanjaZaliha",
                column: "ArtikalId",
                principalTable: "Artikli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StanjaZaliha_Lokacije_LokacijaId",
                table: "StanjaZaliha",
                column: "LokacijaId",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicijalnaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    Sifra = table.Column<string>(type: "text", nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimalnaKolicina = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    KodLokacije = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransakcijeZaliha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtikalId = table.Column<int>(type: "integer", nullable: false),
                    Kolicina = table.Column<int>(type: "integer", nullable: false),
                    Tip = table.Column<int>(type: "integer", nullable: false),
                    DatumVreme = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KorisnikId = table.Column<string>(type: "text", nullable: false),
                    Napomena = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransakcijeZaliha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransakcijeZaliha_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanjaZaliha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtikalId = table.Column<int>(type: "integer", nullable: false),
                    LokacijaId = table.Column<int>(type: "integer", nullable: false),
                    TrenutnaKolicina = table.Column<int>(type: "integer", nullable: false),
                    PoslednjeAzuriranje = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanjaZaliha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StanjaZaliha_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanjaZaliha_Lokacije_LokacijaId",
                        column: x => x.LokacijaId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_Sifra",
                table: "Artikli",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StanjaZaliha_ArtikalId",
                table: "StanjaZaliha",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_StanjaZaliha_LokacijaId",
                table: "StanjaZaliha",
                column: "LokacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijeZaliha_ArtikalId",
                table: "TransakcijeZaliha",
                column: "ArtikalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanjaZaliha");

            migrationBuilder.DropTable(
                name: "TransakcijeZaliha");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "Artikli");
        }
    }
}

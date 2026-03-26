using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DodajLokaciju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KodLokacije",
                table: "Lokacije",
                newName: "Opis");

            migrationBuilder.AddColumn<string>(
                name: "Kod",
                table: "Lokacije",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kod",
                table: "Lokacije");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "Lokacije",
                newName: "KodLokacije");
        }
    }
}

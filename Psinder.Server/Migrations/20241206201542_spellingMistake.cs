using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Server.Migrations
{
    /// <inheritdoc />
    public partial class spellingMistake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuldingNumber",
                table: "Shelters",
                newName: "BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "ApartementNumber",
                table: "Shelters",
                newName: "ApartmentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Shelters",
                newName: "BuldingNumber");

            migrationBuilder.RenameColumn(
                name: "ApartmentNumber",
                table: "Shelters",
                newName: "ApartementNumber");
        }
    }
}

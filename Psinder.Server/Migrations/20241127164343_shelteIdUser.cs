using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Server.Migrations
{
    /// <inheritdoc />
    public partial class shelteIdUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Users");
        }
    }
}

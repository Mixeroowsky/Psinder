using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Server.Migrations
{
    /// <inheritdoc />
    public partial class shelterForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shelters_ShelterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShelterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Shelters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_UserId",
                table: "Shelters",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelters_Users_UserId",
                table: "Shelters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelters_Users_UserId",
                table: "Shelters");

            migrationBuilder.DropIndex(
                name: "IX_Shelters_UserId",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shelters");

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShelterId",
                table: "Users",
                column: "ShelterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Shelters_ShelterId",
                table: "Users",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

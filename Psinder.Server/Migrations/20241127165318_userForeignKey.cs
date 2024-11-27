using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psinder.Server.Migrations
{
    /// <inheritdoc />
    public partial class userForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shelters_ShelterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShelterId",
                table: "Users");
        }
    }
}

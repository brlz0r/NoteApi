using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApi.Migrations
{
    public partial class PathWrapper_update_userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathWrappers_Users_UserId",
                table: "PathWrappers");

            migrationBuilder.DropIndex(
                name: "IX_PathWrappers_UserId",
                table: "PathWrappers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PathWrappers_UserId",
                table: "PathWrappers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PathWrappers_Users_UserId",
                table: "PathWrappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

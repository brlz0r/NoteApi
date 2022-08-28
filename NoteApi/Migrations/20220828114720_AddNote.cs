using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApi.Migrations
{
    public partial class AddNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PathWrappers");

            migrationBuilder.AddColumn<Guid>(
                name: "NoteId",
                table: "PathWrappers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PathWrappers_NoteId",
                table: "PathWrappers",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PathWrappers_Notes_NoteId",
                table: "PathWrappers",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathWrappers_Notes_NoteId",
                table: "PathWrappers");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_PathWrappers_NoteId",
                table: "PathWrappers");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "PathWrappers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PathWrappers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

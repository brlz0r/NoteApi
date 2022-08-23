using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathWrappers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrokeWidth = table.Column<float>(type: "real", nullable: false),
                    StrokeColor = table.Column<long>(type: "bigint", nullable: false),
                    Alpha = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathWrappers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathWrappers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X = table.Column<float>(type: "real", nullable: false),
                    Y = table.Column<float>(type: "real", nullable: false),
                    PathWrapperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_PathWrappers_PathWrapperId",
                        column: x => x.PathWrapperId,
                        principalTable: "PathWrappers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PathWrappers_UserId",
                table: "PathWrappers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_PathWrapperId",
                table: "Points",
                column: "PathWrapperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "PathWrappers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

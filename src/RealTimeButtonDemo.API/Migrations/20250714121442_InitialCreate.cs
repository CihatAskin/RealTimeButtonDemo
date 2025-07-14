using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeButtonDemo.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ButtonStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LastClickedBy = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastClickedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButtonStates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ButtonStates",
                columns: new[] { "Id", "Color", "IsActive", "LastClickedAt", "LastClickedBy", "RoomId" },
                values: new object[] { 1, "#007bff", true, new DateTime(2025, 7, 14, 12, 14, 42, 602, DateTimeKind.Utc).AddTicks(3485), "system", "demo-room" });

            migrationBuilder.CreateIndex(
                name: "IX_ButtonStates_RoomId",
                table: "ButtonStates",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ButtonStates");
        }
    }
}

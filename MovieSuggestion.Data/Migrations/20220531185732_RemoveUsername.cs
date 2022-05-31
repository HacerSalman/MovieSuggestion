using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieSuggestion.Data.Migrations
{
    public partial class RemoveUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_user_username",
                table: "user");

            migrationBuilder.DropColumn(
                name: "username",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "username",
                table: "user",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_user_username",
                table: "user",
                column: "username");
        }
    }
}

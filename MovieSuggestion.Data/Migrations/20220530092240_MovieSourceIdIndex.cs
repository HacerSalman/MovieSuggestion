using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieSuggestion.Data.Migrations
{
    public partial class MovieSourceIdIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_movie_source_id",
                table: "movie",
                column: "source_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_movie_source_id",
                table: "movie");
        }
    }
}

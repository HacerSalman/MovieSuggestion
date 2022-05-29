using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieSuggestion.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "entity_status",
                columns: table => new
                {
                    v = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_status", x => x.v);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    v = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.v);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    original_title = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    overview = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    score = table.Column<double>(type: "double", nullable: false),
                    adult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    backdrop_path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    source_id = table.Column<long>(type: "bigint", nullable: false),
                    original_language = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    popularity = table.Column<double>(type: "double", nullable: false),
                    poster_path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    video = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    status = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    Update_time = table.Column<long>(type: "bigint", nullable: false),
                    owner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.id);
                    table.ForeignKey(
                        name: "FK_movie_entity_status_status",
                        column: x => x.status,
                        principalTable: "entity_status",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    Update_time = table.Column<long>(type: "bigint", nullable: false),
                    owner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.UniqueConstraint("AK_user_username", x => x.username);
                    table.ForeignKey(
                        name: "FK_user_entity_status_status",
                        column: x => x.status,
                        principalTable: "entity_status",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_movie",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    movie_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    score = table.Column<double>(type: "double", nullable: false),
                    status = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    Update_time = table.Column<long>(type: "bigint", nullable: false),
                    owner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_movie", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_movie_entity_status_status",
                        column: x => x.status,
                        principalTable: "entity_status",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_movie_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_movie_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_movie_note",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    movie_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    note = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    Update_time = table.Column<long>(type: "bigint", nullable: false),
                    owner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_movie_note", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_movie_note_entity_status_status",
                        column: x => x.status,
                        principalTable: "entity_status",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_movie_note_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_movie_note_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_permission",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    permission = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "VARCHAR(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    Update_time = table.Column<long>(type: "bigint", nullable: false),
                    owner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_permission_entity_status_status",
                        column: x => x.status,
                        principalTable: "entity_status",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_permission_permission_permission",
                        column: x => x.permission,
                        principalTable: "permission",
                        principalColumn: "v",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_permission_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "entity_status",
                column: "v",
                values: new object[]
                {
                    "ACTIVE",
                    "PASSIVE",
                    "DELETED"
                });

            migrationBuilder.InsertData(
                table: "permission",
                column: "v",
                values: new object[]
                {
                    "USER_MANAGE",
                    "MOVIE_MANAGE",
                    "MOVIE_DELETE"
                });

            migrationBuilder.CreateIndex(
                name: "IX_movie_create_time",
                table: "movie",
                column: "create_time");

            migrationBuilder.CreateIndex(
                name: "IX_movie_score",
                table: "movie",
                column: "score");

            migrationBuilder.CreateIndex(
                name: "IX_movie_status",
                table: "movie",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_movie_title",
                table: "movie",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_movie_Update_time",
                table: "movie",
                column: "Update_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_create_time",
                table: "user",
                column: "create_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_name",
                table: "user",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_user_status",
                table: "user",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_Update_time",
                table: "user",
                column: "Update_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_create_time",
                table: "user_movie",
                column: "create_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_movie_id",
                table: "user_movie",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_status",
                table: "user_movie",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_Update_time",
                table: "user_movie",
                column: "Update_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_user_id_movie_id",
                table: "user_movie",
                columns: new[] { "user_id", "movie_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_note_create_time",
                table: "user_movie_note",
                column: "create_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_note_movie_id",
                table: "user_movie_note",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_note_status",
                table: "user_movie_note",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_note_Update_time",
                table: "user_movie_note",
                column: "Update_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_note_user_id",
                table: "user_movie_note",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_permission_create_time",
                table: "user_permission",
                column: "create_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_permission_permission_user_id",
                table: "user_permission",
                columns: new[] { "permission", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_permission_status",
                table: "user_permission",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_user_permission_Update_time",
                table: "user_permission",
                column: "Update_time");

            migrationBuilder.CreateIndex(
                name: "IX_user_permission_user_id",
                table: "user_permission",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_movie");

            migrationBuilder.DropTable(
                name: "user_movie_note");

            migrationBuilder.DropTable(
                name: "user_permission");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "entity_status");
        }
    }
}

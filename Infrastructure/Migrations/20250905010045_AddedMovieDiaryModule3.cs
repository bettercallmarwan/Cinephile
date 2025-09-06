using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMovieDiaryModule3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieDiaryId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MovieDiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieDiaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieDiaries_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_MovieDiaryId",
                table: "Logs",
                column: "MovieDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDiaries_MovieId",
                table: "MovieDiaries",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDiaries_UserId",
                table: "MovieDiaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_MovieDiaries_MovieDiaryId",
                table: "Logs",
                column: "MovieDiaryId",
                principalTable: "MovieDiaries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_MovieDiaries_MovieDiaryId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "MovieDiaries");

            migrationBuilder.DropIndex(
                name: "IX_Logs_MovieDiaryId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "MovieDiaryId",
                table: "Logs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AlternativeName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    MovieLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Top250 = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSeries = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswHash = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    value = table.Column<double>(type: "REAL", nullable: false),
                    MovieId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryEntityMovieEntity",
                columns: table => new
                {
                    CountriesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MoviesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryEntityMovieEntity", x => new { x.CountriesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CountryEntityMovieEntity_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryEntityMovieEntity_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreEntityMovieEntity",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MoviesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreEntityMovieEntity", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreEntityMovieEntity_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreEntityMovieEntity_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    kp = table.Column<double>(type: "REAL", nullable: false),
                    imdb = table.Column<double>(type: "REAL", nullable: false),
                    filmCritics = table.Column<double>(type: "REAL", nullable: false),
                    russianFilmCritics = table.Column<double>(type: "REAL", nullable: false),
                    MovieId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntityUserEntity",
                columns: table => new
                {
                    FavMovieUsersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FavMoviesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntityUserEntity", x => new { x.FavMovieUsersId, x.FavMoviesId });
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity_Movies_FavMoviesId",
                        column: x => x.FavMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity_Users_FavMovieUsersId",
                        column: x => x.FavMovieUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntityUserEntity1",
                columns: table => new
                {
                    WatchLaterMoviesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WatchLaterUsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntityUserEntity1", x => new { x.WatchLaterMoviesId, x.WatchLaterUsersId });
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity1_Movies_WatchLaterMoviesId",
                        column: x => x.WatchLaterMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity1_Users_WatchLaterUsersId",
                        column: x => x.WatchLaterUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_MovieId",
                table: "Budgets",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryEntityMovieEntity_MoviesId",
                table: "CountryEntityMovieEntity",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreEntityMovieEntity_MoviesId",
                table: "GenreEntityMovieEntity",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntityUserEntity_FavMoviesId",
                table: "MovieEntityUserEntity",
                column: "FavMoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntityUserEntity1_WatchLaterUsersId",
                table: "MovieEntityUserEntity1",
                column: "WatchLaterUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "CountryEntityMovieEntity");

            migrationBuilder.DropTable(
                name: "GenreEntityMovieEntity");

            migrationBuilder.DropTable(
                name: "MovieEntityUserEntity");

            migrationBuilder.DropTable(
                name: "MovieEntityUserEntity1");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

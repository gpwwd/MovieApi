using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class ratings_short_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "RussianFilmCritics",
                table: "Ratings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Kp",
                table: "Ratings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Imdb",
                table: "Ratings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "FilmCritics",
                table: "Ratings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "RussianFilmCritics",
                table: "Ratings",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Kp",
                table: "Ratings",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Imdb",
                table: "Ratings",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FilmCritics",
                table: "Ratings",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class manytomanynewtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity_Movies_FavMoviesId",
                table: "MovieEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity_Users_FavMovieUsersId",
                table: "MovieEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity1_Movies_WatchLaterMoviesId",
                table: "MovieEntityUserEntity1");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity1_Users_WatchLaterUsersId",
                table: "MovieEntityUserEntity1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieEntityUserEntity1",
                table: "MovieEntityUserEntity1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieEntityUserEntity",
                table: "MovieEntityUserEntity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0b4eaead-0cdd-41e3-afee-d09afdf239c3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e3fedb0d-a919-4ed4-95a9-bc156a529c24"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe58e750-cc57-4171-8b54-24e6d7a36d26"));

            migrationBuilder.RenameTable(
                name: "MovieEntityUserEntity1",
                newName: "WatchLaterMoviesUsers");

            migrationBuilder.RenameTable(
                name: "MovieEntityUserEntity",
                newName: "FavMovieUsers");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEntityUserEntity1_WatchLaterUsersId",
                table: "WatchLaterMoviesUsers",
                newName: "IX_WatchLaterMoviesUsers_WatchLaterUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEntityUserEntity_FavMoviesId",
                table: "FavMovieUsers",
                newName: "IX_FavMovieUsers_FavMoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchLaterMoviesUsers",
                table: "WatchLaterMoviesUsers",
                columns: new[] { "WatchLaterMoviesId", "WatchLaterUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavMovieUsers",
                table: "FavMovieUsers",
                columns: new[] { "FavMovieUsersId", "FavMoviesId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1eea000b-474c-47cc-a551-cf62cb8c6a0b"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("9a6261f1-aae2-4335-9e4e-a4c36135c19e"), null, "User", "USER" },
                    { new Guid("aaf977a4-2138-4d2e-8143-e1fad1359c5f"), null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FavMovieUsers_Movies_FavMoviesId",
                table: "FavMovieUsers",
                column: "FavMoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavMovieUsers_Users_FavMovieUsersId",
                table: "FavMovieUsers",
                column: "FavMovieUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterMoviesUsers_Movies_WatchLaterMoviesId",
                table: "WatchLaterMoviesUsers",
                column: "WatchLaterMoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterMoviesUsers_Users_WatchLaterUsersId",
                table: "WatchLaterMoviesUsers",
                column: "WatchLaterUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavMovieUsers_Movies_FavMoviesId",
                table: "FavMovieUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavMovieUsers_Users_FavMovieUsersId",
                table: "FavMovieUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterMoviesUsers_Movies_WatchLaterMoviesId",
                table: "WatchLaterMoviesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterMoviesUsers_Users_WatchLaterUsersId",
                table: "WatchLaterMoviesUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchLaterMoviesUsers",
                table: "WatchLaterMoviesUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavMovieUsers",
                table: "FavMovieUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1eea000b-474c-47cc-a551-cf62cb8c6a0b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9a6261f1-aae2-4335-9e4e-a4c36135c19e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aaf977a4-2138-4d2e-8143-e1fad1359c5f"));

            migrationBuilder.RenameTable(
                name: "WatchLaterMoviesUsers",
                newName: "MovieEntityUserEntity1");

            migrationBuilder.RenameTable(
                name: "FavMovieUsers",
                newName: "MovieEntityUserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaterMoviesUsers_WatchLaterUsersId",
                table: "MovieEntityUserEntity1",
                newName: "IX_MovieEntityUserEntity1_WatchLaterUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_FavMovieUsers_FavMoviesId",
                table: "MovieEntityUserEntity",
                newName: "IX_MovieEntityUserEntity_FavMoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieEntityUserEntity1",
                table: "MovieEntityUserEntity1",
                columns: new[] { "WatchLaterMoviesId", "WatchLaterUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieEntityUserEntity",
                table: "MovieEntityUserEntity",
                columns: new[] { "FavMovieUsersId", "FavMoviesId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0b4eaead-0cdd-41e3-afee-d09afdf239c3"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("e3fedb0d-a919-4ed4-95a9-bc156a529c24"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("fe58e750-cc57-4171-8b54-24e6d7a36d26"), null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity_Movies_FavMoviesId",
                table: "MovieEntityUserEntity",
                column: "FavMoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity_Users_FavMovieUsersId",
                table: "MovieEntityUserEntity",
                column: "FavMovieUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity1_Movies_WatchLaterMoviesId",
                table: "MovieEntityUserEntity1",
                column: "WatchLaterMoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity1_Users_WatchLaterUsersId",
                table: "MovieEntityUserEntity1",
                column: "WatchLaterUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

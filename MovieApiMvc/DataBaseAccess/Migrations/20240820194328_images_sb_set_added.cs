using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class images_sb_set_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfoEntity_Movies_MovieId",
                table: "ImageInfoEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageInfoEntity",
                table: "ImageInfoEntity");

            migrationBuilder.RenameTable(
                name: "ImageInfoEntity",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_ImageInfoEntity_MovieId",
                table: "Images",
                newName: "IX_Images_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Movies_MovieId",
                table: "Images",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Movies_MovieId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ImageInfoEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Images_MovieId",
                table: "ImageInfoEntity",
                newName: "IX_ImageInfoEntity_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageInfoEntity",
                table: "ImageInfoEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfoEntity_Movies_MovieId",
                table: "ImageInfoEntity",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

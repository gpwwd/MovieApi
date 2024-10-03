using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserFiledsForRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6a1b05a5-0d9d-4c95-932a-d8aca688e2b0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9809bf73-f4d6-4251-9095-a13b4a3ebe1d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9bdd45ac-c729-47ff-b660-843f0364eaea"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("19641c2c-0ac3-444a-b64a-1eca1fe7119d"), null, "User", "USER" },
                    { new Guid("b0f222ee-5a5f-4948-8557-f04c09f8867d"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("facb167c-4263-4a8b-bb98-1edb0e1bc168"), null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("19641c2c-0ac3-444a-b64a-1eca1fe7119d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b0f222ee-5a5f-4948-8557-f04c09f8867d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("facb167c-4263-4a8b-bb98-1edb0e1bc168"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6a1b05a5-0d9d-4c95-932a-d8aca688e2b0"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("9809bf73-f4d6-4251-9095-a13b4a3ebe1d"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("9bdd45ac-c729-47ff-b660-843f0364eaea"), null, "User", "USER" }
                });
        }
    }
}

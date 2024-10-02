using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

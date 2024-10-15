using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0568acb7-8206-4f88-be7b-db627d32b5ef"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2124ddf3-c11e-49c0-943e-92ad21aac8f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8b7ba6c4-b84c-41ac-87c9-168705027d89"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0b4eaead-0cdd-41e3-afee-d09afdf239c3"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("e3fedb0d-a919-4ed4-95a9-bc156a529c24"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("fe58e750-cc57-4171-8b54-24e6d7a36d26"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0568acb7-8206-4f88-be7b-db627d32b5ef"), null, "Subscriber", "SUBSCRIBER" },
                    { new Guid("2124ddf3-c11e-49c0-943e-92ad21aac8f8"), null, "User", "USER" },
                    { new Guid("8b7ba6c4-b84c-41ac-87c9-168705027d89"), null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}

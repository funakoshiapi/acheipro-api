using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace acheipro_api.Migrations
{
    /// <inheritdoc />
    public partial class passwordRecovery5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0104bbed-56e6-454b-b701-9f3647148223");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f745a4-734d-4d99-9a83-f0b4a9cac330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45ad20e7-eafa-439a-be1c-fcdf907e154d");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PasswordRecoveries");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9312f3f0-1406-4c85-b199-1b37be945a60", null, "Administrator", "ADMINISTRATOR" },
                    { "9c6a6a15-5f41-4129-96e8-9fbc38d9dbb9", null, "Manager", "MANAGER" },
                    { "afb5bb46-481a-4b7b-9b6b-242fa81c73de", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9312f3f0-1406-4c85-b199-1b37be945a60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c6a6a15-5f41-4129-96e8-9fbc38d9dbb9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb5bb46-481a-4b7b-9b6b-242fa81c73de");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PasswordRecoveries",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0104bbed-56e6-454b-b701-9f3647148223", null, "Administrator", "ADMINISTRATOR" },
                    { "30f745a4-734d-4d99-9a83-f0b4a9cac330", null, "Manager", "MANAGER" },
                    { "45ad20e7-eafa-439a-be1c-fcdf907e154d", null, "User", "USER" }
                });
        }
    }
}

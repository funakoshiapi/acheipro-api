using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace acheipro_api.Migrations
{
    /// <inheritdoc />
    public partial class passwordRecovery2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d216254e-eeb0-496b-aa32-3713cf09b8d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6dd552e-4ab8-44e1-bb20-88851073d630");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd3b740-cb74-41d1-a5c5-9fb148ee1f55");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PasswordRecoveries");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PasswordRecoveries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76f6df52-01bd-46c8-a167-3287356c9474", null, "User", "USER" },
                    { "82498827-00c4-4f8d-8f5e-3ad3736feff5", null, "Manager", "MANAGER" },
                    { "be8383d4-6cb6-499a-8ba5-48144622e00a", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76f6df52-01bd-46c8-a167-3287356c9474");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82498827-00c4-4f8d-8f5e-3ad3736feff5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be8383d4-6cb6-499a-8ba5-48144622e00a");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PasswordRecoveries");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "PasswordRecoveries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d216254e-eeb0-496b-aa32-3713cf09b8d2", null, "User", "USER" },
                    { "e6dd552e-4ab8-44e1-bb20-88851073d630", null, "Administrator", "ADMINISTRATOR" },
                    { "fbd3b740-cb74-41d1-a5c5-9fb148ee1f55", null, "Manager", "MANAGER" }
                });
        }
    }
}

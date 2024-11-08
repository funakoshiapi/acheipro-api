using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace acheipro_api.Migrations
{
    /// <inheritdoc />
    public partial class newAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64365909-9161-4523-9b4c-829c2bfcb63f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da932f7b-9be3-419a-8160-c3782740bde9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2008cde-9c61-43cb-858f-8d1cb7bbf6b2");

            migrationBuilder.AddColumn<bool>(
                name: "Claimed",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "X",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2744298c-d049-4f7c-ae8a-eb72f0f3e018", null, "Manager", "MANAGER" },
                    { "76ad585e-aa77-4935-8cfb-7cfac6de6893", null, "User", "USER" },
                    { "a1023cb1-66ec-4be9-8b9e-390b63360e6d", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2744298c-d049-4f7c-ae8a-eb72f0f3e018");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76ad585e-aa77-4935-8cfb-7cfac6de6893");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1023cb1-66ec-4be9-8b9e-390b63360e6d");

            migrationBuilder.DropColumn(
                name: "Claimed",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "64365909-9161-4523-9b4c-829c2bfcb63f", null, "Administrator", "ADMINISTRATOR" },
                    { "da932f7b-9be3-419a-8160-c3782740bde9", null, "User", "USER" },
                    { "e2008cde-9c61-43cb-858f-8d1cb7bbf6b2", null, "Manager", "MANAGER" }
                });
        }
    }
}

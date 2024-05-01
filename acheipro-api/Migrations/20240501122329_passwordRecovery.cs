using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace acheipro_api.Migrations
{
    /// <inheritdoc />
    public partial class passwordRecovery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "463fa19e-6e18-4fe0-9b94-0b3d81818b3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97cb6a6f-e843-4d5d-8b4d-5fd64e4a439b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca820c54-9100-4f60-bc31-e641ca1ab51d");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("6f37f3af-7ba0-4365-a98f-924c9a865c8d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("7437b1bb-21ac-4d65-aaf5-c689da20b50d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("af58eeaa-9f5b-11ee-8c90-0242ac120002"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002"));

            migrationBuilder.AlterColumn<string>(
                name: "CompanyMission",
                table: "CompanyDatas",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyDescription",
                table: "CompanyDatas",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateTable(
                name: "PasswordRecoveries",
                columns: table => new
                {
                    RecoveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordRecoveries", x => x.RecoveryId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordRecoveries");

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

            migrationBuilder.AlterColumn<string>(
                name: "CompanyMission",
                table: "CompanyDatas",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyDescription",
                table: "CompanyDatas",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "463fa19e-6e18-4fe0-9b94-0b3d81818b3b", null, "Administrator", "ADMINISTRATOR" },
                    { "97cb6a6f-e843-4d5d-8b4d-5fd64e4a439b", null, "User", "USER" },
                    { "ca820c54-9100-4f60-bc31-e641ca1ab51d", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Email", "ImageName", "Industry", "Name", "Province", "Role", "Telephone", "Website" },
                values: new object[,]
                {
                    { new Guid("af58eeaa-9f5b-11ee-8c90-0242ac120002"), "Golfe 2, Rua 3", "Angola", null, null, "Juridicos", "Luanda Legal LLC", "Luanda", "Advogado", null, null },
                    { new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002"), "Talatona, Rua 6", "Angola", null, null, "Contabilidade", "Contabilistical LLC", "Luanda", "Contabilidade", null, null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("6f37f3af-7ba0-4365-a98f-924c9a865c8d"), new Guid("af58eeaa-9f5b-11ee-8c90-0242ac120002"), "Felipe Sousa", "Software Developer" },
                    { new Guid("7437b1bb-21ac-4d65-aaf5-c689da20b50d"), new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002"), "Mbula Matadi", "Director Relacoes Publicas" }
                });
        }
    }
}

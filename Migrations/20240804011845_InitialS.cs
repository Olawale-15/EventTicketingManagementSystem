using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("54126ef3-39e0-4130-a877-98ae5d9e2117"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9c49148-a024-4836-af11-f03bfdea0e48"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("6f65636f-68bb-4424-9a2d-0125f90eab8e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc715a30-9655-433a-8098-3084d5717c49"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b31b2e4-caa6-4318-8428-74d17bcd9b7c"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("591743c6-53f3-43df-85be-cee239001d45"), "EventOrganizer" },
                    { new Guid("6035035c-81e2-4367-beaa-61ac8979015b"), "Attendee" },
                    { new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38"), "admin@gmail.com", "$2a$10$k7NDCRNIZTWgDs8/PNux3OzbYKm0TywSMAGkazDQtU1ONA.lHgdx2", new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"), "$2a$10$k7NDCRNIZTWgDs8/PNux3O", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("7127b573-2375-4eda-a82c-a05ef707431a"), 0m, new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("591743c6-53f3-43df-85be-cee239001d45"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6035035c-81e2-4367-beaa-61ac8979015b"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("7127b573-2375-4eda-a82c-a05ef707431a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4b31b2e4-caa6-4318-8428-74d17bcd9b7c"), "Admin" },
                    { new Guid("54126ef3-39e0-4130-a877-98ae5d9e2117"), "Attendee" },
                    { new Guid("a9c49148-a024-4836-af11-f03bfdea0e48"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("bc715a30-9655-433a-8098-3084d5717c49"), "admin@gmail.com", "$2a$10$X7YADiLRyhmlEq0GmDJlLexqfuvkapYYZLSpz.az99fI7sLX7iKeG", new Guid("4b31b2e4-caa6-4318-8428-74d17bcd9b7c"), "$2a$10$X7YADiLRyhmlEq0GmDJlLe", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("6f65636f-68bb-4424-9a2d-0125f90eab8e"), 0m, new Guid("bc715a30-9655-433a-8098-3084d5717c49") });
        }
    }
}

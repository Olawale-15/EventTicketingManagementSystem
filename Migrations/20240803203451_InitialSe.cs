using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialSe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("72ff14d6-5c1b-451a-9ed0-8ead62cac524"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e6475b9e-4f6a-4964-9538-d5a31651c70a"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("a8f4de54-430e-4af2-91f4-d3aa1c8c4a80"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7aca3a7-1c88-4a5d-9b87-89d6cb12d526"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0bde6aff-b582-4635-ae5d-aa26e09c2436"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0bde6aff-b582-4635-ae5d-aa26e09c2436"), "Admin" },
                    { new Guid("72ff14d6-5c1b-451a-9ed0-8ead62cac524"), "Attendee" },
                    { new Guid("e6475b9e-4f6a-4964-9538-d5a31651c70a"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("b7aca3a7-1c88-4a5d-9b87-89d6cb12d526"), "admin@gmail.com", "$2a$10$EJpDrk71MKDwFXcEwtKt0OCeqsmLtNQpI7x0M4NPUTBuAxpPhZ2oC", new Guid("0bde6aff-b582-4635-ae5d-aa26e09c2436"), "$2a$10$EJpDrk71MKDwFXcEwtKt0O", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("a8f4de54-430e-4af2-91f4-d3aa1c8c4a80"), 0m, new Guid("b7aca3a7-1c88-4a5d-9b87-89d6cb12d526") });
        }
    }
}

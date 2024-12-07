using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialSee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1326682e-8e3d-4a53-bdb1-955d41a067a8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("50a03c94-8294-477b-8323-d580e3318b09"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("626286b9-60a5-426f-8aa8-2bfe16391bbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f05c6d7e-e545-4b05-9c0b-6fe8ed098773"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ba117bdc-a7d6-4af4-bb6c-14c26da89923"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1326682e-8e3d-4a53-bdb1-955d41a067a8"), "Attendee" },
                    { new Guid("50a03c94-8294-477b-8323-d580e3318b09"), "EventOrganizer" },
                    { new Guid("ba117bdc-a7d6-4af4-bb6c-14c26da89923"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("f05c6d7e-e545-4b05-9c0b-6fe8ed098773"), "admin@gmail.com", "$2a$10$/KzraG4Pr3PnaFb.d/07ye8n3WRtYXA.Lqih33wG1I9L6yiJB.GrK", new Guid("ba117bdc-a7d6-4af4-bb6c-14c26da89923"), "$2a$10$/KzraG4Pr3PnaFb.d/07ye", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("626286b9-60a5-426f-8aa8-2bfe16391bbb"), 0m, new Guid("f05c6d7e-e545-4b05-9c0b-6fe8ed098773") });
        }
    }
}

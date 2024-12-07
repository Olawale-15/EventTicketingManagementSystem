using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Restart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac1d148f-ecc4-4db3-8710-e8d414003687"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f842d80e-e59c-487b-ae9b-f22ad80760d2"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("b0cfda33-36f8-467e-8cdd-0b27fc1c051b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e2273ad7-b0d6-4cbc-8ff3-d709c0d7daa6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1dacf4bd-8b66-4932-a33d-f33b1775d44e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("479691df-0a60-47cb-bcae-5d0704f8beef"), "Admin" },
                    { new Guid("720cc931-2768-4417-a9bf-badacb8497ab"), "Attendee" },
                    { new Guid("a9642cfe-4bb5-4bd0-a501-02576459a822"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("9970ed8c-d851-4173-900e-84a0b4caa837"), "admin@gmail.com", "$2a$10$yNXRInrrAPs/Q5CXEXwkjOxJxeWfbUL4K3Yw6uBYE/muAPXs.Heh.", new Guid("479691df-0a60-47cb-bcae-5d0704f8beef"), "$2a$10$yNXRInrrAPs/Q5CXEXwkjO", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("904d335b-4e92-4265-9bf7-4d1707c4b253"), 0m, new Guid("9970ed8c-d851-4173-900e-84a0b4caa837") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("720cc931-2768-4417-a9bf-badacb8497ab"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9642cfe-4bb5-4bd0-a501-02576459a822"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("904d335b-4e92-4265-9bf7-4d1707c4b253"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9970ed8c-d851-4173-900e-84a0b4caa837"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("479691df-0a60-47cb-bcae-5d0704f8beef"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1dacf4bd-8b66-4932-a33d-f33b1775d44e"), "Admin" },
                    { new Guid("ac1d148f-ecc4-4db3-8710-e8d414003687"), "EventOrganizer" },
                    { new Guid("f842d80e-e59c-487b-ae9b-f22ad80760d2"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("e2273ad7-b0d6-4cbc-8ff3-d709c0d7daa6"), "admin@gmail.com", "$2a$10$W8LtePPd44sSAb/Z0SwFru3fxE/bHF0is2kAsksbQ0roVDP1RqG2a", new Guid("1dacf4bd-8b66-4932-a33d-f33b1775d44e"), "$2a$10$W8LtePPd44sSAb/Z0SwFru", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("b0cfda33-36f8-467e-8cdd-0b27fc1c051b"), 0m, new Guid("e2273ad7-b0d6-4cbc-8ff3-d709c0d7daa6") });
        }
    }
}

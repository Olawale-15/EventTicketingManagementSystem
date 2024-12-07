using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Refres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("896115d5-0475-494b-b3bc-88797d7099e9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ce1123e6-ced7-4d13-9010-6b45d120c24a"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("d7d5c479-a466-408d-8f17-8da07c94f53c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7dc46ef2-6ce5-4837-9c7e-c3ac1d022e9d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("044d3ff7-6a83-4a3e-a8f3-c2d6a91be959"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("044d3ff7-6a83-4a3e-a8f3-c2d6a91be959"), "Admin" },
                    { new Guid("896115d5-0475-494b-b3bc-88797d7099e9"), "Attendee" },
                    { new Guid("ce1123e6-ced7-4d13-9010-6b45d120c24a"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("7dc46ef2-6ce5-4837-9c7e-c3ac1d022e9d"), "admin@gmail.com", "$2a$10$5X56fD89QzZRUqjPPLi6cekQL8Z03TSIIjn89si2v2GbdDKDfKjyu", new Guid("044d3ff7-6a83-4a3e-a8f3-c2d6a91be959"), "$2a$10$5X56fD89QzZRUqjPPLi6ce", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("d7d5c479-a466-408d-8f17-8da07c94f53c"), 0m, new Guid("7dc46ef2-6ce5-4837-9c7e-c3ac1d022e9d") });
        }
    }
}

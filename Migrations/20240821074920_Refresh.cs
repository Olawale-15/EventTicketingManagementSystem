using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("931882dc-f51f-4b3f-8ec6-965548b84d53"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac74d3ea-b5b7-4d86-94ea-918aabd4a2fa"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("97b8f9f6-3e79-418e-99b1-cf49547c2f56"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6dcd9b8d-dfab-47fb-91fd-9a216df9349e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("219351e4-540a-4e83-9d8f-95642d49037b"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("219351e4-540a-4e83-9d8f-95642d49037b"), "Admin" },
                    { new Guid("931882dc-f51f-4b3f-8ec6-965548b84d53"), "EventOrganizer" },
                    { new Guid("ac74d3ea-b5b7-4d86-94ea-918aabd4a2fa"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("6dcd9b8d-dfab-47fb-91fd-9a216df9349e"), "admin@gmail.com", "$2a$10$fivrQ7dZGrFGsB1TMO363OUMTm80w.B.si3htM6whEoYOrCQNQdK6", new Guid("219351e4-540a-4e83-9d8f-95642d49037b"), "$2a$10$fivrQ7dZGrFGsB1TMO363O", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("97b8f9f6-3e79-418e-99b1-cf49547c2f56"), 0m, new Guid("6dcd9b8d-dfab-47fb-91fd-9a216df9349e") });
        }
    }
}

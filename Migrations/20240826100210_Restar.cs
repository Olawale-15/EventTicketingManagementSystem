using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Restar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("09364676-2ea6-43b7-a852-37a4ed709b37"), "EventOrganizer" },
                    { new Guid("4b463d0d-bde7-4963-977a-e9b14e43ceb2"), "Attendee" },
                    { new Guid("c1ea0cab-c76a-4c6e-943d-c3ec378777e4"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("68a44a0f-2ad5-4946-a33c-05483656b350"), "admin@gmail.com", "$2a$10$fm0sKbfQOF0FRCPFi18BauxUOAY.EGgZN.Bftdsy3llA.G6cHTlfe", new Guid("c1ea0cab-c76a-4c6e-943d-c3ec378777e4"), "$2a$10$fm0sKbfQOF0FRCPFi18Bau", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("84eaa2c9-cf6d-4937-b410-35072160a75e"), 0m, new Guid("68a44a0f-2ad5-4946-a33c-05483656b350") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("09364676-2ea6-43b7-a852-37a4ed709b37"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b463d0d-bde7-4963-977a-e9b14e43ceb2"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("84eaa2c9-cf6d-4937-b410-35072160a75e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("68a44a0f-2ad5-4946-a33c-05483656b350"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c1ea0cab-c76a-4c6e-943d-c3ec378777e4"));

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
    }
}

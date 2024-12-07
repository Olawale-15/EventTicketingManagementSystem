using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2b916610-815d-4684-9121-31bf6c55de84"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("38434b04-4527-4b0e-9e31-cc5306ad8314"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("b13720c8-431d-4077-af6b-f1bb2a9ed378"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6582a08a-a94f-4f2f-b41b-2ccb0cff30f1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("900741d5-aa63-4f3e-9129-ceb1ef51d03c"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"), "Admin" },
                    { new Guid("d1f2c904-735a-4d7b-a8c6-56e972b86c21"), "EventOrganizer" },
                    { new Guid("f422457d-b47d-4198-bc63-62f474492a99"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a"), "admin@gmail.com", "$2a$10$.K7wMAh4OmbQt7hG1LMhE.PySKoHGWass1i.D7VMg898H1YY.IrOy", new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"), "$2a$10$.K7wMAh4OmbQt7hG1LMhE.", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("6e3123f3-cb17-4f71-956d-e280692e5cce"), 0m, new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1f2c904-735a-4d7b-a8c6-56e972b86c21"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f422457d-b47d-4198-bc63-62f474492a99"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("6e3123f3-cb17-4f71-956d-e280692e5cce"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b916610-815d-4684-9121-31bf6c55de84"), "EventOrganizer" },
                    { new Guid("38434b04-4527-4b0e-9e31-cc5306ad8314"), "Attendee" },
                    { new Guid("900741d5-aa63-4f3e-9129-ceb1ef51d03c"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("6582a08a-a94f-4f2f-b41b-2ccb0cff30f1"), "admin@gmail.com", "$2a$10$WN4WUtK1n2xYUj6n5pgW.OIWLsd0dpy92Ct4Ux68CI7gYUy20EH22", new Guid("900741d5-aa63-4f3e-9129-ceb1ef51d03c"), "$2a$10$WN4WUtK1n2xYUj6n5pgW.O", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("b13720c8-431d-4077-af6b-f1bb2a9ed378"), 0m, new Guid("6582a08a-a94f-4f2f-b41b-2ccb0cff30f1") });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5fd63170-4e0a-47cd-8036-56253655ecac"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bac8cbd3-8433-4f6e-b14e-fab314e5ea65"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("4ba44053-fb79-4aeb-9c81-ea9ca54c3b7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46d6baac-cac5-441a-b010-531343ed1ebd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e9abaac8-a0b1-4b69-80b1-aa85cc7e4e11"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1baa0597-7157-4c77-ad8c-7360e0f71e7d"), "Admin" },
                    { new Guid("6b8600ed-c456-415e-a8f2-77846dccddf3"), "EventOrganizer" },
                    { new Guid("7b27d76a-a420-42e2-b561-e4089c058796"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("146a5282-3772-4385-af9c-2a269192d049"), "admin@gmail.com", "$2a$10$GaXoh85QGXQmlUsyRAi9tORZTrP3YSM./YmA5uLJQJ9sZ4OHnQOJq", new Guid("1baa0597-7157-4c77-ad8c-7360e0f71e7d"), "$2a$10$GaXoh85QGXQmlUsyRAi9tO", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("f6c513e4-690d-4be5-a54e-23f620636900"), 0m, new Guid("146a5282-3772-4385-af9c-2a269192d049") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6b8600ed-c456-415e-a8f2-77846dccddf3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7b27d76a-a420-42e2-b561-e4089c058796"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("f6c513e4-690d-4be5-a54e-23f620636900"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("146a5282-3772-4385-af9c-2a269192d049"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1baa0597-7157-4c77-ad8c-7360e0f71e7d"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5fd63170-4e0a-47cd-8036-56253655ecac"), "EventOrganizer" },
                    { new Guid("bac8cbd3-8433-4f6e-b14e-fab314e5ea65"), "Attendee" },
                    { new Guid("e9abaac8-a0b1-4b69-80b1-aa85cc7e4e11"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("46d6baac-cac5-441a-b010-531343ed1ebd"), "admin@gmail.com", "$2a$10$gqtmW24srBZEFPhiRIa9Q.2LuiaS2Z3SxXF2Ux4tbll.gWdsycQfi", new Guid("e9abaac8-a0b1-4b69-80b1-aa85cc7e4e11"), "$2a$10$gqtmW24srBZEFPhiRIa9Q.", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("4ba44053-fb79-4aeb-9c81-ea9ca54c3b7b"), 0m, new Guid("46d6baac-cac5-441a-b010-531343ed1ebd") });
        }
    }
}

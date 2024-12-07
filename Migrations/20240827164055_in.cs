using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class @in : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("05f27dec-5c62-4266-a475-a75f2b84e33a"), "Admin" },
                    { new Guid("77d6f378-7bf8-4a33-976c-ecb768ab8503"), "EventOrganizer" },
                    { new Guid("bd36a9f9-a6cd-4ab9-890d-65bba21d028f"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("c19fc9cb-d13b-4d0c-a6b2-480609acef99"), "admin@gmail.com", "$2a$10$FEQembmBCqkzEuv0SipnNOVy5.lmLk3EFGwvbMkq5/cSTKIec80Q6", new Guid("05f27dec-5c62-4266-a475-a75f2b84e33a"), "$2a$10$FEQembmBCqkzEuv0SipnNO", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("4446452d-0d9d-464b-8612-83c39bba2722"), 0m, new Guid("c19fc9cb-d13b-4d0c-a6b2-480609acef99") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77d6f378-7bf8-4a33-976c-ecb768ab8503"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd36a9f9-a6cd-4ab9-890d-65bba21d028f"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("4446452d-0d9d-464b-8612-83c39bba2722"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c19fc9cb-d13b-4d0c-a6b2-480609acef99"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("05f27dec-5c62-4266-a475-a75f2b84e33a"));

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
    }
}

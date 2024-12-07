using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class ojki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("5dfe5fa5-e63a-42c4-bf99-5f00f5597ffa"), "EventOrganizer" },
                    { new Guid("74847557-4129-4ea8-8b89-de8d33cb0ff4"), "Attendee" },
                    { new Guid("ee6c83ba-120d-4224-be42-ddd5dd0299bf"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("f10118a2-61b6-480a-8636-0a41400b7b2b"), "admin@gmail.com", "$2a$10$7bH23x8uOUQbbg1wY0Q8BOZZjE8SimY.kMJYHbMKLMz9Kn88x5iHi", new Guid("ee6c83ba-120d-4224-be42-ddd5dd0299bf"), "$2a$10$7bH23x8uOUQbbg1wY0Q8BO", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("3253479c-8e08-403a-84cc-ea4ac4459e4e"), 0m, new Guid("f10118a2-61b6-480a-8636-0a41400b7b2b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5dfe5fa5-e63a-42c4-bf99-5f00f5597ffa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("74847557-4129-4ea8-8b89-de8d33cb0ff4"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("3253479c-8e08-403a-84cc-ea4ac4459e4e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f10118a2-61b6-480a-8636-0a41400b7b2b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ee6c83ba-120d-4224-be42-ddd5dd0299bf"));

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
    }
}

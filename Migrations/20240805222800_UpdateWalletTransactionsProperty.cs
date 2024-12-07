using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletTransactionsProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("61ee26b0-d817-4184-91ac-b60472afcefc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a2c10223-92d0-4ca1-b154-522c5b54ae4e"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("bfe83ea1-8566-4efa-9cf0-fb9160044f36"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4e704daa-0dc3-41d4-a59b-772a7146f27f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e6365101-9781-4169-8e01-3229b3964b0e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("516f2f1e-e619-4c2d-905d-82f844bf0d87"), "Attendee" },
                    { new Guid("72c9f0a4-6e68-4092-bc43-d00ac07ee105"), "Admin" },
                    { new Guid("9366f04c-5621-4da2-99f3-7e7f1a908c45"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("f78a7f38-eb77-4b51-a593-8fc01e3fbfbb"), "admin@gmail.com", "$2a$10$nJYrsvnjWm81DMjKTEXgguB7O6TaLsx8OI5wsd7b3xq5tWvyXJTja", new Guid("72c9f0a4-6e68-4092-bc43-d00ac07ee105"), "$2a$10$nJYrsvnjWm81DMjKTEXggu", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("3937e421-d609-4feb-9bf8-90b281031d85"), 0m, new Guid("f78a7f38-eb77-4b51-a593-8fc01e3fbfbb") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("516f2f1e-e619-4c2d-905d-82f844bf0d87"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9366f04c-5621-4da2-99f3-7e7f1a908c45"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("3937e421-d609-4feb-9bf8-90b281031d85"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f78a7f38-eb77-4b51-a593-8fc01e3fbfbb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("72c9f0a4-6e68-4092-bc43-d00ac07ee105"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("61ee26b0-d817-4184-91ac-b60472afcefc"), "Attendee" },
                    { new Guid("a2c10223-92d0-4ca1-b154-522c5b54ae4e"), "EventOrganizer" },
                    { new Guid("e6365101-9781-4169-8e01-3229b3964b0e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("4e704daa-0dc3-41d4-a59b-772a7146f27f"), "admin@gmail.com", "$2a$10$efleYQyjSjtFc/WIjxCWqOeWNYkDkLaxXOZfFunpvWsIhrkAErh6m", new Guid("e6365101-9781-4169-8e01-3229b3964b0e"), "$2a$10$efleYQyjSjtFc/WIjxCWqO", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("bfe83ea1-8566-4efa-9cf0-fb9160044f36"), 0m, new Guid("4e704daa-0dc3-41d4-a59b-772a7146f27f") });
        }
    }
}

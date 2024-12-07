using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletTransactionsPropert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReference",
                table: "Transactions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Ticket",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Transactions",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Transactions",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Transactions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transactions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("77aedb62-0597-4e93-a813-2182131fa945"), "Attendee" },
                    { new Guid("9939e1f9-bdc5-4a97-89d1-6373aea415a0"), "EventOrganizer" },
                    { new Guid("a163c5d2-9bac-4b4f-b02a-b418cb21ebd2"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("d47cbdf9-e6e6-427a-8e43-34b5570a6fce"), "admin@gmail.com", "$2a$10$lmi.s788HFy0L.QioORx5eIvcr/fNbAzG2XytLpveHmoywKs8Q.6S", new Guid("a163c5d2-9bac-4b4f-b02a-b418cb21ebd2"), "$2a$10$lmi.s788HFy0L.QioORx5e", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("a4d44f91-ef8a-4e9a-8dd3-3ccdf1a689fb"), 0m, new Guid("d47cbdf9-e6e6-427a-8e43-34b5570a6fce") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77aedb62-0597-4e93-a813-2182131fa945"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9939e1f9-bdc5-4a97-89d1-6373aea415a0"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("a4d44f91-ef8a-4e9a-8dd3-3ccdf1a689fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d47cbdf9-e6e6-427a-8e43-34b5570a6fce"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a163c5d2-9bac-4b4f-b02a-b418cb21ebd2"));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReference",
                table: "Transactions",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Ticket",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Transactions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Transactions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Transactions",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}

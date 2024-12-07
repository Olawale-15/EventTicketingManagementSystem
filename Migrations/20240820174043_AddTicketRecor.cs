using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketRecor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d2a5d0e2-86e8-4889-860f-d54b719c647e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("df3f6744-8df7-420e-a19e-5bb10bf0e814"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("6cde22c5-5856-459c-863c-56551f39bf1b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d4bd819-e9d0-4a79-b58c-d51e139217d2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d9220e84-22cd-4276-a294-3598e024d6a7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0069f13b-e0e6-4c22-9af3-d4007c324273"), "EventOrganizer" },
                    { new Guid("2fb8201d-ad75-4e66-879b-2ec087aa6613"), "Admin" },
                    { new Guid("a77ecb0a-d9f8-4a07-8b76-b89e20bf39af"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("a99aee69-89ea-4930-9f51-feaec725b69a"), "admin@gmail.com", "$2a$10$tgpQCciTFsKH0eKA1g/uhufRg33Wylen9Vuqh7zf7vIq.AjvLYRSG", new Guid("2fb8201d-ad75-4e66-879b-2ec087aa6613"), "$2a$10$tgpQCciTFsKH0eKA1g/uhu", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("a3150180-55b6-4ee7-9014-69ea0713e475"), 0m, new Guid("a99aee69-89ea-4930-9f51-feaec725b69a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0069f13b-e0e6-4c22-9af3-d4007c324273"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a77ecb0a-d9f8-4a07-8b76-b89e20bf39af"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("a3150180-55b6-4ee7-9014-69ea0713e475"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a99aee69-89ea-4930-9f51-feaec725b69a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2fb8201d-ad75-4e66-879b-2ec087aa6613"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("d2a5d0e2-86e8-4889-860f-d54b719c647e"), "EventOrganizer" },
                    { new Guid("d9220e84-22cd-4276-a294-3598e024d6a7"), "Admin" },
                    { new Guid("df3f6744-8df7-420e-a19e-5bb10bf0e814"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("6d4bd819-e9d0-4a79-b58c-d51e139217d2"), "admin@gmail.com", "$2a$10$0i9jZ689ged2VuBRhuSTquvWR2oyJpNryREHVLxt2ocERkp8YqSa.", new Guid("d9220e84-22cd-4276-a294-3598e024d6a7"), "$2a$10$0i9jZ689ged2VuBRhuSTqu", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("6cde22c5-5856-459c-863c-56551f39bf1b"), 0m, new Guid("6d4bd819-e9d0-4a79-b58c-d51e139217d2") });
        }
    }
}

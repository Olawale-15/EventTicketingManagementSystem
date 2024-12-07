using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTicketRecords_TicketId",
                table: "AttendeeTicketRecords",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeTicketRecords_Tickets_TicketId",
                table: "AttendeeTicketRecords",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeTicketRecords_Tickets_TicketId",
                table: "AttendeeTicketRecords");

            migrationBuilder.DropIndex(
                name: "IX_AttendeeTicketRecords_TicketId",
                table: "AttendeeTicketRecords");

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
    }
}

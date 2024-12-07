using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Rest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets",
                column: "AttendeeId",
                principalTable: "Attendees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets",
                column: "AttendeeId",
                principalTable: "Attendees",
                principalColumn: "Id");
        }
    }
}

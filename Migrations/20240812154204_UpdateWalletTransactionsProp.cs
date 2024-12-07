using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletTransactionsProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a61ba614-d4b7-423a-9f92-201fe0aea431"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f66956cd-f5ba-4905-8de4-ea979101c0b5"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("8c8db895-a473-4841-9899-5983cb9f0a06"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4cc67aea-3f1c-42a8-87c3-fc7c35f06442"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b91a981d-3a19-42a8-8f8a-f947abef7ea4"));

            migrationBuilder.AlterColumn<string>(
                name: "Venue",
                table: "Events",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Venue",
                table: "Events",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a61ba614-d4b7-423a-9f92-201fe0aea431"), "Attendee" },
                    { new Guid("b91a981d-3a19-42a8-8f8a-f947abef7ea4"), "Admin" },
                    { new Guid("f66956cd-f5ba-4905-8de4-ea979101c0b5"), "EventOrganizer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("4cc67aea-3f1c-42a8-87c3-fc7c35f06442"), "admin@gmail.com", "$2a$10$UOuScJnjf7.imzg4m7BwBuj2fH.g.eIONbVNO.nHLAmPVGxItApJe", new Guid("b91a981d-3a19-42a8-8f8a-f947abef7ea4"), "$2a$10$UOuScJnjf7.imzg4m7BwBu", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("8c8db895-a473-4841-9899-5983cb9f0a06"), 0m, new Guid("4cc67aea-3f1c-42a8-87c3-fc7c35f06442") });
        }
    }
}

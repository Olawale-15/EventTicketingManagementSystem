using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletTransactionsProper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

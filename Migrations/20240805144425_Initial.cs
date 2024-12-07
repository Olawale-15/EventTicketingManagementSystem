using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                keyValue: new Guid("591743c6-53f3-43df-85be-cee239001d45"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6035035c-81e2-4367-beaa-61ac8979015b"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("7127b573-2375-4eda-a82c-a05ef707431a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendeeId",
                table: "Tickets",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets",
                column: "AttendeeId",
                principalTable: "Attendees",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendeeId",
                table: "Tickets",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("591743c6-53f3-43df-85be-cee239001d45"), "EventOrganizer" },
                    { new Guid("6035035c-81e2-4367-beaa-61ac8979015b"), "Attendee" },
                    { new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38"), "admin@gmail.com", "$2a$10$k7NDCRNIZTWgDs8/PNux3OzbYKm0TywSMAGkazDQtU1ONA.lHgdx2", new Guid("e372d93e-16b6-430c-80cd-8d0b772d6753"), "$2a$10$k7NDCRNIZTWgDs8/PNux3O", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("7127b573-2375-4eda-a82c-a05ef707431a"), 0m, new Guid("cbf1ac46-724e-45d4-ace8-55c0e42d4c38") });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Attendees_AttendeeId",
                table: "Tickets",
                column: "AttendeeId",
                principalTable: "Attendees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

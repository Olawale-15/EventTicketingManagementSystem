using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1f2c904-735a-4d7b-a8c6-56e972b86c21"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f422457d-b47d-4198-bc63-62f474492a99"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("6e3123f3-cb17-4f71-956d-e280692e5cce"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"));

            migrationBuilder.CreateTable(
                name: "AttendeeTicketRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AttendeeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TicketId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TicketCount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeTicketRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendeeTicketRecords_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendeeTicketRecords_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTicketRecords_AttendeeId",
                table: "AttendeeTicketRecords",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTicketRecords_EventId",
                table: "AttendeeTicketRecords",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendeeTicketRecords");

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
                    { new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"), "Admin" },
                    { new Guid("d1f2c904-735a-4d7b-a8c6-56e972b86c21"), "EventOrganizer" },
                    { new Guid("f422457d-b47d-4198-bc63-62f474492a99"), "Attendee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Salt", "UserName" },
                values: new object[] { new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a"), "admin@gmail.com", "$2a$10$.K7wMAh4OmbQt7hG1LMhE.PySKoHGWass1i.D7VMg898H1YY.IrOy", new Guid("3e1c5c73-6520-4e8d-a11e-49a607a30017"), "$2a$10$.K7wMAh4OmbQt7hG1LMhE.", "CEO" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[] { new Guid("6e3123f3-cb17-4f71-956d-e280692e5cce"), 0m, new Guid("32996ecd-dc79-46d1-a5e0-b1f78fdc3c8a") });
        }
    }
}

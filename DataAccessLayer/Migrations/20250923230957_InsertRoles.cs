using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InsertRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "727846b0-e54f-4b51-838f-0d20be6cbe28");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7A903292-EE2C-448F-8F65-360C0B47262D", "DA95C9AF-5BC9-4EB6-A0CF-55C7280D9A24", "Admin", "ADMIN" },
                    { "B3F4CA26-0C00-41D3-B595-C3F6CEC3F4C1", "09E5C58F-F81B-45CC-A9F9-53AF119614F9", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "114f5b27-bb41-4f69-a0ef-58a949b036db", 0, "591c16f7-bde7-4c73-9a29-cb384cb89646", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEPHfwI9gMMv4ztJPZyLbqZpzZQb88ybszn0uDESFQjFRyztC2KP71AHHvuCuDIYWlA==", "5537531375", false, "e20c28e6-b765-40d4-8216-d602df20ec8c", "[]", false, "adminBerke" });

            migrationBuilder.InsertData(
                table: "PropertyStatuses",
                columns: new[] { "Id", "Name", "RgbColorForStatistics" },
                values: new object[] { 3, "Daily Rent", "rgba(76, 185, 231, .5)" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7A903292-EE2C-448F-8F65-360C0B47262D");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B3F4CA26-0C00-41D3-B595-C3F6CEC3F4C1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "114f5b27-bb41-4f69-a0ef-58a949b036db");

            migrationBuilder.DeleteData(
                table: "PropertyStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "727846b0-e54f-4b51-838f-0d20be6cbe28", 0, "7eb35cbb-e9fb-4a42-b74b-8b000411f39e", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEDWdf2SpP2O/n+JpCLmgQApVJdNeFirZ++WUXAuMYnaHpsoI7moRLY3l1ZySYQFsHQ==", "5537531375", false, "4a53e751-815e-42fc-902a-0ea5286f0a4b", "[]", false, "adminBerke" });
        }
    }
}

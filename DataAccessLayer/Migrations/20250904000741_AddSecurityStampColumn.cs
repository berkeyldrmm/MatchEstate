using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityStampColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb7833de-220c-4376-b31c-8810bb68ab3c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "727846b0-e54f-4b51-838f-0d20be6cbe28", 0, "7eb35cbb-e9fb-4a42-b74b-8b000411f39e", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEDWdf2SpP2O/n+JpCLmgQApVJdNeFirZ++WUXAuMYnaHpsoI7moRLY3l1ZySYQFsHQ==", "5537531375", false, "4a53e751-815e-42fc-902a-0ea5286f0a4b", "[]", false, "adminBerke" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "727846b0-e54f-4b51-838f-0d20be6cbe28");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bb7833de-220c-4376-b31c-8810bb68ab3c", 0, "54c942fb-ab7d-4290-931e-bb2eb0ddbfcd", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEExXlPReVU44e7h/VYTOLLRx680Mjpy6S4hx2ZvtmFfU85maW3XtW3Mi5+PsEEPZiQ==", "5537531375", false, null, "[]", false, "adminBerke" });
        }
    }
}

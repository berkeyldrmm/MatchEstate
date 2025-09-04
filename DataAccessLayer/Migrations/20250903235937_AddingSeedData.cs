using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3037972-9ce1-4ff7-a564-62faaad7d64e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bb7833de-220c-4376-b31c-8810bb68ab3c", 0, "54c942fb-ab7d-4290-931e-bb2eb0ddbfcd", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEExXlPReVU44e7h/VYTOLLRx680Mjpy6S4hx2ZvtmFfU85maW3XtW3Mi5+PsEEPZiQ==", "5537531375", false, null, "[]", false, "adminBerke" });

            migrationBuilder.InsertData(
                table: "PropertyStatuses",
                columns: new[] { "Id", "Name", "RgbColorForStatistics" },
                values: new object[,]
                {
                    { 1, "For Sale", "rgba(76, 185, 231, .7)" },
                    { 2, "For Rent", "rgba(76, 185, 231, .5)" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "PropertyName", "RgbColorForStatistics" },
                values: new object[,]
                {
                    { 1, "Shop", "rgba(76, 185, 231, .9)" },
                    { 2, "Land", "rgba(76, 185, 231, .7)" },
                    { 3, "Commercial Unit", "rgba(76, 185, 231, .5)" },
                    { 4, "Apartment", "rgba(76, 185, 231, .3)" },
                    { 5, "Farmland", "rgba(76, 185, 231, .1)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb7833de-220c-4376-b31c-8810bb68ab3c");

            migrationBuilder.DeleteData(
                table: "PropertyStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a3037972-9ce1-4ff7-a564-62faaad7d64e", 0, "9296c0b2-1ff6-4c8d-b9bd-3a4e182eb720", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEDIFDnrKUNWSiq4TrPwrlWdwjEAedYJvwg1g45yw5BEl0z0NjmyKWk2XcNhEgZZeTw==", "5537531375", false, null, "[]", false, "adminBerke" });
        }
    }
}

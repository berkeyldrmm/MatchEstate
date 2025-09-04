using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0c46f987-fb54-4602-8b77-6bdf2329592e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a3037972-9ce1-4ff7-a564-62faaad7d64e", 0, "9296c0b2-1ff6-4c8d-b9bd-3a4e182eb720", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEDIFDnrKUNWSiq4TrPwrlWdwjEAedYJvwg1g45yw5BEl0z0NjmyKWk2XcNhEgZZeTw==", "5537531375", false, null, "[]", false, "adminBerke" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3037972-9ce1-4ff7-a564-62faaad7d64e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0c46f987-fb54-4602-8b77-6bdf2329592e", 0, "7c0603e0-d3d3-49a7-a0de-002c2ee7f94b", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "K+cthYmIUWF6dOIZkdMbZXT9S5p//xxYgrjHOoF64jg=", "5537531375", false, null, "[]", false, "adminBerke" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class setUserTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3F80B602-7C9B-476D-A1B4-8746974528D9", "AQAAAAIAAYagAAAAEPwZZFryFv96B2o0ftzaEM8gLstcedFD1pQZD5GUDi/LcS+v4Ejpk0In1C61ox+/Xg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3a2210b-a8d1-4daf-9710-21dde0e41f0b", "AQAAAAIAAYagAAAAEDPmczseYDDJQiCsHtc7wMoWU8M3t4/60vcs5adiKnaI5lMi+4NIkGikVyIbkL/Cpg==" });
        }
    }
}

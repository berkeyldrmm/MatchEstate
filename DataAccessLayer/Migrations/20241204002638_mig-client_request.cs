using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migclient_request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequests_Clients_ClientId",
                table: "PropertyRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequests_Clients_ClientId",
                table: "PropertyRequests",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequests_Clients_ClientId",
                table: "PropertyRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequests_Clients_ClientId",
                table: "PropertyRequests",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

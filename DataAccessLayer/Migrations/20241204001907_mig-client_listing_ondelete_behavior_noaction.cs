using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migclient_listing_ondelete_behavior_noaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListings_Clients_ClientId",
                table: "PropertyListings");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListings_Clients_ClientId",
                table: "PropertyListings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListings_Clients_ClientId",
                table: "PropertyListings");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListings_Clients_ClientId",
                table: "PropertyListings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertyListings",
                newName: "DealStatus");

            migrationBuilder.RenameColumn(
                name: "SoldDate",
                table: "PropertyListings",
                newName: "DealDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DealStatus",
                table: "PropertyListings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DealDate",
                table: "PropertyListings",
                newName: "SoldDate");
        }
    }
}

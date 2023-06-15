using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary_BidStamp.Migrations
{
    /// <inheritdoc />
    public partial class ColumnDatatypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StampId",
                table: "Bids",
                newName: "ListingId");

            migrationBuilder.AlterColumn<int>(
                name: "BidAmount",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "Bids",
                newName: "StampId");

            migrationBuilder.AlterColumn<decimal>(
                name: "BidAmount",
                table: "Bids",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

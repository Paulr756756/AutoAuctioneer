using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary_BidStamp.Migrations
{
    /// <inheritdoc />
    public partial class Reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Bids",
                 columns: table => new
                 {
                     BidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     StampId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     BidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                     BidTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Bids", x => x.BidId);
                 });

             migrationBuilder.CreateTable(
                name: "Stamps",
                columns: table => new
                 {
                     StampId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     StampTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     CatalogNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     StartingBid = table.Column<int>(type: "int", nullable: true),
                     EndingBid = table.Column<int>(type: "int", nullable: true),
                     StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                     EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Stamps", x => x.StampId);
                 });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

             migrationBuilder.CreateTable(
                 name: "WatchLists",
                 columns: table => new
                 {
                     WatchlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     StampId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_WatchLists", x => x.WatchlistId);
                 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Stamps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WatchLists");
        }
    }
}

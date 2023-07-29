using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer_AutoAuctioneer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    VerificationToken = table.Column<string>(type: "text", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CarParts",
                columns: table => new
                {
                    CarpartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MarketPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartType = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    EngineType = table.Column<string>(type: "text", nullable: true),
                    Displacement = table.Column<double>(type: "double precision", nullable: true),
                    Horsepower = table.Column<int>(type: "integer", nullable: true),
                    Torque = table.Column<int>(type: "integer", nullable: true),
                    CarMake = table.Column<string>(type: "text", nullable: true),
                    CarModel = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParts", x => x.CarpartId);
                    table.ForeignKey(
                        name: "FK_CarParts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    VIN = table.Column<string>(type: "text", nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    Transmission = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    BodyStyle = table.Column<string>(type: "text", nullable: true),
                    ExteriorFeatures = table.Column<List<string>>(type: "text[]", nullable: true),
                    InteriorColor = table.Column<string>(type: "text", nullable: true),
                    SeatingCapacity = table.Column<int>(type: "integer", nullable: false),
                    InteriorFeatures = table.Column<List<string>>(type: "text[]", nullable: true),
                    EngineType = table.Column<string>(type: "text", nullable: true),
                    EngineDisplacement = table.Column<double>(type: "double precision", nullable: true),
                    Horsepower = table.Column<int>(type: "integer", nullable: true),
                    Torque = table.Column<int>(type: "integer", nullable: true),
                    FuelEfficiency = table.Column<double>(type: "double precision", nullable: true),
                    AftermarketUpgrades = table.Column<List<string>>(type: "text[]", nullable: true),
                    WheelsAndTires = table.Column<string>(type: "text", nullable: true),
                    Suspension = table.Column<string>(type: "text", nullable: true),
                    AudioAndEntertainment = table.Column<List<string>>(type: "text[]", nullable: true),
                    NumberOfPreviousOwners = table.Column<int>(type: "integer", nullable: true),
                    OwnershipHistory = table.Column<List<string>>(type: "text[]", nullable: true),
                    HasAccidentHistory = table.Column<bool>(type: "boolean", nullable: false),
                    ServiceRecords = table.Column<List<string>>(type: "text[]", nullable: true),
                    SafetyFeatures = table.Column<List<string>>(type: "text[]", nullable: true),
                    TechnologyFeatures = table.Column<List<string>>(type: "text[]", nullable: true),
                    ImageUrls = table.Column<List<string>>(type: "text[]", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarPartId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listings_CarParts_CarPartId",
                        column: x => x.CarPartId,
                        principalTable: "CarParts",
                        principalColumn: "CarpartId");
                    table.ForeignKey(
                        name: "FK_Listings_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId");
                    table.ForeignKey(
                        name: "FK_Listings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BidAmount = table.Column<int>(type: "integer", nullable: false),
                    BidTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
                    table.ForeignKey(
                        name: "FK_Bids_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "ListingId");
                    table.ForeignKey(
                        name: "FK_Bids_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ListingId",
                table: "Bids",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserId",
                table: "Bids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_UserId",
                table: "CarParts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarId",
                table: "Listings",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarPartId",
                table: "Listings",
                column: "CarPartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserId",
                table: "Listings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "CarParts");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

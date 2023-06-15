using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary_BidStamp.Migrations
{
    /// <inheritdoc />
    public partial class StampListingListingUserandUserStampRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Stamps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_UserId",
                table: "Stamps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_StampId",
                table: "Listings",
                column: "StampId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserId",
                table: "Listings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Stamps_StampId",
                table: "Listings",
                column: "StampId",
                principalTable: "Stamps",
                principalColumn: "StampId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stamps_Users_UserId",
                table: "Stamps",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Stamps_StampId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Stamps_Users_UserId",
                table: "Stamps");

            migrationBuilder.DropIndex(
                name: "IX_Stamps_UserId",
                table: "Stamps");

            migrationBuilder.DropIndex(
                name: "IX_Listings_StampId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_UserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stamps");
        }
    }
}

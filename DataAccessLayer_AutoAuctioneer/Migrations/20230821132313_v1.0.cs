using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer_AutoAuctioneer.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings");

            migrationBuilder.AddColumn<Guid>(
                name: "CarpartId1",
                table: "CarParts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Engine_CarpartId1",
                table: "CarParts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IndividualCarPart_CarpartId1",
                table: "CarParts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_CarpartId1",
                table: "CarParts",
                column: "CarpartId1");

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_Engine_CarpartId1",
                table: "CarParts",
                column: "Engine_CarpartId1");

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_IndividualCarPart_CarpartId1",
                table: "CarParts",
                column: "IndividualCarPart_CarpartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_CarParts_CarpartId1",
                table: "CarParts",
                column: "CarpartId1",
                principalTable: "CarParts",
                principalColumn: "CarpartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_CarParts_Engine_CarpartId1",
                table: "CarParts",
                column: "Engine_CarpartId1",
                principalTable: "CarParts",
                principalColumn: "CarpartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_CarParts_IndividualCarPart_CarpartId1",
                table: "CarParts",
                column: "IndividualCarPart_CarpartId1",
                principalTable: "CarParts",
                principalColumn: "CarpartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_CarParts_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_CarParts_Engine_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_CarParts_IndividualCarPart_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_CarParts_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropIndex(
                name: "IX_CarParts_Engine_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropIndex(
                name: "IX_CarParts_IndividualCarPart_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "CarpartId1",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "Engine_CarpartId1",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "IndividualCarPart_CarpartId1",
                table: "CarParts");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

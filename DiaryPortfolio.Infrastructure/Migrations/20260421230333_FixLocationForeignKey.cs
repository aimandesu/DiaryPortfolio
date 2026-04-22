using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixLocationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Locations_LocationModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_LocationModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "LocationModelId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_LocationId",
                table: "Medias",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Locations_LocationId",
                table: "Medias",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Locations_LocationId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_LocationId",
                table: "Medias");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_LocationModelId",
                table: "Medias",
                column: "LocationModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Locations_LocationModelId",
                table: "Medias",
                column: "LocationModelId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}

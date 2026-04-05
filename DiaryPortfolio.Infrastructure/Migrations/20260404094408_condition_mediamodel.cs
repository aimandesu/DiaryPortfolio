using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class condition_mediamodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Selections_SelectionMediaStatusModelId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Selections_SelectionMediaTypeModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_SelectionMediaStatusModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_SelectionMediaTypeModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "SelectionMediaStatusModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "SelectionMediaTypeModelId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MediaStatusSelectionId",
                table: "Medias",
                column: "MediaStatusSelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MediaTypeSelectionId",
                table: "Medias",
                column: "MediaTypeSelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Selections_MediaStatusSelectionId",
                table: "Medias",
                column: "MediaStatusSelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Selections_MediaTypeSelectionId",
                table: "Medias",
                column: "MediaTypeSelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Selections_MediaStatusSelectionId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Selections_MediaTypeSelectionId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_MediaStatusSelectionId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_MediaTypeSelectionId",
                table: "Medias");

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionMediaStatusModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionMediaTypeModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_SelectionMediaStatusModelId",
                table: "Medias",
                column: "SelectionMediaStatusModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_SelectionMediaTypeModelId",
                table: "Medias",
                column: "SelectionMediaTypeModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Selections_SelectionMediaStatusModelId",
                table: "Medias",
                column: "SelectionMediaStatusModelId",
                principalTable: "Selections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Selections_SelectionMediaTypeModelId",
                table: "Medias",
                column: "SelectionMediaTypeModelId",
                principalTable: "Selections",
                principalColumn: "Id");
        }
    }
}

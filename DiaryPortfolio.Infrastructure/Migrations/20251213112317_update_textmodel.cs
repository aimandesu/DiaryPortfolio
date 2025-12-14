using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_textmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_TextStyle_TextModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TextModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "TextModelId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TextId",
                table: "Medias",
                column: "TextId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias",
                column: "TextId",
                principalTable: "TextStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TextId",
                table: "Medias");

            migrationBuilder.AddColumn<Guid>(
                name: "TextModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TextModelId",
                table: "Medias",
                column: "TextModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_TextStyle_TextModelId",
                table: "Medias",
                column: "TextModelId",
                principalTable: "TextStyle",
                principalColumn: "Id");
        }
    }
}

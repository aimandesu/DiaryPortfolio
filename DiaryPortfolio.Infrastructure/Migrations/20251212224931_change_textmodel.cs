using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_textmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextStyle_Medias_MediaId",
                table: "TextStyle");

            migrationBuilder.DropIndex(
                name: "IX_TextStyle_MediaId",
                table: "TextStyle");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "TextStyle");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "TextStyle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TextStyle_MediaId",
                table: "TextStyle",
                column: "MediaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextStyle_Medias_MediaId",
                table: "TextStyle",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

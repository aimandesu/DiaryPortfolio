using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addConditionForTextStyleandSpaceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Spaces_SpaceModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_SpaceModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "SpaceModelId",
                table: "Medias");

            migrationBuilder.AlterColumn<string>(
                name: "TextStyle",
                table: "TextStyle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_SpaceId",
                table: "Medias",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Spaces_SpaceId",
                table: "Medias",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Spaces_SpaceId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_SpaceId",
                table: "Medias");

            migrationBuilder.AlterColumn<int>(
                name: "TextStyle",
                table: "TextStyle",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "SpaceModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_SpaceModelId",
                table: "Medias",
                column: "SpaceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Spaces_SpaceModelId",
                table: "Medias",
                column: "SpaceModelId",
                principalTable: "Spaces",
                principalColumn: "Id");
        }
    }
}

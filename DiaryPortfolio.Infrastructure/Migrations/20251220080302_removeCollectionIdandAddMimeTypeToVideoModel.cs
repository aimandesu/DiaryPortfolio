using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeCollectionIdandAddMimeTypeToVideoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Collections_CollectionId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_CollectionId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Medias");

            migrationBuilder.AddColumn<string>(
                name: "Mime",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionModelId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_CollectionModelId",
                table: "Medias",
                column: "CollectionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Collections_CollectionModelId",
                table: "Medias",
                column: "CollectionModelId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Collections_CollectionModelId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_CollectionModelId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Mime",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CollectionModelId",
                table: "Medias");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Medias_CollectionId",
                table: "Medias",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Collections_CollectionId",
                table: "Medias",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

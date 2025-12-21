using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCollectionIdIntoMediaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Collections_CollectionModelId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Medias_MediaModelId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Medias_MediaModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_MediaModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MediaModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MediaModelId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "MediaModelId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "CollectionModelId",
                table: "Medias",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_CollectionModelId",
                table: "Medias",
                newName: "IX_Medias_CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MediaId",
                table: "Videos",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MediaId",
                table: "Photos",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Collections_CollectionId",
                table: "Medias",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Medias_MediaId",
                table: "Photos",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Medias_MediaId",
                table: "Videos",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Collections_CollectionId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Medias_MediaId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Medias_MediaId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_MediaId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MediaId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Medias",
                newName: "CollectionModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_CollectionId",
                table: "Medias",
                newName: "IX_Medias_CollectionModelId");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaModelId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MediaModelId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MediaModelId",
                table: "Videos",
                column: "MediaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MediaModelId",
                table: "Photos",
                column: "MediaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Collections_CollectionModelId",
                table: "Medias",
                column: "CollectionModelId",
                principalTable: "Collections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Medias_MediaModelId",
                table: "Photos",
                column: "MediaModelId",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Medias_MediaModelId",
                table: "Videos",
                column: "MediaModelId",
                principalTable: "Medias",
                principalColumn: "Id");
        }
    }
}

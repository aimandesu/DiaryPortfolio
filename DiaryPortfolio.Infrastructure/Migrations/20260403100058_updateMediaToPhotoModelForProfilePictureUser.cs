using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateMediaToPhotoModelForProfilePictureUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Medias_ProfileMediaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ProfileMediaId",
                table: "AspNetUsers",
                newName: "ProfilePhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ProfileMediaId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ProfilePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ProfilePhotoId",
                table: "AspNetUsers",
                newName: "ProfileMediaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ProfileMediaId");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Medias_ProfileMediaId",
                table: "AspNetUsers",
                column: "ProfileMediaId",
                principalTable: "Medias",
                principalColumn: "Id");
        }
    }
}

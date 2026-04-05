using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_each_table_itsfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomUrls_AspNetUsers_UserId",
                table: "CustomUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_UserId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_AspNetUsers_UserId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Spaces",
                newName: "DiaryProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Spaces_UserId",
                table: "Spaces",
                newName: "IX_Spaces_DiaryProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Skills",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_UserId",
                table: "Skills",
                newName: "IX_Skills_PortfolioProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                newName: "IX_Projects_PortfolioProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Experiences",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                newName: "IX_Experiences_PortfolioProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Educations",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                newName: "IX_Educations_PortfolioProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CustomUrls",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomUrls_UserId",
                table: "CustomUrls",
                newName: "IX_CustomUrls_PortfolioProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUrls_PortfolioProfile_PortfolioProfileId",
                table: "CustomUrls",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_PortfolioProfile_PortfolioProfileId",
                table: "Educations",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_PortfolioProfile_PortfolioProfileId",
                table: "Experiences",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_PortfolioProfile_PortfolioProfileId",
                table: "Projects",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_PortfolioProfile_PortfolioProfileId",
                table: "Skills",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileId",
                table: "Spaces",
                column: "DiaryProfileId",
                principalTable: "DiaryProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomUrls_PortfolioProfile_PortfolioProfileId",
                table: "CustomUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_PortfolioProfile_PortfolioProfileId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_PortfolioProfile_PortfolioProfileId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_PortfolioProfile_PortfolioProfileId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_PortfolioProfile_PortfolioProfileId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileId",
                table: "Spaces");

            migrationBuilder.RenameColumn(
                name: "DiaryProfileId",
                table: "Spaces",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Spaces_DiaryProfileId",
                table: "Spaces",
                newName: "IX_Spaces_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "Skills",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_PortfolioProfileId",
                table: "Skills",
                newName: "IX_Skills_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_PortfolioProfileId",
                table: "Projects",
                newName: "IX_Projects_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "Experiences",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_PortfolioProfileId",
                table: "Experiences",
                newName: "IX_Experiences_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "Educations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_PortfolioProfileId",
                table: "Educations",
                newName: "IX_Educations_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "CustomUrls",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomUrls_PortfolioProfileId",
                table: "CustomUrls",
                newName: "IX_CustomUrls_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "DiaryProfileUserId",
                table: "Spaces",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DiaryProfileUserId",
                table: "Spaces",
                column: "DiaryProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUrls_AspNetUsers_UserId",
                table: "CustomUrls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_UserId",
                table: "Skills",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_AspNetUsers_UserId",
                table: "Spaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileUserId",
                table: "Spaces",
                column: "DiaryProfileUserId",
                principalTable: "DiaryProfile",
                principalColumn: "UserId");
        }
    }
}

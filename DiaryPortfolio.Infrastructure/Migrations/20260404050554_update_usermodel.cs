using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_usermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioProfile",
                table: "PortfolioProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryProfile",
                table: "DiaryProfile");

            migrationBuilder.DropColumn(
                name: "DiaryProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PortfolioProfileId",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioProfile",
                table: "PortfolioProfile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryProfile",
                table: "DiaryProfile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_UserId",
                table: "PortfolioProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryProfile_UserId",
                table: "DiaryProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUrls_PortfolioProfile_PortfolioProfileId",
                table: "CustomUrls",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_PortfolioProfile_PortfolioProfileId",
                table: "Educations",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_PortfolioProfile_PortfolioProfileId",
                table: "Experiences",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_PortfolioProfile_PortfolioProfileId",
                table: "Projects",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_PortfolioProfile_PortfolioProfileId",
                table: "Skills",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileId",
                table: "Spaces",
                column: "DiaryProfileId",
                principalTable: "DiaryProfile",
                principalColumn: "Id",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioProfile",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_UserId",
                table: "PortfolioProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryProfile",
                table: "DiaryProfile");

            migrationBuilder.DropIndex(
                name: "IX_DiaryProfile_UserId",
                table: "DiaryProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "DiaryProfileId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioProfileId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioProfile",
                table: "PortfolioProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryProfile",
                table: "DiaryProfile",
                column: "UserId");

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
    }
}

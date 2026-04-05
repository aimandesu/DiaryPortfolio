using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExplicitRelationshipConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Files_ResumeFileId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeFileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_LocationId",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_ProfilePhotoId",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile");

            migrationBuilder.DropColumn(
                name: "ResumeFileId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Resume");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_FileId",
                table: "Resume",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume",
                column: "TemplateId",
                unique: true,
                filter: "[TemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_LocationId",
                table: "PortfolioProfile",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_ProfilePhotoId",
                table: "PortfolioProfile",
                column: "ProfilePhotoId",
                unique: true,
                filter: "[ProfilePhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile",
                column: "ResumeId",
                unique: true,
                filter: "[ResumeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Files_FileId",
                table: "Resume",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_TemplateId",
                table: "Resume",
                column: "TemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Files_FileId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_TemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_FileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_LocationId",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_ProfilePhotoId",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeFileId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeTemplateId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeFileId",
                table: "Resume",
                column: "ResumeFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_LocationId",
                table: "PortfolioProfile",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_ProfilePhotoId",
                table: "PortfolioProfile",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Files_ResumeFileId",
                table: "Resume",
                column: "ResumeFileId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id");
        }
    }
}

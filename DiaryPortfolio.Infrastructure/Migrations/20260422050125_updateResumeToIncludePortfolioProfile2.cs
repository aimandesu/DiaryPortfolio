using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateResumeToIncludePortfolioProfile2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioProfile_Resume_ResumeId",
                table: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "PortfolioProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioProfileId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Resume_PortfolioProfileId",
                table: "Resume",
                column: "PortfolioProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_PortfolioProfile_PortfolioProfileId",
                table: "Resume",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_PortfolioProfile_PortfolioProfileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_PortfolioProfileId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PortfolioProfileId",
                table: "Resume");

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeId",
                table: "PortfolioProfile",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProfile_ResumeId",
                table: "PortfolioProfile",
                column: "ResumeId",
                unique: true,
                filter: "[ResumeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioProfile_Resume_ResumeId",
                table: "PortfolioProfile",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }
    }
}

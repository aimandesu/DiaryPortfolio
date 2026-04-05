using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seperate_profile_for_services : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Locations_LocationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Resume_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "AspNetUsers",
                newName: "PortfolioProfileId");

            migrationBuilder.RenameColumn(
                name: "ProfilePhotoId",
                table: "AspNetUsers",
                newName: "DiaryProfileId");

            migrationBuilder.AddColumn<Guid>(
                name: "DiaryProfileUserId",
                table: "Spaces",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiaryProfile",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DiaryProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioProfile",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResumeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfilePhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PortfolioProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioProfile_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PortfolioProfile_Photos_ProfilePhotoId",
                        column: x => x.ProfilePhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PortfolioProfile_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DiaryProfileUserId",
                table: "Spaces",
                column: "DiaryProfileUserId");

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
                name: "FK_Spaces_DiaryProfile_DiaryProfileUserId",
                table: "Spaces",
                column: "DiaryProfileUserId",
                principalTable: "DiaryProfile",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_DiaryProfile_DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "DiaryProfile");

            migrationBuilder.DropTable(
                name: "PortfolioProfile");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "DiaryProfileUserId",
                table: "Spaces");

            migrationBuilder.RenameColumn(
                name: "PortfolioProfileId",
                table: "AspNetUsers",
                newName: "ResumeId");

            migrationBuilder.RenameColumn(
                name: "DiaryProfileId",
                table: "AspNetUsers",
                newName: "ProfilePhotoId");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Locations_LocationId",
                table: "AspNetUsers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Resume_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }
    }
}

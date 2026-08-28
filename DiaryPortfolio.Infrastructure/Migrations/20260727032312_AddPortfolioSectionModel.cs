using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioSectionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    PortfolioProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioSections_PortfolioProfile_PortfolioProfileId",
                        column: x => x.PortfolioProfileId,
                        principalTable: "PortfolioProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioSections_PortfolioProfileId",
                table: "PortfolioSections",
                column: "PortfolioProfileId");

            // Backfill: every profile created before this migration gets the
            // same six sections, visible, in the order the page rendered them
            // before layout became configurable - so nothing changes on deploy.
            migrationBuilder.Sql(@"
                INSERT INTO PortfolioSections (Id, PortfolioProfileId, SectionType, [Order], IsVisible)
                SELECT NEWID(), Id, 'about', 0, 1 FROM PortfolioProfile
                UNION ALL
                SELECT NEWID(), Id, 'skills', 1, 1 FROM PortfolioProfile
                UNION ALL
                SELECT NEWID(), Id, 'education', 2, 1 FROM PortfolioProfile
                UNION ALL
                SELECT NEWID(), Id, 'experience', 3, 1 FROM PortfolioProfile
                UNION ALL
                SELECT NEWID(), Id, 'projects', 4, 1 FROM PortfolioProfile
                UNION ALL
                SELECT NEWID(), Id, 'resume', 5, 1 FROM PortfolioProfile;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioSections");
        }
    }
}

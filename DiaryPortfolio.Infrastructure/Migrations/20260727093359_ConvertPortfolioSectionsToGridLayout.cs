using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertPortfolioSectionsToGridLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "PortfolioSections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "PortfolioSections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "W",
                table: "PortfolioSections",
                type: "int",
                nullable: false,
                defaultValue: 12);

            migrationBuilder.AddColumn<int>(
                name: "H",
                table: "PortfolioSections",
                type: "int",
                nullable: false,
                defaultValue: 4);

            // Every section that was visible under the old tab/stack model
            // becomes a full-width block, stacked in its old top-to-bottom
            // order, on a 12-column grid. Sections that were hidden are left
            // unplaced (X/Y null) - they land in the palette instead of on
            // the page, which is exactly what "hidden" meant before.
            migrationBuilder.Sql(@"
                UPDATE PortfolioSections
                SET X = 0, Y = [Order] * 4, W = 12, H = 4
                WHERE IsVisible = 1;
            ");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "PortfolioSections");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "PortfolioSections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PortfolioSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "PortfolioSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"
                UPDATE PortfolioSections
                SET [Order] = ISNULL(Y, 0) / 4, IsVisible = CASE WHEN X IS NULL THEN 0 ELSE 1 END;
            ");

            migrationBuilder.DropColumn(
                name: "X",
                table: "PortfolioSections");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "PortfolioSections");

            migrationBuilder.DropColumn(
                name: "W",
                table: "PortfolioSections");

            migrationBuilder.DropColumn(
                name: "H",
                table: "PortfolioSections");
        }
    }
}

using System;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.SaveLayout
{
    public sealed record class SectionPlacementInput(
        Guid SectionId,
        int X,
        int Y,
        int W,
        int H
    );
}

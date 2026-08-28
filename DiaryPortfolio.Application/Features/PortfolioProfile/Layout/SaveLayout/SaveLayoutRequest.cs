using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System.Collections.Generic;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.SaveLayout
{
    public sealed record class SaveLayoutRequest(
        List<SectionPlacementInput> Placements
    ) : IRequest<ResultResponse<List<PortfolioSectionModelDto>>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

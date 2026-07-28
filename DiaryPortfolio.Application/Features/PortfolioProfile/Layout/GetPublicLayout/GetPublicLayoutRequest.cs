using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using Mediator;
using System.Collections.Generic;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetPublicLayout
{
    public sealed record class GetPublicLayoutRequest(
        string Username
    ) : IRequest<ResultResponse<List<PortfolioSectionModelDto>>>;
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System.Collections.Generic;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetMyLayout
{
    public sealed record class GetMyLayoutRequest() : IRequest<ResultResponse<List<PortfolioSectionModelDto>>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

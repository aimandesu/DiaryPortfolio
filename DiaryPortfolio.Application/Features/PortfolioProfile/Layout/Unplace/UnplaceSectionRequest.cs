using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.Unplace
{
    [RequireOwnership(typeof(PortfolioSectionModel))]
    public sealed record class UnplaceSectionRequest(
        string Id
    ) : IRequest<ResultResponse<PortfolioSectionModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

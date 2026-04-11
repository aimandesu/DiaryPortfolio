using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.GetAll
{
    public sealed record class GetAllExperienceRequest(
        string UserId) : IRequest<ResultResponse<List<ExperienceModelDto>>>;
}

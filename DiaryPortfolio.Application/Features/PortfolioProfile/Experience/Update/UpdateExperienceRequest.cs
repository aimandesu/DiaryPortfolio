using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Update
{
    public sealed record class UpdateExperienceRequest(
        string Id,
        ExperienceUpload ExperienceUpload
    ) : IRequest<ResultResponse<ExperienceModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

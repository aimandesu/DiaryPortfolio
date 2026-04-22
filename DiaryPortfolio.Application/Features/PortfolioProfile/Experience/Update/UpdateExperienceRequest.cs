using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Update
{
    [RequireOwnership(typeof(ExperienceModel))]
    public sealed record class UpdateExperienceRequest(
        string Id,
        ExperienceUpload ExperienceUpload
    ) : IRequest<ResultResponse<ExperienceModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

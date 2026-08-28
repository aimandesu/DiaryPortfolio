using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Portfolio.Update
{
    [RequireOwnership(typeof(PortfolioProfileModel))]
    public sealed record class UpdateProfileRequest(
        ProfileUpload ProfileUpload
    ) : IRequest<ResultResponse<UserModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

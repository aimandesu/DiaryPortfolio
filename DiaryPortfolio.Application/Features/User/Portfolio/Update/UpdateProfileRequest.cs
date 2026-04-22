using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Profile.Update
{
    [RequireOwnership(typeof(PortfolioProfileModel))]
    public sealed record class UpdateProfileRequest(
        ProfileUpload ProfileUpload
    ) : IRequest<ResultResponse<UserModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Profile.Create
{
    public sealed record class CreateProfileRequest(
        ProfileUpload ProfileUpload) : IRequest<ResultResponse<UserModelDto>>;
}

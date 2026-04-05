using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Get
{
    public sealed record class GetUserRequest(
        string Username,
        ProfileType ProfileType) : IRequest<ResultResponse<UserModelDto>>;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Get
{
    public sealed record class GetUserRequest(string Username) : IRequest<ResultResponse<UserModelDto>>;
}

using DiaryPortfolio.Application.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.Login
{
    public sealed record class LoginRequest(
        string EmailOrUsername,
        string Password) : IRequest<ResultResponse<AuthenticationResponse>>;
}

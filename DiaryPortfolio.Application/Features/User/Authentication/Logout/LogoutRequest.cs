using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.Logout
{
    public sealed record class LogoutRequest(
        ) : IRequest;
}

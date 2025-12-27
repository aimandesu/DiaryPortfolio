using DiaryPortfolio.Application.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.SignUp
{
    public sealed record class SignUpRequest(
        string Email,
        string Username,
        string Password,
        string PasswordConfirmation) : IRequest<ResultResponse<AuthenticationResponse>>;
}

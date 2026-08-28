using DiaryPortfolio.Application.Common;
using Mediator;
using System;

namespace DiaryPortfolio.Application.Features.User.Authentication.ResetPassword
{
    public sealed record class ResetPasswordRequest(
        Guid UserId,
        string Token,
        string NewPassword,
        string NewPasswordConfirmation) : IRequest<ResultResponse<bool>>;
}

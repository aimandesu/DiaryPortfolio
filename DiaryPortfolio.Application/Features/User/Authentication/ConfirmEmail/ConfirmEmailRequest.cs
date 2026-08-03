using DiaryPortfolio.Application.Common;
using Mediator;
using System;

namespace DiaryPortfolio.Application.Features.User.Authentication.ConfirmEmail
{
    public sealed record class ConfirmEmailRequest(
        Guid UserId,
        string Token) : IRequest<ResultResponse<bool>>;
}

using DiaryPortfolio.Application.Common;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Authentication.ResendConfirmationEmail
{
    public sealed record class ResendConfirmationEmailRequest(
        string Email) : IRequest<ResultResponse<bool>>;
}

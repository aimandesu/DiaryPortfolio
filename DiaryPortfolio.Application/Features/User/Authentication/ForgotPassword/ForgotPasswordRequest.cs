using DiaryPortfolio.Application.Common;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Authentication.ForgotPassword
{
    public sealed record class ForgotPasswordRequest(
        string Email) : IRequest<ResultResponse<bool>>;
}

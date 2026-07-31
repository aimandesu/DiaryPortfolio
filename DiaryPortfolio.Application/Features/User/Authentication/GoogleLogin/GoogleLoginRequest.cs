using DiaryPortfolio.Application.Common;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Authentication.GoogleLogin;

public sealed record GoogleLoginRequest(
    string OAuthToken) : IRequest<ResultResponse<AuthenticationResponse>>;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.User.Authentication.Login;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using Google.Apis.Auth;
using Mediator;
using Microsoft.Extensions.Configuration;

namespace DiaryPortfolio.Application.Features.User.Authentication.GoogleLogin;

internal class GoogleLoginHandler(
    IAuthenticationRepository  authenticationRepository,
    ITokenRepository tokenRepository,
    IConfiguration configuration) : IRequestHandler<GoogleLoginRequest, ResultResponse<AuthenticationResponse>>
{
    public async ValueTask<ResultResponse<AuthenticationResponse>> Handle(
        GoogleLoginRequest request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience =
                [
                    configuration["Authentication:Google:ClientId"]
                ]
            };
                
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                request.OAuthToken, settings);
                
            var email = payload.Email;
            var name = payload.Name;
            var googleUserId = payload.Subject;
            
            var response = await authenticationRepository.FindOrCreateUserGoogle(
                email,
                name,
                googleUserId);
            
            if (response.Error != Error.None)
            {
                return ResultResponse<AuthenticationResponse>
                    .Failure(response.Error);
            }

            var token = tokenRepository.GenerateToken(
                Email: response.Result?.Email ?? "",
                UserId: response.Result?.Id ?? Guid.Empty,
                PortfolioProfileId: response.Result?.PortfolioProfile?.Id,
                DiaryProfileId: response.Result?.DiaryProfile?.Id);
            
            return ResultResponse<AuthenticationResponse>.Success(
                new AuthenticationResponse
                {
                    User = response.Result?.ToPortfolioProfileDto(),
                    JWTAccessToken = token.JWTAccessToken,
                    RefreshToken = token.RefreshToken,
                    ExpiresAt = token.ExpiresAt
                }
            );

        }
        catch (InvalidJwtException ex)
        {
            return ResultResponse<AuthenticationResponse>.Failure(
                new Error(System.Net.HttpStatusCode.Unauthorized, ex.Message)
                );
        }
    }
}
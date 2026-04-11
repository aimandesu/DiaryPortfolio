using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.Login
{
    internal class LoginHandler : IRequestHandler<LoginRequest, ResultResponse<AuthenticationResponse>>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private IUserRepository _userRepository;

        public LoginHandler(
            ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository,
            IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
        }

        public async ValueTask<ResultResponse<AuthenticationResponse>> Handle(
            LoginRequest request, 
            CancellationToken cancellationToken)
        {
            var loginResult = await _authenticationRepository.Login(  
                EmailOrUsername: request.EmailOrUsername,
                password: request.Password
            );

            if (loginResult.Error != Error.None)
            {
                return ResultResponse<AuthenticationResponse>.Failure(loginResult.Error);
            }

            var user = await _userRepository.GetUserByUserId(
                loginResult.Result.Id, Domain.Enum.ProfileType.All);

            var token = _tokenRepository.GenerateToken(
                Email: loginResult.Result.Email ?? "",
                UserId: loginResult.Result?.Id ?? Guid.Empty,
                PortfolioProfileId: user.PortfolioProfile.Id,
                DiaryProfileId: user.DiaryProfile.Id
            );

            return ResultResponse<AuthenticationResponse>.Success(
                new AuthenticationResponse
                {
                    User = loginResult.Result?.ToPortfolioProfileDto(),
                    JWTAccessToken = token.JWTAccessToken,
                    RefreshToken = token.RefreshToken,
                    ExpiresAt = token.ExpiresAt
                }
            );

        }
    }
}

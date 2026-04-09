using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.IRepository.IAuthenticationRepository;
using DiaryPortfolio.Application.IRepository.ITokenRepository;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Application.Mapper.User;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.SignUp
{
    internal class SignUpHandler : IRequestHandler<SignUpRequest, ResultResponse<AuthenticationResponse>>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public SignUpHandler(
            ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository
        )
        {
            _tokenRepository = tokenRepository;
            _authenticationRepository = authenticationRepository;
        }

        public async ValueTask<ResultResponse<AuthenticationResponse>> Handle(
            SignUpRequest request, 
            CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirmation)
            {
                return ResultResponse<AuthenticationResponse>.Failure(
                    new Error(System.Net.HttpStatusCode.Unauthorized, "Password and confirmation do not match")
                );
            }

            var portfolioProfile = new PortfolioProfile();
            var diaryProfile =  new DiaryProfile();  //--remove for now because not using it, for 2nd phase

            var signUpResult = await _authenticationRepository.SignUp(
                user: new UserModel
                {
                    UserName = request.Username,
                    Email = request.Email,
                    PortfolioProfile = portfolioProfile,
                    DiaryProfile = diaryProfile,
                },
                password: request.Password
            );

            if (signUpResult == null)
            {
                return ResultResponse<AuthenticationResponse>.Failure(
                    new Error(System.Net.HttpStatusCode.Conflict, "User sign up failed")
                );
            }

            var token = _tokenRepository.GenerateToken(
                Email: signUpResult.Email ?? "",
                UserId: signUpResult?.Id ?? Guid.Empty
            );

            return ResultResponse<AuthenticationResponse>.Success(
                new AuthenticationResponse
                {
                    User = signUpResult?.ToPortfolioProfileDto(),
                    JWTAccessToken = token.JWTAccessToken,
                    RefreshToken = token.RefreshToken,
                    ExpiresAt = token.ExpiresAt
                }
            );

        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
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
    internal class SignUpHandler : IRequestHandler<SignUpRequest, ResultResponse<SignUpResponse>>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public SignUpHandler(
            ITokenRepository tokenRepository,
            IUserRepository userRepository
        )
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public async ValueTask<ResultResponse<SignUpResponse>> Handle(
            SignUpRequest request, 
            CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirmation)
            {
                return ResultResponse<SignUpResponse>.Failure(
                    new Error("PASSWORD_MISMATCH", "Password and confirmation do not match")
                );
            }

            var signUpResult = await _userRepository.SignUp(
                user: new UserModel
                {
                    UserName = request.Username,
                    Email = request.Email
                },
                password: request.Password
            );

            if (signUpResult == null)
            {
                return ResultResponse<SignUpResponse>.Failure(
                    new Error("SIGN_UP_FAILED", "User sign up failed")
                );
            }

            var token = _tokenRepository.GenerateToken(
                Email: signUpResult.Email ?? "",
                UserId: signUpResult?.Id ?? Guid.Empty
            );

            return ResultResponse<SignUpResponse>.Success(
                new SignUpResponse
                {
                    User = signUpResult?.ToUserModelDto(),
                    JWTAccessToken = token.JWTAccessToken,
                    RefreshToken = token.RefreshToken,
                    ExpiresAt = token.ExpiresAt
                }
            );

        }
    }
}

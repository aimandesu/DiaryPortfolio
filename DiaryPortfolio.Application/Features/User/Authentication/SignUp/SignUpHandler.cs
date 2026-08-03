using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.Email;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SignUpHandler> _logger;

        public SignUpHandler(
            ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<SignUpHandler> logger
        )
        {
            _tokenRepository = tokenRepository;
            _authenticationRepository = authenticationRepository;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
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

            var portfolioProfile = new PortfolioProfileModel
            {
                // PortfolioSections = PortfolioSectionModel.CreateDefaults(),
            };
            var diaryProfile =  new DiaryProfileModel();  //--remove for now because not using it, for 2nd phase

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
                UserId: signUpResult?.Id ?? Guid.Empty,
                PortfolioProfileId: portfolioProfile.Id,
                DiaryProfileId: diaryProfile.Id
            );

            try
            {
                var confirmationToken = await _authenticationRepository
                    .GenerateEmailConfirmationTokenAsync(signUpResult!);
                
                var frontendBaseUrl = _configuration["FrontendBaseUrl"];
                var link = $"{frontendBaseUrl}/confirm-email?userId={signUpResult!.Id}&token={Uri.EscapeDataString(confirmationToken)}";
                var (subject, html) = EmailTemplates.ConfirmationEmail(link);

                await _emailSender.SendEmailAsync(signUpResult!.Email ?? request.Email, subject, html, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send confirmation email to {Email}", request.Email);
            }

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

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.Email;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.ForgotPassword
{
    internal class ForgotPasswordHandler(
        IAuthenticationRepository authenticationRepository,
        IEmailSender emailSender,
        IConfiguration configuration,
        ILogger<ForgotPasswordHandler> logger)
        : IRequestHandler<ForgotPasswordRequest, ResultResponse<bool>>
    {
        public async ValueTask<ResultResponse<bool>> Handle(
            ForgotPasswordRequest request,
            CancellationToken cancellationToken)
        {
            var user = await authenticationRepository.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                try
                {
                    var token = await authenticationRepository.GeneratePasswordResetTokenAsync(user);
                    
                    var frontendBaseUrl = configuration["FrontendBaseUrl"];
                    var link = $"{frontendBaseUrl}/reset-password?userId={user.Id}&token={Uri.EscapeDataString(token)}";
                    var (subject, html) = EmailTemplates.PasswordResetEmail(link);

                    await emailSender.SendEmailAsync(user.Email ?? request.Email, subject, html, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to send password reset email to {Email}", request.Email);
                }
            }

            // Always report success to avoid leaking whether an account/email exists.
            return ResultResponse<bool>.Success(true);
        }
    }
}

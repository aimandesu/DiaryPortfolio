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

namespace DiaryPortfolio.Application.Features.User.Authentication.ResendConfirmationEmail
{
    internal class ResendConfirmationEmailHandler(
        IAuthenticationRepository authenticationRepository,
        IEmailSender emailSender,
        IConfiguration configuration,
        ILogger<ResendConfirmationEmailHandler> logger)
        : IRequestHandler<ResendConfirmationEmailRequest, ResultResponse<bool>>
    {
        public async ValueTask<ResultResponse<bool>> Handle(
            ResendConfirmationEmailRequest request,
            CancellationToken cancellationToken)
        {
            var user = await authenticationRepository.FindByEmailAsync(request.Email);

            if (user is not null && !user.EmailConfirmed)
            {
                try
                {
                    var token = await authenticationRepository.GenerateEmailConfirmationTokenAsync(user);
                    
                    var frontendBaseUrl = configuration["FrontendBaseUrl"];
                    var link = $"{frontendBaseUrl}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
                    var (subject, html) = EmailTemplates.ConfirmationEmail(link);

                    await emailSender.SendEmailAsync(user.Email ?? request.Email, subject, html, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to resend confirmation email to {Email}", request.Email);
                }
            }
            
            return ResultResponse<bool>.Success(true);
        }
    }
}

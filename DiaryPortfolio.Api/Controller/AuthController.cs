using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.User.Authentication;
using DiaryPortfolio.Application.Features.User.Authentication.SignUp;
using DiaryPortfolio.Application.Features.User.Authentication.GoogleLogin;
using DiaryPortfolio.Application.Features.User.Authentication.Login;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using DiaryPortfolio.Application.Features.User.Authentication.Logout;
using DiaryPortfolio.Application.Features.User.Authentication.ConfirmEmail;
using DiaryPortfolio.Application.Features.User.Authentication.ResendConfirmationEmail;
using DiaryPortfolio.Application.Features.User.Authentication.ForgotPassword;
using DiaryPortfolio.Application.Features.User.Authentication.ResetPassword;
using Google.Apis.Auth;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        IMediator mediator) : ControllerBase
    {
        [HttpPost("signUp")]
        public async Task<ActionResult<ResultResponse<AuthenticationResponse>>> SignUp(
            [FromBody] SignUpRequest query,
            CancellationToken cancellationToken
        )
        {
            var request = new SignUpRequest(
                query.Email,
                query.Username,
                query.Password,
                query.PasswordConfirmation
            );

            return await mediator.Send(request, cancellationToken);

        }

        [HttpPost("login")]
        public async Task<ActionResult<ResultResponse<AuthenticationResponse>>> Login(
            [FromBody] LoginRequest query,
            CancellationToken cancellationToken
        )
        {
            var request = new LoginRequest(
                query.EmailOrUsername,
                query.Password
            );
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            CancellationToken cancellationToken)
        {
            var request = new LogoutRequest();

            await mediator.Send(request, cancellationToken);

            return Ok();

        }
        
        [HttpPost("loginWithGoogle")]
        public async Task<ActionResult<ResultResponse<AuthenticationResponse>>> LoginWithGoogle(
            [FromBody] GoogleLoginRequest googleLoginRequest,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(
                googleLoginRequest,
                cancellationToken);
        }

        [HttpPost("confirmEmail")]
        public async Task<ActionResult<ResultResponse<bool>>> ConfirmEmail(
            [FromBody] ConfirmEmailRequest query,
            CancellationToken cancellationToken)
        {
            var request = new ConfirmEmailRequest(query.UserId, query.Token);
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("resendConfirmationEmail")]
        public async Task<ActionResult<ResultResponse<bool>>> ResendConfirmationEmail(
            [FromBody] ResendConfirmationEmailRequest query,
            CancellationToken cancellationToken)
        {
            var request = new ResendConfirmationEmailRequest(query.Email);
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult<ResultResponse<bool>>> ForgotPassword(
            [FromBody] ForgotPasswordRequest query,
            CancellationToken cancellationToken)
        {
            var request = new ForgotPasswordRequest(query.Email);
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResultResponse<bool>>> ResetPassword(
            [FromBody] ResetPasswordRequest query,
            CancellationToken cancellationToken)
        {
            var request = new ResetPasswordRequest(
                query.UserId,
                query.Token,
                query.NewPassword,
                query.NewPasswordConfirmation);
            return await mediator.Send(request, cancellationToken);
        }
    }
}

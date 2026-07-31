using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.User.Authentication;
using DiaryPortfolio.Application.Features.User.Authentication.SignUp;
using DiaryPortfolio.Application.Features.User.Authentication.GoogleLogin;
using DiaryPortfolio.Application.Features.User.Authentication.Login;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using DiaryPortfolio.Application.Features.User.Authentication.Logout;
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
    }
}

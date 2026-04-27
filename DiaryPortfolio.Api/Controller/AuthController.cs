using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.User.Authentication;
using DiaryPortfolio.Application.Features.User.Authentication.SignUp;
using DiaryPortfolio.Application.Features.User.Authentication.Login;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using DiaryPortfolio.Application.Features.User.Authentication.Logout;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

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

            return await _mediator.Send(request, cancellationToken);

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
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            CancellationToken cancellationToken)
        {
            var request = new LogoutRequest();

            await _mediator.Send(request, cancellationToken);

            return Ok();

        }
    }
}

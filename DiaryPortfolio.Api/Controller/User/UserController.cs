using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.Features.User.Authentication;
using DiaryPortfolio.Application.Features.User.Authentication.Login;
using DiaryPortfolio.Application.Features.User.Authentication.SignUp;
using DiaryPortfolio.Application.Features.User.Get;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller.User
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly SignInManager<UserModel> _signInManager;

        public UserController(
            IMediator mediator,
            SignInManager<UserModel> signInManager
        )
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<ResultResponse<UserModelDto>>> GetUser(
            [FromQuery] GetUserRequest request,
            CancellationToken cancellationToken
        )
        {
            return await _mediator.Send(request, cancellationToken);
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
    }
}

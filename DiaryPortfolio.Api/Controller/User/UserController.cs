using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.Features.User.Get;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller.User
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<ResultResponse<UserModelDto>>> GetUser(
            [FromQuery] GetUserRequest request,
            CancellationToken cancellationToken
        )
        {
            return await _mediator.Send(request, cancellationToken);
        }

    }
}

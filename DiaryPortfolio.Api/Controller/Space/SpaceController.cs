using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Space;
using DiaryPortfolio.Application.Features.DiaryProfile.Space.Create;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller.Space
{
    [Route("api/space")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SpaceController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpPost("addSpace")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<SpaceModelDto>>> AddSpace(
            [FromForm] string Title,
            CancellationToken cancellationToken
        )
        {
            var request = new CreateSpaceRequest(Title);
            return await _mediator.Send(request, cancellationToken);
        }

    }
}

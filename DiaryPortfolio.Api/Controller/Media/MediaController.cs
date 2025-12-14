using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.Media.GetAll;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DiaryPortfolio.Api.Controller.Media
{
    [Route("api/media")]
    public class MediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediaController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ResultResponse<Pagination<MediaModel>>>> GetAllMediaByUsername(
            [FromQuery] QuerySearchObject query,
            [FromQuery] string username,
            CancellationToken cancellationToken
        )
        {
            var request = new GetAllMediaRequest(query, username);
            return await _mediator.Send(request, cancellationToken);

        }

    }

}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.CustomUrl.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.CustomUrl.Delete;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/custom-url")]
    [ApiController]
    public class CustomUrlController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomUrlController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<CustomUrlModelDto>>> CreateCustomUrl(
            [FromBody] CreateCustomUrlRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                request,
                cancellationToken);

            return response;
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<CustomUrlModelDto>>> DeleteCustomUrl(
            [FromRoute] DeleteCustomUrlRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                request,
                cancellationToken);

            return response;
        }

    }
}

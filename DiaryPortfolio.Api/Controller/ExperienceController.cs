using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Create;
using DiaryPortfolio.Application.Request;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/experience")]
    public class ExperienceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExperienceController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ExperienceModelDto>>> CreateExperience(
            [FromBody] ExperienceUpload request, 
            CancellationToken cancellationToken
        )
        {
            var response = new CreateExperienceRequest(request);

            return await _mediator.Send(response, cancellationToken);

        }


    }
}

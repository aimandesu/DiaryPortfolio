using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.DiaryProfile.Media.Delete;
using DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Delete;
using DiaryPortfolio.Application.Features.PortfolioProfile.Experience.GetAll;
using DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Update;
using DiaryPortfolio.Application.Features.Reporting.Experience;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
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

        [HttpGet("getAll/{userId}")]
        public async Task<ActionResult<ResultResponse<List<ExperienceModelDto>>>> GetAllExperiences(
            [FromRoute] string userId,
            CancellationToken cancellationToken
        )
        {
            var response = new GetAllExperienceRequest(userId);
            return await _mediator.Send(response, cancellationToken);
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

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ExperienceModelDto>>> DeleteExperience(
            [FromRoute] string id,
            CancellationToken cancellationToken
        )
        {
            var request = new DeleteExperienceRequest(id);
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPatch("update/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ExperienceModelDto>>> UpdateExperience(
            [FromRoute] string id,
            [FromBody] ExperienceUpload request,
            CancellationToken cancellationToken
        )
        {
            var response = new UpdateExperienceRequest(id, request);

            return await _mediator.Send(response,cancellationToken);

        }


        [HttpGet("export/pdf/{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ExportPdf(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
           

            var pdfBytes = await _mediator.Send(
                new CreateExperienceReportRequest(id),
                cancellationToken
            );

            return File(pdfBytes, "application/pdf", "experience-report.pdf");
        }

    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Delete;
using DiaryPortfolio.Application.Features.PortfolioProfile.Resume.GetAll;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/resume")]
    [ApiController]
    public class ResumeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        public ResumeController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate-resume-template/{templateId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ResumeModel>>> GenerateResumeTemplate(
            string templateId,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new CreateResumeRequest(templateId),
                cancellationToken
            );
        }
        
        [HttpGet("selection")]
        public async Task<IActionResult> GetResumeSelection(
            string username,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetAllResumeSelectionRequest(username),
                cancellationToken);

            if (response.Error != Error.None)
            {
                return View("~/Views/Error.cshtml", response.Error);
            }
            
            return View("~/Views/Resume/ResumeSelection.cshtml", response.Result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ResumeModel>>> DeleteResume(
             [FromRoute] string id,
             CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new DeleteResumeRequest(id),
                cancellationToken);
        }

    }
}

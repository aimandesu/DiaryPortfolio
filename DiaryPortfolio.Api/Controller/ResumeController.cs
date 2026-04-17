using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Delete;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/resume")]
    [ApiController]
    public class ResumeController : ControllerBase
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

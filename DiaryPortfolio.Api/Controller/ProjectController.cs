using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Project.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Project.Delete;
using DiaryPortfolio.Application.Features.PortfolioProfile.Project.Update;
using DiaryPortfolio.Application.Request;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ProjectModelDto>>> CreateProject(
            [FromForm] CreateProjectRequest request, //We can use CreateProjectCommand if we derive it with IRequestHandler
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                request, 
                cancellationToken);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ProjectModelDto>>> DeleteProject(
            [FromRoute] DeleteProjectRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                request,
                cancellationToken);

            return response;

        }

        [HttpPost("update/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<ProjectModelDto>>> UpdateProject(
            [FromRoute] string id,
            [FromForm] UpdateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
               request with { Id = id },
               cancellationToken);

            return response;
        }

    }
}

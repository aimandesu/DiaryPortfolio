using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Delete;
using DiaryPortfolio.Application.Features.PortfolioProfile.Skill.GetAll;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/skill")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<SkillModelDto>>> CreateSkill(
            [FromBody] CreateSkillRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                request,
                cancellationToken);

            return response;
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<SkillModelDto>>> DeleteSkill(
            [FromRoute] DeleteSkillRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                request,
                cancellationToken);

            return response;
        }

        [HttpGet("getAll/{username}")]
        public async Task<ActionResult<ResultResponse<List<SkillModelDto>>>> GetAllSkill(
            [FromRoute] string username,
            CancellationToken cancellationToken)
        {
            var response = new GetAllSkillRequest(username);
            return await _mediator.Send(response, cancellationToken);
        }

    }
}

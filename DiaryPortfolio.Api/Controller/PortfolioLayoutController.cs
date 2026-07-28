using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetMyLayout;
using DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetPublicLayout;
using DiaryPortfolio.Application.Features.PortfolioProfile.Layout.SaveLayout;
using DiaryPortfolio.Application.Features.PortfolioProfile.Layout.Unplace;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/portfolio-layout")]
    [ApiController]
    public class PortfolioLayoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PortfolioLayoutController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("mine")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<List<PortfolioSectionModelDto>>>> GetMyLayout(
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetMyLayoutRequest(), cancellationToken);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ResultResponse<List<PortfolioSectionModelDto>>>> GetPublicLayout(
            [FromRoute] string username,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPublicLayoutRequest(username), cancellationToken);
        }

        [HttpPut("layout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<List<PortfolioSectionModelDto>>>> SaveLayout(
            [FromBody] SaveLayoutRequest request,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}/unplace")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<PortfolioSectionModelDto>>> UnplaceSection(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UnplaceSectionRequest(id), cancellationToken);
        }
    }
}

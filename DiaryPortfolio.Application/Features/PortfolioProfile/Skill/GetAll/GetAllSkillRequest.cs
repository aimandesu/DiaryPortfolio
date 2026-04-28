using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Skill.GetAll
{
    public sealed record class GetAllSkillRequest(
         string Username) : IRequest<ResultResponse<List<SkillModelDto>>>;
}

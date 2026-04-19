using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Create
{
    public sealed record class CreateSkillRequest(
        string SkillName,
        string Description,
        SkillLevelEnum SkillLevel
    ) : IRequest<ResultResponse<SkillModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

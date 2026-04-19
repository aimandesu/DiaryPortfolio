using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Delete
{
    [RequireOwnership(typeof(SkillModel))]
    public sealed record class DeleteSkillRequest(
        string Id
    ) : IRequest<ResultResponse<SkillModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}
  
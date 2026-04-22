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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Delete
{
    [RequireOwnership(typeof(ExperienceModel))]
    public sealed record class DeleteExperienceRequest(
        string Id
    ) : IRequest<ResultResponse<ExperienceModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Delete
{
    [RequireOwnership(typeof(ProjectModel))]
    public sealed record class DeleteProjectRequest(
        string Id
    ) : IRequest<ResultResponse<ProjectModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

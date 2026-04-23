using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Update
{
    [RequireOwnership(typeof(ProjectModel))]
    public sealed record class UpdateProjectRequest(
        string JsonProjectUploadRequest,
        IFormFile? ProjectFileStream,
        List<IFormFile>? MediaFileStreams
    ) : IRequest<ResultResponse<ProjectModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile
    {
        public string Id { get; init; } = string.Empty;
    }
}

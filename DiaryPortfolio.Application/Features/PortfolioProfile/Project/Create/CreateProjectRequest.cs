using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using Mediator;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Create
{
    public sealed record class CreateProjectRequest(
        string JsonProjectUploadRequest,
        IFormFile? ProjectFileStream,
        List<IFormFile> MediaFileStreams
    ) : IRequest<ResultResponse<ProjectModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}
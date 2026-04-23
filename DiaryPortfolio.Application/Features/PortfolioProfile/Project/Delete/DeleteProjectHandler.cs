using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Delete
{
    internal class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, ResultResponse<ProjectModelDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectHandler(
            IProjectRepository projectRepository,
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<ProjectModelDto>> Handle(
            DeleteProjectRequest request, 
            CancellationToken cancellationToken)
        {

            var filePaths = new List<string>();
            var project = await _projectRepository.DeleteProject(new Guid(request.Id));

            if (project.Result.ProjectPhotos != null)
            {
                filePaths.AddRange(project.Result.ProjectPhotos
                    .Select(p => p?.Photo?.Url ?? ""));
            }

            if (project.Result.ProjectVideos != null)
            {
                filePaths.AddRange(project.Result.ProjectVideos
                    .Select(v => v?.Video?.Url ?? ""));
            }

            if (project.Result.ProjectFile != null)
            {
                filePaths.Add(project?.Result?.ProjectFile?.Url ?? "");
            }

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);

                if (filePaths.Count > 0)
                { 
                    _fileHandlerRepository.DeleteFiles(filePaths);
                }

                return ResultResponse<ProjectModelDto>.Success(
                    project.Result.ToProjectModelDto());


            }
            catch (DbUpdateException ex)
            {
                return ResultResponse<ProjectModelDto>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.Conflict,
                        ex.InnerException?.Message ?? ex.Message)
                );
            }


        }
    }
}

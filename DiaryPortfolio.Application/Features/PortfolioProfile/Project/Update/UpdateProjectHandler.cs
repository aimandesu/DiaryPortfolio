using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Update
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, ResultResponse<ProjectModelDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(
            IProjectRepository projectRepository,
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<ProjectModelDto>> Handle(
            UpdateProjectRequest request, 
            CancellationToken cancellationToken)
        {
            var project = request.JsonProjectUploadRequest.DeserializeForm<ProjectUpload>();

            var streams = new[]
            {
                request.ProjectFileStream?.ToMediaStream()
            }
            .Where(x => x is not null)
            .Cast<MediaStream>()
            .Concat(request.MediaFileStreams?.ToMediaStreams() ?? []);

            var existingProject = await _projectRepository.GetProject(
                new Guid(request.Id));

            if (existingProject.Error != Error.None)
            {
                return ResultResponse<ProjectModelDto>.Failure(
                    existingProject.Error);
            }    

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                [.. streams],
                MediaType.Project,
                existingProject.Result?.Id.ToString()
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<ProjectModelDto>.Failure(
                    uploadResult.Error);
            }

            var media = uploadResult.Result.ExtractMedia();

            //Delete part 

            var deletedIds = project?.DeletedIds ?? [];

            var filesToDelete = new List<string>();

            filesToDelete.AddRange(
                existingProject.Result?.ProjectPhotos
                .Where(p => p.Photo != null && deletedIds.Contains(p.Photo.Id.ToString()))
                .Select(p => p.Photo?.Url ?? "") ?? []
                //.Where(url => !string.IsNullOrEmpty(url)) ?? []
                );

            filesToDelete.AddRange(
                existingProject.Result?.ProjectVideos
                .Where(pv => pv.Video != null && deletedIds.Contains(pv.Video.Id.ToString()))
                .Select(pv => pv.Video?.Url ?? "") ?? []
                //.Where(url => !string.IsNullOrEmpty(url))
                );

            filesToDelete.Add(existingProject.Result?.ProjectFile?.Url ?? "");

            var uploadProject = await _projectRepository.UpdateProject(
                projectUpload: project,
                file: media.Files.FirstOrDefault(),
                photos: media.Photos,
                videos: media.Videos,
                project: existingProject.Result
            );

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);

                if (filesToDelete != null && filesToDelete.Count > 0)
                {
                    _fileHandlerRepository.DeleteFiles(filesToDelete);
                }

                return ResultResponse<ProjectModelDto>.Success(
                    uploadProject.Result.ToProjectModelDto());
            }
            catch (DbUpdateException ex)
            {
                _fileHandlerRepository.DeleteFiles(
                    [
                    .. media.Files.Select(e => e.Url),
                    .. media.Photos.Select(e => e.Url),
                    .. media.Videos.Select(e => e.Url)
                    ]
                );

                return ResultResponse<ProjectModelDto>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.Conflict,
                        ex.InnerException?.Message ?? ex.Message));
            }

        }
    }
}

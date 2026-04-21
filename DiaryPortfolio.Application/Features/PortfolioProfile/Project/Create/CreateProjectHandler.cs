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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Create
{
    internal class CreateProjectHandler : IRequestHandler<CreateProjectRequest, ResultResponse<ProjectModelDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectHandler(
            IProjectRepository projectRepository,
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<ProjectModelDto>> Handle(
            CreateProjectRequest request, 
            CancellationToken cancellationToken)
        {
            //-------------------------------------------> original implementation
            //var project = JsonSerializer.Deserialize<ProjectUpload>(
            //    request.JsonProjectUploadRequest,
            //    new JsonSerializerOptions
            //    {
            //        Converters = { new JsonStringEnumConverter() },
            //        PropertyNameCaseInsensitive = true
            //    });

            //var streams = new[]
            //{
            //    request.ProjectFileStream != null
            //        ? new MediaStream
            //        {
            //            Stream = request.ProjectFileStream.OpenReadStream(),
            //            FileName = request.ProjectFileStream.FileName
            //        }
            //        : null
            //}
            //.Where(x => x is not null)
            //.Cast<MediaStream>()
            //.Concat(request.MediaFileStreams.Select(f => new MediaStream
            //{
            //    Stream = f.OpenReadStream(),
            //    FileName = f.FileName
            //})); --------------------------------------> original implementation

            var project = request.JsonProjectUploadRequest.DeserializeForm<ProjectUpload>();

            var streams = new[]
            {
                request.ProjectFileStream?.ToMediaStream()
            }
            .Where(x => x is not null)
            .Cast<MediaStream>()
            .Concat(request.MediaFileStreams.ToMediaStreams());

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                [.. streams],
                MediaType.Project);

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<ProjectModelDto>.Failure(
                    uploadResult.Error);
            }

            var media = uploadResult.Result.ExtractMedia();

            var projectUpload = new ProjectUpload
            {
                Title = project?.Title ?? "",
                Description = project?.Description ?? ""
            };

            var uploadProject = await _projectRepository.CreateProject(
                projectUpload: projectUpload,
                file: media.Files.FirstOrDefault(),
                videos: media.Videos,
                photos: media.Photos
            );

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);
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
                        ex.InnerException?.Message ?? ex.Message)
                );

            }

        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper.Media;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Create
{
    internal class CreateMediaHandler : IRequestHandler<CreateMediaRequest, ResultResponse<MediaModelDto>>
    {
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IMediaHandlerRepository _mediaHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMediaHandler(
            IFileHandlerRepository fileHandlerRepository,
            IMediaHandlerRepository mediaHandlerRepository,
            IUnitOfWork unitOfWork
        )
        {
            _fileHandlerRepository = fileHandlerRepository;
            _mediaHandlerRepository = mediaHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<MediaModelDto>> Handle(
            CreateMediaRequest request, 
            CancellationToken cancellationToken)
        {
            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                request.MediaUpload.FileStreams,
                request.MediaUpload.MediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<MediaModelDto>.Failure(uploadResult.Error);
            }

            var uploadMediaResult = await _mediaHandlerRepository.UploadMedia(
                title: request.MediaUpload.Title,
                description: request.MediaUpload.Description,
                mediaStatus: request.MediaUpload.MediaStatus,
                mediaType: request.MediaUpload.MediaType,
                textStyle: request.MediaUpload.TextTitle.ToString(),
                spaceId: request.MediaUpload.SpaceId,
                location: request.MediaUpload.Location,
                condition: request.MediaUpload.Condition,
                videos: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.Video))
                    .Select(e => e[MediaSubType.Video].Videos)
                    .ToList()!,
                photos: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.Image))
                    .Select(e => e[MediaSubType.Image].Photos)
                    .ToList()!
            );

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);
                return ResultResponse<MediaModelDto>.Success(uploadMediaResult.Result.ToMediaModelDto());
            }
            catch (DbUpdateException ex)
            {
                // Rollback: delete uploaded files
                _fileHandlerRepository.DeleteFiles(
                    [
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.Video))
                        .Select(e => e[MediaSubType.Video].Videos?.Url ?? ""),
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.Image))
                        .Select(e => e[MediaSubType.Image].Photos?.Url ?? "")
                    ]);

                return ResultResponse<MediaModelDto>.Failure(
                    new Error("Database_Error", ex.InnerException?.Message ?? ex.Message)
                );
            }

        }
    }
}

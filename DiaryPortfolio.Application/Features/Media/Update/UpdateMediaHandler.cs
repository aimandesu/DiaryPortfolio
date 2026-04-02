using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper.Media;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Update
{
    internal class UpdateMediaHandler : IRequestHandler<UpdateMediaRequest, ResultResponse<MediaModelDto>>
    {
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IMediaHandlerRepository _mediaHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMediaHandler(
            IFileHandlerRepository fileHandlerRepository, 
            IMediaHandlerRepository mediaHandlerRepository, 
            IUnitOfWork unitOfWork)
        {
            _fileHandlerRepository = fileHandlerRepository;
            _mediaHandlerRepository = mediaHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<MediaModelDto>> Handle(
            UpdateMediaRequest request, 
            CancellationToken cancellationToken)
        {
            var mediaType = EnumHelper.ParseEnumOrThrow<MediaType>(
                request.MediaUpload.MediaType.ToString()
            );

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                request.MediaUpload.FileStreams,
                mediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<MediaModelDto>.Failure(uploadResult.Error);
            }

            var existingMedia = await _mediaHandlerRepository.GetMediaWithFiles(new Guid(request.mediaId));

            var oldFilePaths = existingMedia?.Result?.PhotoModels
                .Select(p => p.Url)
                .Concat(existingMedia.Result.VideoModels.Select(v => v.Url))
                .ToList();

            var updateMediaResult = await _mediaHandlerRepository.UpdateMedia(
                id: new Guid(request.mediaId),
                media: request.MediaUpload,
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

                _fileHandlerRepository.DeleteFiles(oldFilePaths ?? []);

                return ResultResponse<MediaModelDto>.Success(updateMediaResult.Result.ToMediaModelDto());
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

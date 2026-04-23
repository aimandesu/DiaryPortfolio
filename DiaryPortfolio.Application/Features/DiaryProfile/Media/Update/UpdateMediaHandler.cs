using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.Update
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

            var existingMedia = await _mediaHandlerRepository
                .GetMediaWithFiles(new Guid(request.Id));

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                request.MediaUpload.FileStreams,
                mediaType,
                existingMedia.Result?.Id.ToString()
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<MediaModelDto>
                    .Failure(uploadResult.Error);
            }

            //var deletedVideoIdGuids = request.MediaUpload.DeletedVideoIds
            //    .Select(id => Guid.Parse(id))
            //    .ToHashSet();

            //var deletedPhotoIdGuids = request.MediaUpload.DeletedPhotoIds
            //    .Select(id => Guid.Parse(id))
            //    .ToHashSet();

            List<string> deletedIds = [
                .. request.MediaUpload.DeletedPhotoIds, 
                .. request.MediaUpload.DeletedVideoIds
            ];

            var deletedPhotos = existingMedia.Result?.MediaPhotos
                .Where(p => p.Photo != null && deletedIds//deletedPhotoIdGuids
                    .Contains(p.Photo.Id.ToString()))
                .Select(p => p?.Photo?.Url);

            var deletedVideos = existingMedia.Result?.MediaVideos
                .Where(v => v?.Video != null && deletedIds //deletedVideoIdGuids
                    .Contains(v.Video.Id.ToString()))
                .Select(v => v?.Video?.Url);

            var filesToDelete = deletedPhotos?.Concat(deletedVideos ?? []).ToList();

            var media = uploadResult.Result.ExtractMedia();

            var updateMediaResult = await _mediaHandlerRepository.UpdateMedia(
                media: request.MediaUpload,
                videos: media.Videos,
                photos: media.Photos,
                existingMedia: existingMedia.Result
            );

            try
            {

                await _unitOfWork.SaveChanges(cancellationToken);

                if (filesToDelete != null && filesToDelete.Count > 0)
                {
                    _fileHandlerRepository.DeleteFiles(filesToDelete);
                }

                return ResultResponse<MediaModelDto>
                    .Success(updateMediaResult.Result.ToMediaModelDto());
            }
            catch (DbUpdateException ex)
            {
                _fileHandlerRepository.DeleteFiles(
                    [
                    .. media.Photos.Select(e => e.Url),
                    .. media.Videos.Select(e => e.Url)
                    ]);

                return ResultResponse<MediaModelDto>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.Conflict, 
                        ex.InnerException?.Message ?? ex.Message)
                );
            }

        }
    }
}

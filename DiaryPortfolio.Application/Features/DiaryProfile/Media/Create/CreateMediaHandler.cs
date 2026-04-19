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
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.Create
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
            var mediaType = EnumHelper.ParseEnumOrThrow<MediaType>(
                request.MediaUpload.MediaType.ToString()
            );

            //var mediaStatus = EnumHelper.ParseEnumOrThrow<MediaStatus>(
            //   request.MediaUpload.MediaStatus.ToString()
            //);

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                request.MediaUpload.FileStreams,
                mediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<MediaModelDto>.Failure(uploadResult.Error);
            }

            var uploadMediaResult = await _mediaHandlerRepository.UploadMedia(
                mediaUpload: request.MediaUpload,
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
                    new Error(System.Net.HttpStatusCode.Conflict, ex.InnerException?.Message ?? ex.Message)
                );
            }

        }
    }
}

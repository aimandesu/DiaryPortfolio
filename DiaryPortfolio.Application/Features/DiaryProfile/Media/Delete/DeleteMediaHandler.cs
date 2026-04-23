using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.Delete
{
    internal class DeleteMediaHandler : IRequestHandler<DeleteMediaRequest, ResultResponse<MediaModel>>
    {
        private readonly IMediaHandlerRepository _mediaHandlerRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMediaHandler(
            IMediaHandlerRepository mediaHandlerRepository,
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mediaHandlerRepository = mediaHandlerRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }


        public async ValueTask<ResultResponse<MediaModel>> Handle(
            DeleteMediaRequest request, 
            CancellationToken cancellationToken)
        {
            var filePaths = new List<string>();
            var mediaFiles = await _mediaHandlerRepository.DeleteMedia(new Guid(request.Id));

            var media = mediaFiles.Result;

            if (media?.MediaPhotos != null)
            {
                filePaths.AddRange(media.MediaPhotos.Select(p => p?.Photo?.Url ?? ""));
            }

            if (media?.MediaVideos != null)
            {
                filePaths.AddRange(media.MediaVideos.Select(v => v?.Video?.Url ?? ""));
            }

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);

                if (filePaths.Count > 0)
                {
                    _fileHandlerRepository.DeleteFiles(filePaths);
                }

                return ResultResponse<MediaModel>.Success(
                    new MediaModel { Id = new Guid(request.Id) }
                );
            }
            catch (DbUpdateException ex) {
                return ResultResponse<MediaModel>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.Conflict,
                        ex.InnerException?.Message ?? ex.Message)
                );
            }

        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Delete
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
            var mediaFiles = _mediaHandlerRepository.GetMediaFiles(request.MediaId);

            if (mediaFiles.Count == 0)
            {
                return ResultResponse<MediaModel>.Failure(new Error("NOT FOUND", "Media Files not found"));
            }

            _fileHandlerRepository.DeleteFiles(mediaFiles);
            await _unitOfWork.SaveChanges(cancellationToken);

            return ResultResponse<MediaModel>.Success(
                new MediaModel { Id = new Guid(request.MediaId) }
            );

        }
    }
}

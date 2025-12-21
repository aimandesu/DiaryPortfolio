using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Create.Post
{
    internal class PostHandler : IRequestHandler<PostRequest, ResultResponse<MediaModel>>
    {
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IMediaHandlerRepository _mediaHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostHandler(
            IFileHandlerRepository fileHandlerRepository,
            IMediaHandlerRepository mediaHandlerRepository,
            IUnitOfWork unitOfWork
        )
        {
            _fileHandlerRepository = fileHandlerRepository;
            _mediaHandlerRepository = mediaHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<MediaModel>> Handle(
            PostRequest request, 
            CancellationToken cancellationToken)
        {
            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                request.MediaUpload.FileStreams,
                request.MediaUpload.MediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<MediaModel>.Failure(uploadResult.Error);
            }

            var uploadMediaResult = await _mediaHandlerRepository.UploadMedia(
                videos: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.Video))
                    .Select(e => e[MediaSubType.Video].Videos)
                    .ToList()!,
                photos: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.Image))
                    .Select(e => e[MediaSubType.Image].Photos)
                    .ToList()!
            );

            if (uploadMediaResult.Error != Error.None)
            {
                //remove pictures and videos from storage as media upload failed
                return ResultResponse<MediaModel>.Failure(uploadMediaResult.Error);
            }

            await _unitOfWork.SaveChanges(cancellationToken);

            return uploadMediaResult;

        }
    }
}

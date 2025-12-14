using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.IRepository.IMediaRepository;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.GetAll
{
    internal class GetAllMediaHandler : IRequestHandler<GetAllMediaRequest, ResultResponse<Pagination<MediaModel>>>
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IUserRepository _userRepository;

        public GetAllMediaHandler(
            IMediaRepository mediaRepository,
            IUserRepository userRepository
        )
        {
            _mediaRepository = mediaRepository;
            _userRepository = userRepository;
        }

        public async ValueTask<ResultResponse<Pagination<MediaModel>>> Handle(
            GetAllMediaRequest request, 
            CancellationToken cancellationToken)
        {
           
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user == null)
            {
                return ResultResponse<Pagination<MediaModel>>.Failure(
                    new Error("USER_NOT_FOUND", "User not found")
                );
            }

            Guid userId = user.Id;

            var mediaPagination = await _mediaRepository.GetAllMediaByUsername(
                request.QuerySearchObject,
                userId);

            return ResultResponse<Pagination<MediaModel>>.Success(mediaPagination);
        }
    }
}

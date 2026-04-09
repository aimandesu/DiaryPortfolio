using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.IRepository.IMediaRepository;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Application.Mapper.Media;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.GetAll
{
    internal class GetAllMediaHandler : IRequestHandler<GetAllMediaRequest, ResultResponse<Pagination<MediaModelDto>>>
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

        public async ValueTask<ResultResponse<Pagination<MediaModelDto>>> Handle(
            GetAllMediaRequest request, 
            CancellationToken cancellationToken)
        {
           
            var user = await _userRepository.GetUserByUsername(request.Username, Domain.Enum.ProfileType.Diary);

            if (user == null)
            {
                return ResultResponse<Pagination<MediaModelDto>>.Failure(
                    new Error(System.Net.HttpStatusCode.NotFound, "User not found")
                );
            }

            Guid userId = user.Id;

            var mediaPagination = await _mediaRepository.GetAllMediaByUsername(
                request.QuerySearchObject,
                userId);

            //var mapped = mediaPagination.MapPagination(e => e.ToMediaModelDto());

            return ResultResponse<Pagination<MediaModelDto>>.Success(mediaPagination); //mapped
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Application.Mapper.User;
using Mediator;

namespace DiaryPortfolio.Application.Features.User.Get
{
    internal class GetUserHandler : IRequestHandler<GetUserRequest, ResultResponse<UserModelDto>>
    {

        private readonly IUserRepository _userRepository;
        public GetUserHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async ValueTask<ResultResponse<UserModelDto>> Handle(
            GetUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user == null)
            {
                return ResultResponse<UserModelDto>.Failure(
                    new Error("USER_NOT_FOUND", "User not found")
                );
            }

            return ResultResponse<UserModelDto>.Success(user.ToUserModelDto());


        }


    }
}

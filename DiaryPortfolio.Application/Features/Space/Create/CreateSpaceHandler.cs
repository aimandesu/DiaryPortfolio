using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.ISpaceRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Space.Create
{
    internal class CreateSpaceHandler : IRequestHandler<CreateSpaceRequest, ResultResponse<SpaceModel>>
    {
        private readonly ISpaceRepository _spaceRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSpaceHandler(
            ISpaceRepository spaceRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _spaceRepository = spaceRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<SpaceModel>> Handle(
            CreateSpaceRequest request,
            CancellationToken cancellationToken)
        {

            var space = new SpaceModel
            {
                Title = request.Title,
                CreatedAt = DateTime.UtcNow,
                UserId = _userService.UserId!.Value
            };

            _spaceRepository.AddSpace(space);
            await _unitOfWork.SaveChanges(cancellationToken);

            return ResultResponse<SpaceModel>.Success(space);
        }

    }
}

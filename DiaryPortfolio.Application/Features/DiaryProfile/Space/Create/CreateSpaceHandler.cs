using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Space.Create
{
    internal class CreateSpaceHandler : IRequestHandler<CreateSpaceRequest, ResultResponse<SpaceModelDto>>
    {
        private readonly ISpaceRepository _spaceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSpaceHandler(
            ISpaceRepository spaceRepository,
            IUnitOfWork unitOfWork)
        {
            _spaceRepository = spaceRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<SpaceModelDto>> Handle(
            CreateSpaceRequest request,
            CancellationToken cancellationToken)
        {

            var response = await _spaceRepository.AddSpace(request.Title);

            if(response.Error != Error.None)
            {
                return ResultResponse<SpaceModelDto>.Failure(response.Error);
            }

            await _unitOfWork.SaveChanges(cancellationToken);

            return ResultResponse<SpaceModelDto>.Success(
                response.Result.ToSpaceModelDto());
        }

    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetMyLayout
{
    internal class GetMyLayoutHandler : IRequestHandler<GetMyLayoutRequest, ResultResponse<List<PortfolioSectionModelDto>>>
    {
        private readonly IPortfolioSectionRepository _portfolioSectionRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public GetMyLayoutHandler(
            IPortfolioSectionRepository portfolioSectionRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _portfolioSectionRepository = portfolioSectionRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<List<PortfolioSectionModelDto>>> Handle(
            GetMyLayoutRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _portfolioSectionRepository.GetMyLayout(
                _userService.PortfolioProfileId ?? Guid.Empty);

            if (response.Error != Error.None)
            {
                return ResultResponse<List<PortfolioSectionModelDto>>.Failure(response.Error);
            }

            // GetMyLayout may have backfilled section types that didn't exist
            // for this profile yet - persist those before returning.
            await _unitOfWork.SaveChanges(cancellationToken);

            return ResultResponse<List<PortfolioSectionModelDto>>.Success(
                [.. response.Result.Select(s => s.ToPortfolioSectionModelDto())]
            );
        }
    }
}

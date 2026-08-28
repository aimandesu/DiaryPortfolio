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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.SaveLayout
{
    internal class SaveLayoutHandler : IRequestHandler<SaveLayoutRequest, ResultResponse<List<PortfolioSectionModelDto>>>
    {
        private readonly IPortfolioSectionRepository _portfolioSectionRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public SaveLayoutHandler(
            IPortfolioSectionRepository portfolioSectionRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _portfolioSectionRepository = portfolioSectionRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<List<PortfolioSectionModelDto>>> Handle(
            SaveLayoutRequest request,
            CancellationToken cancellationToken)
        {
            var placements = request.Placements
                .Select(p => new SectionPlacement(p.SectionId, p.X, p.Y, p.W, p.H))
                .ToList();

            var response = await _portfolioSectionRepository.SaveLayout(
                _userService.PortfolioProfileId ?? Guid.Empty,
                placements);

            if (response.Error != Error.None)
            {
                return ResultResponse<List<PortfolioSectionModelDto>>.Failure(response.Error);
            }

            await _unitOfWork.SaveChanges(cancellationToken);

            return ResultResponse<List<PortfolioSectionModelDto>>.Success(
                [.. response.Result.Select(s => s.ToPortfolioSectionModelDto())]
            );
        }
    }
}

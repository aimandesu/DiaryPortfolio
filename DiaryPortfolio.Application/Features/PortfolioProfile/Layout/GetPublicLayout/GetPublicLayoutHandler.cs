using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using Mediator;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.GetPublicLayout
{
    internal class GetPublicLayoutHandler : IRequestHandler<GetPublicLayoutRequest, ResultResponse<List<PortfolioSectionModelDto>>>
    {
        private readonly IPortfolioSectionRepository _portfolioSectionRepository;

        public GetPublicLayoutHandler(
            IPortfolioSectionRepository portfolioSectionRepository)
        {
            _portfolioSectionRepository = portfolioSectionRepository;
        }

        public async ValueTask<ResultResponse<List<PortfolioSectionModelDto>>> Handle(
            GetPublicLayoutRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _portfolioSectionRepository.GetPublicLayout(request.Username);

            if (response.Error != Error.None)
            {
                return ResultResponse<List<PortfolioSectionModelDto>>.Failure(response.Error);
            }

            return ResultResponse<List<PortfolioSectionModelDto>>.Success(
                [.. response.Result.Select(s => s.ToPortfolioSectionModelDto())]
            );
        }
    }
}

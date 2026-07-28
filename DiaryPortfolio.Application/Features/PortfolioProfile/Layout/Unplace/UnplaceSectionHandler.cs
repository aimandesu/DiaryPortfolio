using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Layout.Unplace
{
    internal class UnplaceSectionHandler : IRequestHandler<UnplaceSectionRequest, ResultResponse<PortfolioSectionModelDto>>
    {
        private readonly IPortfolioSectionRepository _portfolioSectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnplaceSectionHandler(
            IPortfolioSectionRepository portfolioSectionRepository,
            IUnitOfWork unitOfWork)
        {
            _portfolioSectionRepository = portfolioSectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<PortfolioSectionModelDto>> Handle(
            UnplaceSectionRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _portfolioSectionRepository.UnplaceSection(request.Id);

                if (response.Error != Error.None)
                {
                    return ResultResponse<PortfolioSectionModelDto>.Failure(response.Error);
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<PortfolioSectionModelDto>.Success(
                    response.Result.ToPortfolioSectionModelDto());
            }
            catch (AppException ex)
            {
                return ResultResponse<PortfolioSectionModelDto>.Failure(
                    new Error(ex.StatusCode, ex.Message));
            }
        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using Mediator;

namespace DiaryPortfolio.Application.Features.Onboarding.Portfolio.Get;

public class GetPortfolioOnboardingHandler(
    IOnboardingRepository onboardingRepository) 
    : IRequestHandler<GetPortfolioOnboardingRequest, ResultResponse<bool>>
{
    
    
    public async ValueTask<ResultResponse<bool>> Handle(
        GetPortfolioOnboardingRequest request, 
        CancellationToken cancellationToken)
    {
        return await onboardingRepository.GetPortfolioOnboarding();
    }
}
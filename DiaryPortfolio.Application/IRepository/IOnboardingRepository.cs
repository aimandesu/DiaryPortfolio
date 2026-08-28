using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.IRepository;

public interface IOnboardingRepository
{
    Task<ResultResponse<OnboardingSubmission>> CreatePortfolioOnboarding(
        OnboardingSubmission request);

    Task<ResultResponse<bool>> GetPortfolioOnboarding();
}
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using Mediator;

namespace DiaryPortfolio.Application.Features.Onboarding.Portfolio.Create;

public sealed record CreatePortfolioOnboardingRequest(
    OnboardingSubmission OnboardingSubmission) : IRequest<ResultResponse<OnboardingSubmission>>;
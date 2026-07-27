using DiaryPortfolio.Application.Common;
using Mediator;

namespace DiaryPortfolio.Application.Features.Onboarding.Portfolio.Get;

public sealed record GetPortfolioOnboardingRequest()
    : IRequest<ResultResponse<bool>>;
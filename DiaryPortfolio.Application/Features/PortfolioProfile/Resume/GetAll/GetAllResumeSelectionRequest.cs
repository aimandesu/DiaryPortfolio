using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Reporting;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.GetAll;

public sealed record class GetAllResumeSelectionRequest(
    string Username) : IRequest<ResultResponse<ResumeSelectionViewModel>>;
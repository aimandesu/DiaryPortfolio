using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Reporting;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Enum;
using Mediator;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.GetAll;

public class GetAllResumeSelectionHandler : IRequestHandler<GetAllResumeSelectionRequest ,ResultResponse<ResumeSelectionViewModel>>
{
    private readonly IPortfolioProfileRepository _portfolioProfileRepository;
    private readonly IResumeRepository _resumeRepository;
    private readonly IUserRepository _userRepository;

    public GetAllResumeSelectionHandler(
        IPortfolioProfileRepository portfolioProfileRepository,
        IResumeRepository resumeRepository,
        IUserRepository userRepository)
    {
        _portfolioProfileRepository = portfolioProfileRepository;
        _resumeRepository = resumeRepository;
        _userRepository = userRepository;
    }
    
    public async ValueTask<ResultResponse<ResumeSelectionViewModel>> Handle(
        GetAllResumeSelectionRequest request, 
        CancellationToken cancellationToken)
    {
        var userModel = await _userRepository.GetUserByUsername(
            request.Username, 
            ProfileType.Portfolio);

        var id = userModel?.Id;
        
        var response = await _portfolioProfileRepository.GenerateResume(id.ToString() ?? "");
        
        var resume = await _resumeRepository.GetResumeTemplates();

        if (resume.Error != Error.None)
        {
            return ResultResponse<ResumeSelectionViewModel>.Failure(resume.Error);
        }
            
        var model = new ResumeSelectionViewModel
        {
            UserId = id.ToString() ?? "",
            ResumeData = response.Result,
            Templates = resume.Result
        };
        
        return ResultResponse<ResumeSelectionViewModel>.Success(model);
       
    }
}
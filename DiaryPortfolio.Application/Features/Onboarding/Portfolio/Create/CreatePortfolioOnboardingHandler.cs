using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;

namespace DiaryPortfolio.Application.Features.Onboarding.Portfolio.Create;

public class CreatePortfolioOnboardingHandler(
    IOnboardingRepository onboardingRepository,
    IFileHandlerRepository fileHandlerRepository)
    : IRequestHandler<CreatePortfolioOnboardingRequest, ResultResponse<OnboardingSubmission>>
{
    public async ValueTask<ResultResponse<OnboardingSubmission>> Handle(
        CreatePortfolioOnboardingRequest request, 
        CancellationToken cancellationToken)
    {
        var mediaType = MediaType.PortfolioProfile;
        var streams = new List<MediaStream>();

        if (request.OnboardingSubmission.Profile.ProfileFileSteam is not null)
        {
            streams.Add(request.OnboardingSubmission.Profile.ProfileFileSteam);
        }
        
        if (request.OnboardingSubmission.Profile.ResumeFileStream is not null)
        {
            streams.Add(request.OnboardingSubmission.Profile.ResumeFileStream);
        }
        
        var uploadResult = await fileHandlerRepository.DistributeFiles(
            streams,
            mediaType
        );
        
        if (uploadResult.Error != Error.None)
        {
            return ResultResponse<OnboardingSubmission>.Failure(uploadResult.Error);
        }

        var media = uploadResult.Result.ExtractMedia();
        
        request.OnboardingSubmission
            .Profile.ProfilePhoto = media.Photos.FirstOrDefault();
        
        //do also for resume if available to include

        try
        {
            var response = await onboardingRepository.CreatePortfolioOnboarding(
                request.OnboardingSubmission
                );
            
            return ResultResponse<OnboardingSubmission>.Success(
                response.Result);
        }
        catch (AppException ex)
        {
            fileHandlerRepository.DeleteFiles([
                media.Photos.FirstOrDefault()?.Url ?? "",
                media.Files.FirstOrDefault()?.Url ?? "",
            ]);
            
            return ResultResponse<OnboardingSubmission>.Failure(
                new Error(
                    System.Net.HttpStatusCode.Conflict, 
                    ex.ToString())
            );
        }
        
    }
}
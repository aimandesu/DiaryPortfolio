using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Create;

namespace DiaryPortfolio.Application.Request;

public class OnboardingSubmission
{
    public required ProfileUpload Profile { get; set; }
    public List<EducationUpload> Educations { get; set; } = [];
    public List<CreateSkillRequest> Skills { get; set; } = [];
    public List<ExperienceUpload> Experiences { get; set; } = [];

    private bool IsCompleted => Educations.Count > 0 
                                && Skills.Count > 0 
                                &&  Experiences.Count > 0;
}
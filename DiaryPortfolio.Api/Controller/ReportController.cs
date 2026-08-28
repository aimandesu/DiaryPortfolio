using DiaryPortfolio.Application.DTOs.Reporting;
using DiaryPortfolio.Application.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("view/report")] // -> all of this is just direct testing, not for usage in production
    public class ReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IPortfolioProfileRepository _portfolioProfileRepository;
        private readonly IResumeRepository _resumeRepository;
        public ReportController(
            IExperienceRepository experienceRepository, 
            IPortfolioProfileRepository portfolioProfileRepository,
            IResumeRepository resumeRepository)
        {
            _experienceRepository = experienceRepository;
            _portfolioProfileRepository = portfolioProfileRepository;
            _resumeRepository = resumeRepository;
        }

        [HttpGet("experience/{id}")]
        public async Task<IActionResult> ExperiencePreview(
            string id, 
            CancellationToken cancellationToken)
        {
            var response = await _experienceRepository.GetAll(new Guid(id));
            return View("~/Views/Examples/ExperienceReport.cshtml", response);
        }
        
        [HttpGet("resume/gallery/{userId}")]
        public async Task<IActionResult> Gallery(string userId)
        {
            var response = await _portfolioProfileRepository.GenerateResume(userId);
        
            var resume = await _resumeRepository.GetResumeTemplates();
            
            var model = new ResumeSelectionViewModel
            {
                UserId = userId,
                ResumeData = response.Result,
                Templates = resume.Result
            };
        
            return View("~/Views/Resume/ResumeSelection.cshtml", model);
        }

        [HttpGet("resume/{userId}")]
        public async Task<IActionResult> ReportPreview(
            string userId,
            [FromQuery] string template = "classic")
        {
            var response = await _portfolioProfileRepository.GenerateResume(userId);

            var viewName = template switch
            {
                "modern" => "~/Views/Resume/Templates/_Modern.cshtml",
                _        => "~/Views/Resume/Templates/_Classic.cshtml"
            };

            return View(viewName, response.Result);
        }


    }
}

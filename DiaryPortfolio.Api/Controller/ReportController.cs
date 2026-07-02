using DiaryPortfolio.Application.DTOs.Reporting;
using DiaryPortfolio.Application.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("view/report")]
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
            return View("~/Views/Pdf/ExperienceReport.cshtml", response);
        }
        
        [HttpGet("resume/gallery/{id}")]
        public async Task<IActionResult> Gallery(string id)
        {
            var response = await _portfolioProfileRepository.GenerateResume(id);

            var resume = await _resumeRepository.GetResumeTemplates();
            
            var model = new ResumeSelectionViewModel
            {
                UserId = id,
                ResumeData = response.Result,
                Templates = resume.Result
            };

            return View("~/Views/PDF/resume/ResumeSelection.cshtml", model);
        }

        [HttpGet("resume/{id}")]
        public async Task<IActionResult> ReportPreview(
            string id,
            [FromQuery] string template = "classic")
        {
            var response = await _portfolioProfileRepository.GenerateResume(id);

            var viewName = template switch
            {
                "modern" => "~/Views/PDF/Templates/_Modern.cshtml",
                _        => "~/Views/PDF/Templates/_Classic.cshtml"
            };

            return View(viewName, response.Result);
        }


    }
}

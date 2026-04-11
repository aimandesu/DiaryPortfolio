using DiaryPortfolio.Application.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiaryPortfolio.Api.Controller
{
    [Route("view/report")]
    public class ReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IPortfolioProfileRepository _portfolioProfileRepository;
        public ReportController(
            IExperienceRepository experienceRepository, 
            IPortfolioProfileRepository portfolioProfileRepository)
        {
            _experienceRepository = experienceRepository;
            _portfolioProfileRepository = portfolioProfileRepository;
        }

        [HttpGet("experience/{id}")]
        public async Task<IActionResult> ExperiencePreview(
            string id, 
            CancellationToken cancellationToken)
        {
            var response = await _experienceRepository.GetAll(new Guid(id));
            return View("~/Views/Pdf/ExperienceReport.cshtml", response);
        }

        [HttpGet("resume/{id}")]
        public async Task<IActionResult> ReportPreview(
            string id,
            CancellationToken cancellationToken)
        {
            var response = await _portfolioProfileRepository.GenerateResume(id);
            return View("~/Views/Pdf/ResumeReport.cshtml", response.Result);
        }


    }
}

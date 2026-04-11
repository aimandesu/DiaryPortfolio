using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Reporting.Experience
{
    internal class CreateExperienceReportHandler : IRequestHandler<CreateExperienceReportRequest, byte[]>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IRazorViewRenderer _razorRenderer;
        private readonly IPdfGeneratorService _pdfGenerator;
        private readonly IUserService _userService;

        public CreateExperienceReportHandler(
            IExperienceRepository experienceRepository,
            IRazorViewRenderer razorRenderer,
            IPdfGeneratorService pdfGenerator,
            IUserService userService)
        {
            _experienceRepository = experienceRepository;
            _razorRenderer = razorRenderer;
            _pdfGenerator = pdfGenerator;
            _userService = userService;
        }
        public async ValueTask<byte[]> Handle(
            CreateExperienceReportRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _experienceRepository.GetAll(new Guid(request.Id));

            var html = await _razorRenderer.RenderViewToStringAsync("Pdf/ExperienceReport", response);
            return await _pdfGenerator.GenerateFromHtmlAsync(html);
        }
    }
}

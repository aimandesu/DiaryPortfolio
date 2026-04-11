using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Reporting.Resume
{
    internal class CreateResumeReportHandler : IRequestHandler<CreateResumeReportRequest, byte[]>
    {
        private readonly IPortfolioProfileRepository _portfolioProfileRepository;
        private readonly IRazorViewRenderer _razorRenderer;
        private readonly IPdfGeneratorService _pdfGenerator;
        public CreateResumeReportHandler(
            IPortfolioProfileRepository portfolioProfileRepository,
            IRazorViewRenderer razorRenderer,
            IPdfGeneratorService pdfGenerator)
        {
            _portfolioProfileRepository = portfolioProfileRepository;
            _razorRenderer = razorRenderer;
            _pdfGenerator = pdfGenerator;
        }

        public async ValueTask<byte[]> Handle(
            CreateResumeReportRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _portfolioProfileRepository.GenerateResume(request.UserId);

            var html = await _razorRenderer.RenderViewToStringAsync("Pdf/ResumeReport", response.Result);

            return await _pdfGenerator.GenerateFromHtmlAsync(html);
        }
    }
}

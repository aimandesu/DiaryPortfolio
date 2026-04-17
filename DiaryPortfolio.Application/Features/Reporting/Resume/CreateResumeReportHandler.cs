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
        private readonly IResumeRepository _resumeRepository;

        public CreateResumeReportHandler(
            IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        public async ValueTask<byte[]> Handle(
            CreateResumeReportRequest request, 
            CancellationToken cancellationToken)
        {
            return await _resumeRepository.GenerateResumeReport(request.UserId);
        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Reporting.Resume
{
    public sealed record class CreateResumeReportRequest(
        string UserId
    ) : IRequest<byte[]>, 
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

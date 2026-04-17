using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Delete
{
    public sealed record class DeleteResumeRequest(
        string resumeId) : IRequest<ResultResponse<ResumeModel>>;
}

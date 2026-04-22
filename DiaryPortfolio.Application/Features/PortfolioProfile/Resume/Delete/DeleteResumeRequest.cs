using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Delete
{
    [RequireOwnership(typeof(ResumeModel))]
    public sealed record class DeleteResumeRequest(
        string Id
    ) : IRequest<ResultResponse<ResumeModel>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

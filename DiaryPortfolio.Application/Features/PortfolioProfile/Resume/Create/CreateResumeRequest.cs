using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Create
{
    public sealed record class CreateResumeRequest(
        string TemplateId
    ) : IRequest<ResultResponse<ResumeModel>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

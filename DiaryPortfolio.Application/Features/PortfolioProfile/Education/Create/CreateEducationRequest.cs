using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.Create
{
    public sealed record class CreateEducationRequest(
        EducationUpload EducationUpload
    ) : IRequest<ResultResponse<EducationModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

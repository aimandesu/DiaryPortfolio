using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.Delete
{
    [RequireOwnership(typeof(EducationModel))]
    public sealed record class DeleteEducationRequest(
        string Id
    ) : IRequest<ResultResponse<EducationModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.GetAll
{
    public sealed record class GetAllEducationRequest(
        string Username
    ) : IRequest<ResultResponse<List<EducationModelDto>>>;
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.CustomUrl.Create
{
    public sealed record class CreateCustomUrlRequest(
        string Name,
        string Url
    ) : IRequest<ResultResponse<CustomUrlModelDto>>,
        IRequireAuthentication,
        IRequirePortfolioProfile;
}

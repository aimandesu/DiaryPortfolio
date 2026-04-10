using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.GetAll
{
    public sealed record class GetAllMediaRequest(
        QuerySearchObject QuerySearchObject,
        String Username) : IRequest<ResultResponse<Pagination<MediaModelDto>>>;
}

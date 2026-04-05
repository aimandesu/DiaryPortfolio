using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Space;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Space.Create
{
    public sealed record class CreateSpaceRequest(
        string Title) : IRequest<ResultResponse<SpaceModelDto>>, IRequireAuthentication;
}

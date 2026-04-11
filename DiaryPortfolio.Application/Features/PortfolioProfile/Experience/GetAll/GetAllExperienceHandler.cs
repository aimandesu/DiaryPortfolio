using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.GetAll
{
    internal class GetAllExperienceHandler : IRequestHandler<GetAllExperienceRequest, ResultResponse<List<ExperienceModelDto>>>
    {
        private readonly IExperienceRepository _experienceRepository;

        public GetAllExperienceHandler(
            IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public async ValueTask<ResultResponse<List<ExperienceModelDto>>> Handle(
            GetAllExperienceRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _experienceRepository
                    .GetAll(new Guid(request.UserId));

                return ResultResponse<List<ExperienceModelDto>>.Success(
                    response.Select(
                        e => e.ToExperienceModelDto()).ToList());

            }
            catch (AppException ex)
            {
                return ResultResponse<List<ExperienceModelDto>>.Failure(
                   new Error(ex.StatusCode, ex.Message)
               );
            }
        }
    }
}

using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.GetAll
{
    internal class GetAllEducationHandler : IRequestHandler<GetAllEducationRequest, ResultResponse<List<EducationModelDto>>>
    {
        private readonly IEducationRepository _educationRepository;

        public GetAllEducationHandler(
            IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async ValueTask<ResultResponse<List<EducationModelDto>>> Handle(
            GetAllEducationRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _educationRepository.GetAllEducation(request.Username);

            if (response.Error != Error.None)
            {
                return ResultResponse<List<EducationModelDto>>.Failure(response.Error);
            }

            return ResultResponse<List<EducationModelDto>>.Success(
                [.. response.Result.Select(e => e.ToEducationModelDto())]
            );

        }
    }
}

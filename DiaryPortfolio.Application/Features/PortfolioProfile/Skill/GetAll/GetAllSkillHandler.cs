using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Skill.GetAll
{
    internal class GetAllSkillHandler : IRequestHandler<GetAllSkillRequest, ResultResponse<List<SkillModelDto>>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetAllSkillHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async ValueTask<ResultResponse<List<SkillModelDto>>> Handle(
            GetAllSkillRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _skillRepository
                    .GetAllSkill(request.Username);

                return ResultResponse<List<SkillModelDto>>.Success(
                    response.Result.Select(
                        e => e.ToSkillModelDto()).ToList());

            }
            catch (AppException ex)
            {
                return ResultResponse<List<SkillModelDto>>.Failure(
                  new Error(ex.StatusCode, ex.Message)
              );
            }
        }
    }
}

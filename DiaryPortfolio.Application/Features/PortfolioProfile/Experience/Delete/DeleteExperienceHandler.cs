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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Delete
{
    internal class DeleteExperienceHandler : IRequestHandler<DeleteExperienceRequest, ResultResponse<ExperienceModelDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExperienceHandler(
            IExperienceRepository experienceRepository,
            IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<ExperienceModelDto>> Handle(
            DeleteExperienceRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _experienceRepository.Delete(new Guid(request.Id));
                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<ExperienceModelDto>.Success(response.ToExperienceModelDto());

            }
            catch (AppException ex)
            {
                return ResultResponse<ExperienceModelDto>.Failure(
                   new Error(ex.StatusCode, ex.Message)
               );
            }
        }
    }
}

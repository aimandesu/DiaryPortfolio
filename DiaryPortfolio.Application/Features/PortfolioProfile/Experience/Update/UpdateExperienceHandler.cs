using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Update
{
    internal class UpdateExperienceHandler : IRequestHandler<UpdateExperienceRequest, ResultResponse<ExperienceModelDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExperienceHandler(
            IExperienceRepository experienceRepository,
            IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<ExperienceModelDto>> Handle(
            UpdateExperienceRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var location = new LocationModel
                {
                    Name = request.ExperienceUpload.Name,
                    Latitude = request.ExperienceUpload.Latitude,
                    Longitude = request.ExperienceUpload.Longitude
                };

                var entity = new ExperienceModel
                {
                    Id = new Guid(request.Id),
                    Company = request.ExperienceUpload.Company,
                    Role = request.ExperienceUpload.Role,
                    Description = request.ExperienceUpload.Description,
                    StartDate = request.ExperienceUpload.StartDate,
                    EndDate = request.ExperienceUpload.EndDate,
                    Location = location,
                };

                var response = await _experienceRepository.Update(entity);
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

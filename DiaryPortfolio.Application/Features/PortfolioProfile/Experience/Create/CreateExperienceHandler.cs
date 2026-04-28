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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Experience.Create
{
    internal class CreateExperienceHandler : IRequestHandler<CreateExperienceRequest, ResultResponse<ExperienceModelDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        

        public CreateExperienceHandler(
            IExperienceRepository experienceRepository,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async ValueTask<ResultResponse<ExperienceModelDto>> Handle(
            CreateExperienceRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                //var location = new LocationModel
                //{
                //    AddressLine1 = request.ExperienceUpload.Name,
                //    Latitude = request.ExperienceUpload.Latitude,
                //    Longitude = request.ExperienceUpload.Longitude
                //};

                var entity = new ExperienceModel
                {
                    Id = Guid.NewGuid(),
                    Company = request.ExperienceUpload.Company,
                    Role = request.ExperienceUpload.Role,
                    Description = request.ExperienceUpload.Description,
                    StartDate = request.ExperienceUpload.StartDate,
                    EndDate = request.ExperienceUpload.EndDate,
                    Location = request.ExperienceUpload.Location,
                    PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                };

                var response = await _experienceRepository.Create(entity);
                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<ExperienceModelDto>.Success(response.ToExperienceModelDto());
            }
            catch (AppException ex) {
                
                return ResultResponse<ExperienceModelDto>.Failure(
                    new Error(ex.StatusCode, ex.Message)
                );
            }
        }

    }
}

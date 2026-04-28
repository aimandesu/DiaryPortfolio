using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class EducationRepository : IEducationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISelectionHelper _selectionHelper;
        private readonly IUserService _userService;

        public EducationRepository(
            ApplicationDbContext context,
            ISelectionHelper selectionHelper,
            IUserService userService)
        {
            _context = context;
            _selectionHelper = selectionHelper;
            _userService = userService;
        }

        public async Task<ResultResponse<EducationModel>> CreateEducation(
            EducationUpload educationUpload, 
            FileModel? file)
        {
            try
            {
                var selectionResult = await _selectionHelper.GetSelectionResultAsync(FilesEnum.Education);

                var educationFile = new FileModel
                {
                    Url = file?.Url ?? "",
                    SelectionId = selectionResult?.Id ?? Guid.Empty
                };

                var location = new LocationModel
                {
                    AddressLine1 = educationUpload.Location?.Name ?? "",
                    Latitude = Convert.ToDouble(educationUpload.Location?.Latitude),
                    Longitude = Convert.ToDouble(educationUpload.Location?.Longitude)
                };

                var education = new EducationModel
                {
                    Institution = educationUpload.Institution,
                    Achievement = educationUpload.Achievement,
                    StartDate = educationUpload.StartDate,
                    EndDate = educationUpload.EndDate,
                    PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                    LocationId = location.Id,
                    Location = location,
                    FileId = educationFile?.Id,
                    EducationFile = educationFile,
                    SelectionId = selectionResult?.Id ?? Guid.Empty,
                };

                _context.Educations.Add(education);

                return ResultResponse<EducationModel>.Success(education);
            }
            catch (AppException ex)
            {
                return ResultResponse<EducationModel>.Failure(
                   new Error(ex.StatusCode, ex.Message)
               );
            }

        }

        public async Task<ResultResponse<EducationModel>> DeleteEducation(
            string resumeId)
        {
            var education = await _context.Educations
                .Include(f => f.EducationFile)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(e => e.Id == new Guid(resumeId));

            if (education == null) {
                return ResultResponse<EducationModel>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.NotFound,
                        "No Education with the id provided found"));
            }

            var response = new EducationModel
            {
                Institution = education?.Institution ?? "",
                Achievement = education?.Achievement ?? "",
                StartDate = education?.StartDate,
                EndDate = education?.EndDate,
                PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                LocationId = education?.LocationId ?? Guid.Empty,
                Location = education?.Location,
                FileId = education?.FileId,
                EducationFile = education?.EducationFile,
            };

            if (education?.Location != null)
            {
                _context.Locations.Remove(education.Location);
            }

            if (education?.EducationFile != null)
            {
                _context.Files.Remove(education?.EducationFile);
            }

            _context.Educations.Remove(education);

            return ResultResponse<EducationModel>.Success(response);

        }

        public async Task<ResultResponse<List<EducationModel>>> GetAllEducation(
            string userName)
        {
            try
            {
                var query = await _context.Educations
                    .Include(l => l.Location)
                    .Include(e => e.EducationFile)
                    .Where(u => u.PortfolioProfile.User.UserName == userName)
                    .ToListAsync();

                return ResultResponse<List<EducationModel>>.Success(query);
            }
            catch (Exception ex) {
                return ResultResponse<List<EducationModel>>.Failure(
                    new Error(
                        HttpStatusCode.UnprocessableContent, 
                        ex.Message)
                    );
            }

        }
    }
}

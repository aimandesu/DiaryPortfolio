using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class PortfolioProfileRepository : IPortfolioProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PortfolioProfileRepository(
            ApplicationDbContext context,
            IUserService userService
        )
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ResultResponse<ResumeReportDto>> GenerateResume(string userId)
        {
            var query = from user in _context.Users
                            where user.Id == new Guid(userId)

                            join portfolio in _context.PortfolioProfile
                                on user.Id equals portfolio.UserId

                            join photos in _context.Photos
                                on portfolio.ProfilePhotoId equals photos.Id into photoGroup
                            from photos in photoGroup.DefaultIfEmpty()

                            join experience in _context.Experiences
                                on portfolio.Id equals experience.PortfolioProfileId into userExperiences

                           select new ResumeReportDto
                           {
                               User = new UserModelDto
                               {
                                   Name = portfolio.Name,
                                   Email = user.Email,
                                   UserName = user.UserName,
                                   Age = portfolio.Age,
                                   Title = portfolio.Title,
                                   About = portfolio.About,
                                   Address = portfolio.Address,
                                   ProfilePhoto = photos == null ? null : new PhotoModel
                                   {
                                       Id = photos.Id,
                                       Url = photos.Url,
                                       Mime = photos.Mime,
                                       Width = photos.Width,
                                       Height = photos.Height,
                                       Size = photos.Size,
                                   }
                               },
                               Experiencs = userExperiences.Select(e => new ExperienceModelDto
                               {
                                   Company = e.Company,
                                   Role = e.Role,
                                   Description = e.Description,
                                   StartDate = e.StartDate,
                                   EndDate = e.EndDate,
                                   Location = e.Location == null ? null : new LocationModel
                                   {
                                       Id = e.Location.Id,
                                       Name = e.Location.Name,
                                       Latitude = e.Location.Latitude,
                                       Longitude = e.Location.Longitude
                                   }
                               }).ToList(),
                           };

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return ResultResponse<ResumeReportDto>.Failure(
                    new Error(
                        HttpStatusCode.NotFound, 
                        "User or Portfolio not found."));
            }

            return ResultResponse<ResumeReportDto>.Success(result);

        }

        public async Task<ResultResponse<UserModel>> UploadProfile(
            ProfileUpload profileUpload, 
            PhotoModel? profilePhoto, 
            FileModel? resumeFile)
        {
            var userId = _userService.UserId!.Value;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return ResultResponse<UserModel>.Failure(
                    new Error(HttpStatusCode.NotFound, "User not found")
                );

            var existingMedia = await _context.Users
                .Include(d => d.DiaryProfile)
                .Include(d => d.PortfolioProfile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpsertPortfolioProfile @UserId, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @Name, @Age, @Title, @About, @Address, @LocationName, @Latitude, @Longitude, @PhotoUrl, @PhotoMime, @PhotoWidth, @PhotoHeight, @PhotoSize, @FileUrl, @FileDescription",
                new SqlParameter("@UserId", userId),
                new SqlParameter("@UserName", profileUpload.UserName),
                new SqlParameter("@NormalizedUserName", profileUpload.UserName.ToUpperInvariant()),
                new SqlParameter("@Email", profileUpload.Email),
                new SqlParameter("@NormalizedEmail", profileUpload.Email.ToUpperInvariant()),
                new SqlParameter("@Name", profileUpload.Name),
                new SqlParameter("@Age", (object?)profileUpload.Age ?? DBNull.Value),
                new SqlParameter("@Title", profileUpload.Title),
                new SqlParameter("@About", profileUpload.About),
                new SqlParameter("@Address", profileUpload.Address),
                new SqlParameter("@LocationName", profileUpload.Location?.Name ?? ""),
                new SqlParameter("@Latitude", ""),
                new SqlParameter("@Longitude", ""),
                new SqlParameter("@PhotoUrl", (object?)profilePhoto?.Url ?? DBNull.Value),
                new SqlParameter("@PhotoMime", (object?)profilePhoto?.Mime ?? DBNull.Value),
                new SqlParameter("@PhotoWidth", (object?)profilePhoto?.Width ?? DBNull.Value),
                new SqlParameter("@PhotoHeight", (object?)profilePhoto?.Height ?? DBNull.Value),
                new SqlParameter("@PhotoSize", (object?)profilePhoto?.Size ?? DBNull.Value),
                new SqlParameter("@FileUrl", (object?)resumeFile?.Url ?? DBNull.Value),
                new SqlParameter("@FileDescription", (object?)resumeFile?.Description ?? DBNull.Value)
            );

            return ResultResponse<UserModel>.Success(user);
        }
    }
}

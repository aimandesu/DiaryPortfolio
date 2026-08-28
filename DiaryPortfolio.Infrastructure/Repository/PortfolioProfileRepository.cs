using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.DTOs.Reporting;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class PortfolioProfileRepository : IPortfolioProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        //private readonly IUserService _userService;

        public PortfolioProfileRepository(
            ApplicationDbContext context,
            IFileHandlerRepository fileHandlerRepository
            //IUserService userService
        )
        {
            _context = context;
            _fileHandlerRepository = fileHandlerRepository;
            //_userService = userService;
        }

        public async Task<ResultResponse<ResumeReportDto>> GenerateResume(string userId)
        {
            var query = from user in _context.Users
                            where user.Id == new Guid(userId)

                            join portfolio in _context.PortfolioProfile
                                on user.Id equals portfolio.UserId
                                
                            join resume in _context.Resume
                                on  portfolio.Id equals resume.PortfolioProfileId into resumeGroup
                            from resume in resumeGroup.DefaultIfEmpty()
                                
                            join file in _context.Files
                                on resume.FileId equals file.Id into fileGroup
                            from file in fileGroup.DefaultIfEmpty()

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
                                   },
                                   Resume = resume == null ? null : new ResumeModelDto
                                   {
                                       Id = resume.Id,
                                       FileId = resume.FileId,
                                       ResumeFile = file,
                                       TemplateId = resume.TemplateId
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
                                       AddressLine1 = e.Location.AddressLine1,
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
            FileModel? resumeFile,
            UserModel? user)
        {

            try
            {
                if (user?.PortfolioProfile == null)
                    return ResultResponse<UserModel>.Failure(
                        new Error(HttpStatusCode.NotFound, "User not found")
                    );

       
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", user.Id, DbType.Guid);
                parameters.Add("@UserName", profileUpload.UserName, DbType.String);
                parameters.Add("@NormalizedUserName", profileUpload.UserName.ToUpperInvariant(), DbType.String);
                parameters.Add("@Email", profileUpload.Email, DbType.String);
                parameters.Add("@NormalizedEmail", profileUpload.Email.ToUpperInvariant(), DbType.String);
                parameters.Add("@Name", profileUpload.Name, DbType.String);
                parameters.Add("@Age", profileUpload.Age, DbType.Int32); // Dapper handles null cleanly
                parameters.Add("@Title", profileUpload.Title, DbType.String);
                parameters.Add("@About", profileUpload.About, DbType.String);
                parameters.Add("@Address", profileUpload.Address, DbType.String);
                parameters.Add("@AddressLine1", profileUpload.Location?.AddressLine1 ?? "", DbType.String);
                parameters.Add("@AddressLine2", profileUpload.Location?.AddressLine2 ?? "", DbType.String);
                parameters.Add("@Latitude", profileUpload.Location?.Latitude.ToString() ?? "", DbType.String);
                parameters.Add("@Longitude", profileUpload.Location?.Longitude.ToString() ?? "", DbType.String);
                parameters.Add("@PhotoUrl", profilePhoto?.Url, DbType.String);
                parameters.Add("@PhotoMime", profilePhoto?.Mime, DbType.String);
                parameters.Add("@PhotoWidth", profilePhoto?.Width, DbType.Double);
                parameters.Add("@PhotoHeight", profilePhoto?.Height, DbType.Double);
                parameters.Add("@PhotoSize", profilePhoto?.Size, DbType.Double);
                parameters.Add("@FileUrl", resumeFile?.Url, DbType.String);
                parameters.Add("@FileDescription", resumeFile?.Description, DbType.String);

                // Extract the underlying database connection from EF Core
                var connection = _context.Database.GetDbConnection();

                // Execute and directly map the returned row into the nested property object
                var updatedProfile = await connection.QueryFirstOrDefaultAsync<PortfolioProfileModel>(
                    "sp_UpsertPortfolioProfile",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (updatedProfile == null)
                {
                    throw new Exception("Database failed to return the updated portfolio profile.");
                }

                // Hydrate your incoming user object with the fresh database results
                user.UserName = profileUpload.UserName;
                user.NormalizedUserName = profileUpload.UserName.ToUpperInvariant();
                user.Email = profileUpload.Email;
                user.NormalizedEmail = profileUpload.Email.ToUpperInvariant();
                
                user.PortfolioProfile = updatedProfile;
                
                // Retain deep structural relations from your memory inputs if needed
                user.PortfolioProfile.Location = profileUpload.Location;
                user.PortfolioProfile.ProfilePhoto = profilePhoto;
                user.PortfolioProfile.Resume = new ResumeModel
                {
                    ResumeFile = resumeFile
                };

                return ResultResponse<UserModel>.Success(user);

            }
            catch (Exception ex)
            {
                _fileHandlerRepository.DeleteFiles(
                    [
                        profilePhoto?.Url ?? "",
                        resumeFile?.Url ?? ""
                    ]);

                return ResultResponse<UserModel>.Failure(
                   new Error(HttpStatusCode.UnprocessableContent, ex.Message)
                );
            }
           
        }
    }
}

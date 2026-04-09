using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        //private readonly UserManager<UserModel> _userManager;

        public UserRepository(
            ApplicationDbContext context,
            IUserService userService
            //,
            //UserManager<UserModel> userManager
        )
        {
            _context = context;
            _userService = userService;
            //_userManager = userManager;
        }

        public async Task<UserModel?> GetUserByUsername(
            string username, ProfileType profileType)
        {
            var query = _context.Users.AsQueryable();

            if (profileType == ProfileType.Diary)
            {
                query = query
                    .Include(u => u.DiaryProfile);
            }
            else if (profileType == ProfileType.Portfolio)
            {
                query = query
                    .Include(u => u.PortfolioProfile);
            }

            var user = await query
                .FirstOrDefaultAsync(u => u.UserName == username);

            return user;
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
using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Domain.Interfaces;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
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

        public async Task EnsureOwnerAsync(
            Guid resourceId, 
            Type resourceType, 
            Guid userId)
        {
            var entity = await _context.FindAsync(resourceType, resourceId);

            if (entity == null)
                throw new AppException("Resource not found.", HttpStatusCode.NotFound);

            if (entity is not IHaveOwner owned)
                throw new InvalidOperationException($"{resourceType.Name} does not implement IHaveOwner.");

            if (owned.OwnerId != userId)
                throw new AppException("You do not own this resource.", HttpStatusCode.Forbidden);
        }

        public async Task<UserModel?> GetUserByUserId(
            Guid userId, 
            ProfileType profileType)
        {
            var query = _context.Users.AsQueryable();

            if (profileType == ProfileType.Diary)
            {
                query = query
                    .Include(u => u.DiaryProfile);
            }
            
            if (profileType == ProfileType.Portfolio)
            {
                query = query
                    .Include(u => u.PortfolioProfile);
            }

            if (profileType == ProfileType.All)
            {
                query = query
                    .Include(u => u.PortfolioProfile)
                    .Include(u => u.DiaryProfile);
            }


            var user = await query
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
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
            
            if (profileType == ProfileType.Portfolio)
            {
                query = query
                    .Include(u => u.PortfolioProfile);
            }

            if (profileType == ProfileType.All)
            {
                query = query
                    .Include(u => u.PortfolioProfile)
                    .Include(u => u.DiaryProfile);
            }

            var user = await query
                .FirstOrDefaultAsync(u => u.UserName == username);

            return user;
        }
    }
}
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
using System.Reflection;
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
            Type resourceType)
        {
            object? entity;

            // Check if this type knows how to load its own ownership chain
            var withIncludesMethod = resourceType.GetMethod(
                nameof(IUserOwnerQuery.WithOwnerIncludes),
                BindingFlags.Public | BindingFlags.Static);

            if (withIncludesMethod != null)
            {
                // Use the type's own include query, then filter by PK
                var query = (IQueryable<object>)withIncludesMethod.Invoke(null, [_context])!;
                entity = await query
                    .Where(e => EF.Property<Guid>(e, "Id") == resourceId)
                    .FirstOrDefaultAsync();
            }
            else
            {
                // Fallback: simple FindAsync for flat entities
                entity = await _context.FindAsync(resourceType, resourceId);
            }

            if (entity == null)
            {
                return;
                //throw new AppException(
                //    "Resource not found.",
                //    HttpStatusCode.NotFound);
            }

            if (entity is not IUserOwner userOwner)
            {
                throw new InvalidOperationException(
                    $"{resourceType.Name} does not implement IUserOwner " +
                    $"but is using the implementation.");
            }

            if (userOwner.OwnerId != (_userService?.UserId ?? Guid.Empty))
            {
                throw new AppException(
                    "User does not own this resource.",
                    HttpStatusCode.Forbidden);
            }
                
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
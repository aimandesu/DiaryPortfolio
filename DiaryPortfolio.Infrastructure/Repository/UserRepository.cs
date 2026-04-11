using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.IRepository;
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
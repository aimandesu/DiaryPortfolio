using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DiaryPortfolio.Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        
        private readonly UserManager<UserModel> _userManager;

        public UserRepository(
            ApplicationDbContext context,
            UserManager<UserModel> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<UserModel?> SignUp(UserModel user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            return null;

        }
    }
}
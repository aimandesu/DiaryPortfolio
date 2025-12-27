using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

    }
}
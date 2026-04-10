using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class ExperienceRepository : BaseRepository<ExperienceModel>, IExperienceRepository
    {
        private IUserService _userService;
        public ExperienceRepository(
            ApplicationDbContext context,
            IUserService userService) 
            : base(context)
        {
            _userService = userService;
        }
        public override async Task<ExperienceModel> Create(ExperienceModel entity)
        {
            var exists = await _context.Experiences
            .AnyAsync(e => 
                e.PortfolioProfile.User.Id == _userService.UserId 
                && 
                e.Company == entity.Company);

            if (exists)
                throw new AppException("Experience with same company already exists.");

            return await base.Create(entity);
        }

        //public override async Task<ExperienceModel> Get(Guid id)
        //{
        //    return await _context.Experiences
        //        .Include(e => e.Company)
        //        .FirstOrDefaultAsync(e => e.Id == id);
        //}
    }
}

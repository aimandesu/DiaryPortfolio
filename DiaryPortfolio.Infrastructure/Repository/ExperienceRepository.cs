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
        //public override async Task<ExperienceModel> Create(ExperienceModel entity)
        //{
        //    var profile = await _context.PortfolioProfile
        //        .FirstOrDefaultAsync(p => p.UserId == _userService.UserId);

        //    //if (profile == null)
        //    //    throw new AppException("Portfolio profile not found");

        //    var exists = await _context.Experiences
        //        .AnyAsync(e =>
        //            e.PortfolioProfileId == profile.Id 
        //            //&&
        //            //e.Company == entity.Company
        //            );

        //    //if (exists)
        //    //    throw new AppException("Experience with same company already exists.");

        //    entity.PortfolioProfileId = profile.Id;

        //    return await base.Create(entity);
        //}

        public override async Task<List<ExperienceModel>> GetAll(Guid? id)
        {
            var entity = await _context.Experiences
                .Where(e => e.PortfolioProfile.UserId == id).ToListAsync();

            return entity;

        }

        public override async Task<ExperienceModel?> Delete(Guid id)
        {
            var entity = await _context.Experiences
                .Where(e => e.Id == id && e.PortfolioProfile.UserId == _userService.UserId)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new AppException("Experience does not belong to the user");

            _context.Experiences.Remove(entity);

            return entity;
        }

        public override async Task<ExperienceModel> Update(ExperienceModel model) {

            var entity = await _context.Experiences
                .Where(e => e.Id == model.Id && e.PortfolioProfile.UserId == _userService.UserId)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new AppException("Experience does not belong to the user");

            entity.Company = model.Company;
            entity.Role = model.Role;
            entity.Description = model.Description;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;

            if (entity.Location != null && model.Location != null)
            {
                entity.Location.AddressLine1 = model.Location.AddressLine1;
                entity.Location.Latitude = model.Location.Latitude;
                entity.Location.Longitude = model.Location.Longitude;
            }

            return entity;

        }

    }
}

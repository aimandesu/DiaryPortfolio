using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class SkillRepository : BaseRepository<SkillModel>, ISkillRepository
    {
        
        public SkillRepository(
            ApplicationDbContext context) 
            : base(context)
        {
        }

        public override async Task<SkillModel> Create(SkillModel entity)
        {
            return await base.Create(entity);
        }

        public override async Task<List<SkillModel>> GetAll(Guid? id)
        {
            var entity = await _context.Skills
                .Where(e => e.PortfolioProfile.Id == id).ToListAsync();

            return entity;
        }

    }
}

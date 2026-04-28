using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
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

        public async Task<ResultResponse<List<SkillModel>>> GetAllSkill(
            string username)
        {
            try
            {
                var query = await _context.Skills
                    .Include(s => s.Selection)
                        .ThenInclude(t => t.Type)
                    .Where(u => u.PortfolioProfile.User.UserName == username)
                    .ToListAsync();

                return ResultResponse<List<SkillModel>>.Success(query);

            }
            catch (Exception ex)
            {
                return ResultResponse<List<SkillModel>>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.BadRequest,
                        ex.Message)
                    );
            }
        }
    }
}

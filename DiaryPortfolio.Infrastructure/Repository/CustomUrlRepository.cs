using DiaryPortfolio.Application.IRepository;
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
    internal class CustomUrlRepository : BaseRepository<CustomUrlModel>, ICustomUrlRepository
    {
        public CustomUrlRepository(
            ApplicationDbContext context) 
            : base(context)
        {
        }

        public override async Task<List<CustomUrlModel>> GetAll(Guid? id)
        {
            var entity = await _context.CustomUrls
                .Where(e => e.PortfolioProfile.Id == id).ToListAsync();
            
            return entity;
        }

    }
}

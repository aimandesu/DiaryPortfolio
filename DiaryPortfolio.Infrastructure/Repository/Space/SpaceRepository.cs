using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.ISpaceRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.Space
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ApplicationDbContext _context;

        public SpaceRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSpace(SpaceModel space)
        {
            _context.Spaces.Add(space);
        }
    }
}

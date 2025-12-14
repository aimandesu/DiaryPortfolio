using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Application.IRepository.IMediaRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.Media
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MediaRepository> _logger;

        public MediaRepository(
            ApplicationDbContext context,
            ILogger<MediaRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Pagination<MediaModel>> GetAllMediaByUsername(
            QuerySearchObject querySearchObject, 
            Guid userId)
        {
           _logger.LogInformation("Fetching media for user with ID: {UserId}", userId);

            return await _context.Medias
               .Join(_context.Spaces,
                    m => m.SpaceId,
                    s => s.Id,
                    (m, s) => new { m, s })
                .Where(x => x.s.UserId == userId)
                .Select(x => x.m)
                .OrderByDescending(m => m.CreatedAt)
                .PaginateAsync(
                    querySearchObject.PageNumber, 
                    querySearchObject.PageSize);
        }
    }
}

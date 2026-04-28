using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DiaryPortfolio.Infrastructure.Services
{
    internal class SelectionHelper : ISelectionHelper
    {
        private readonly ApplicationDbContext _context;

        public SelectionHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> GetSelectionIdAsync<TEnum>(
            TEnum enumValue, 
            CancellationToken cancellationToken = default) where TEnum : Enum
        {
            var enumString = enumValue.ToString();

            var id = await _context.Selections
                .Where(t => t.Selection == enumString)
                .Select(t => t.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return id;
        }

        public async Task<SelectionModel?> GetSelectionResultAsync<TEnum>(
            TEnum enumValue, 
            CancellationToken cancellationToken = default) where TEnum : Enum
        {
            var enumString = enumValue.ToString();

            return await _context.Selections
                .Include(t => t.Type)
                .Where(t => t.Selection == enumString)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> GetSelectionSpaceId(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var result = await _context.Spaces
                    .Where(e => e.Id == id)
                    .Select(e => e.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}

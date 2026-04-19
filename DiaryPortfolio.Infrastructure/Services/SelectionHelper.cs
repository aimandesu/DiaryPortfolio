using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
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

        public async Task<SelectionResult?> GetSelectionResultAsync<TEnum>(
            TEnum enumValue, 
            CancellationToken cancellationToken = default) where TEnum : Enum
        {
            var enumString = enumValue.ToString();

            var result = await _context.Selections
                .Where(t => t.Selection == enumString)
                .Select(t => new SelectionResult(t.Id, t.TypeId)) 
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}

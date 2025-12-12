using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Application.Common
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int LastPage { get; set; }
    }

    public static class PaginationExtensions
    {
        public static async Task<Pagination<T>> PaginateAsync<T>(
            this IQueryable<T> query,
            int currentPage,
            int perPage
        )
        {
            var total = await query.CountAsync();
            var skip = (currentPage - 1) * perPage;

            var pagedData = await query
                .Skip(skip)
                .Take(perPage)
                .ToListAsync();

            return new Pagination<T>
            {
                Data = pagedData,
                CurrentPage = currentPage,
                PerPage = perPage,
                Total = total,
                LastPage = (int)Math.Ceiling(total / (double)perPage)
            };
        }
    }
}

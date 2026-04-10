using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public virtual async Task<T?> Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return null;

            _context.Set<T>().Remove(entity);
            return entity;
        }

        public virtual async Task<T?> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}

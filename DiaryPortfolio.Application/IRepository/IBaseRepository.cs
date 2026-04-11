using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<T?> Get(Guid id);
        Task<List<T>> GetAll(Guid? id = null);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T?> Delete(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{    
    public interface IRepository<T> where T : class, new()
    {
        T Add(T entity);
        bool Edit(T entity);
        T Find(params object[] keys);
        bool Delete(T entity);
        IEnumerable<T> Get();
        List<T> GetList();
        bool Any(Expression<Func<T, bool>> predicate);
        int Save();

        Task<T> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<T> FindAsync(params object[] keys);
        Task<bool> DeleteAsync(T entity);
        IAsyncEnumerable<T> GetAsync();
        Task<List<T>> GetListAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveAsync();
    }
}

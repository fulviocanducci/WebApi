using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{

    public abstract class Repository<T, TContext> : IRepository<T> 
        where T: class, new()
        where TContext: DbContext
    {
        public TContext Context { get; }
        public Repository(TContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            Context.Add(entity);
            Save();
            return entity;
        }

        public IEnumerable<T> Get()
        {
            return Context.Set<T>().AsEnumerable();
        }

        public bool Edit(T entity)
        {
            Context.Update(entity);
            return Save() > 0;
        }

        public T Find(params object[] keys)
        {
            return Context.Find<T>(keys);
        }

        public bool Delete(T entity)
        {
            Context.Remove(entity);
            return (Save()) > 0;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Any(predicate);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Context.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<bool> EditAsync(T entity)
        {
            Context.Update(entity);
            return (await SaveAsync()) > 0;
        }

        public async Task<T> FindAsync(params object[] keys)
        {
            return await Context.FindAsync<T>(keys);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            Context.Remove(entity);
            return (await SaveAsync()) > 0;
        }

        public IAsyncEnumerable<T> GetAsync()
        {
            return Context.Set<T>().AsAsyncEnumerable<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().AnyAsync(predicate);
        }
        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public List<T> GetList()
        {
            return Context.Set<T>().ToList();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate is null ?
                Context.Set<T>().Count() :
                Context.Set<T>().Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate is null ?
                await Context.Set<T>().CountAsync() :
                await Context.Set<T>().CountAsync(predicate);
        }
    }
}

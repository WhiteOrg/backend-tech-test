using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.DataLayer.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly LibraryDataContext Context;

        public BaseRepository(LibraryDataContext context)
        {
            Context = context;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T updatedEntity)
        {
            Context.Set<T>().Update(updatedEntity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var itemtoRemove = Get(x => x.Id == id);
            Delete(itemtoRemove);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entityToDelete = Get(predicate);
            Delete(entityToDelete);
        }

        public virtual async Task<T> FetchAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> FetchAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<int> TotalCountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        private T Get(Expression<Func<T, bool>> predicate)
        {
            return (T) Context.Set<T>().FirstOrDefault(predicate);
        }
    }
}
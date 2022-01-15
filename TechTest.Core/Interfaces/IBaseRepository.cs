using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechTest.Core.Entities;

namespace TechTest.Core.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity updatedEntity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FetchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FetchAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> TotalCountAsync();
    }
}
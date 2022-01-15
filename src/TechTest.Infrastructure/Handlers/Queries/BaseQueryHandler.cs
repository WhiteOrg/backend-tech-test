using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.Infrastructure.Handlers.Queries
{
    public abstract class BaseQueryHandler<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected abstract IBaseRepository<T> Repository { get; }

        protected BaseQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected async Task<List<T>> HandleGet(int id)
        {
            if (id <= 0)
            {
                return await GetAll();
            }

            return new List<T>() { await GetById(id) };
        }

        private protected virtual async Task<T> GetById(int id)
        {
            return await Repository.GetAsync(id);
        }

        private protected virtual async Task<List<T>> GetAll()
        {
            return (await Repository.GetAllAsync()).ToList();
        }
    }
}
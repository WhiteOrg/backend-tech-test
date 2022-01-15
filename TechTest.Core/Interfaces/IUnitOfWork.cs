using System.Threading;
using System.Threading.Tasks;
using TechTest.Core.Entities;

namespace TechTest.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Author> AuthorRepo { get; }
        IBookRepository BookRepo { get; }
        
        Task<int> SaveAsync();
        Task<int> SaveAsync(CancellationToken token);
    }
}
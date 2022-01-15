using System.Threading.Tasks;
using TechTest.Core.Entities;

namespace TechTest.Core.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task IncreaseSalesCount(int bookId, int increaseAmount = 1);
    }
}
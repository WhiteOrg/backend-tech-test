using System.Threading.Tasks;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.DataLayer.Repositories
{
    internal class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDataContext context) : base(context)
        {
        }

        public async Task IncreaseSalesCount(int bookId, int increaseAmount = 1)
        {
            var book = await GetAsync(bookId);
            book.SalesCount++;
            Update(book);
            
        }
    }
}
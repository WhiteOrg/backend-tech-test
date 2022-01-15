using System.Threading;
using System.Threading.Tasks;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.DataLayer.Repositories;

namespace TechTest.DataLayer
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDataContext _context;

        public UnitOfWork(LibraryDataContext dataContext, IBookRepository bookRepository,
            IBaseRepository<Author> authorRepository)
        {
            _context = dataContext;
            AuthorRepo = authorRepository;
            BookRepo = bookRepository;
        }

        /// <summary>
        ///     ctor used for UnitTests
        /// </summary>
        /// <param name="dataContext"></param>
        public UnitOfWork(LibraryDataContext dataContext)
        {
            _context = dataContext;
            AuthorRepo ??= new BaseRepository<Author>(dataContext);
            BookRepo ??= new BookRepository(dataContext);
        }

        public IBaseRepository<Author> AuthorRepo { get; }

        public IBookRepository BookRepo { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}
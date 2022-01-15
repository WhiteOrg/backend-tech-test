using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Infrastructure.Handlers.Queries
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                return await GetAll();
            }

            var book = await GetById(request.Id);
            return book == null
                ? null
                : new List<Book>() { book };
        }

        private protected virtual async Task<Book> GetById(int id)
        {
            return await _unitOfWork.BookRepo.GetAsync(id);
        }

        private protected virtual async Task<List<Book>> GetAll()
        {
            return (await _unitOfWork.BookRepo
                .GetAllAsync()).ToList();
        }
    }
}
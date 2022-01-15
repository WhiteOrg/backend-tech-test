using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Infrastructure.Handlers.Queries
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, List<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                return await GetAll();
            }

            var author = await GetById(request.Id);
            return author == null
                ? null
                : new List<Author>(){author};
            }

        private protected virtual async Task<Author> GetById(int id)
        {
            return await _unitOfWork.AuthorRepo.GetAsync(id);
            
        }

        private protected virtual async Task<List<Author>> GetAll()
        {
            return (await _unitOfWork.AuthorRepo.GetAllAsync()).ToList();
        }

    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Infrastructure.Handlers.Commands
{
    public class SellBookCommandHandler : IRequestHandler<SellBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SellBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepo.GetAsync(request.Id);
            book.SalesCount++;
            _unitOfWork.BookRepo.Update(book);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
        
    }
}
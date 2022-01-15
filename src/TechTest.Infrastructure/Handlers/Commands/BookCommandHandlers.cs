using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Infrastructure.Handlers.Commands
{
    public class BookCommandHandlers : BaseCommandHandler, IRequestHandler<CreateBookCommand, int>
    , IRequestHandler<UpdateBookCommand, BookDto>, IRequestHandler<DeleteBookCommand>
    {
        public BookCommandHandlers(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = new Book() { Title = request.Title };
            await UnitOfWork.BookRepo.AddAsync(newBook);
            await UnitOfWork.SaveAsync(cancellationToken);
            return newBook.Id;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await UnitOfWork.BookRepo.GetAsync(request.Id);
            if (itemToUpdate == null)
            {
                throw new Exception("Item not found, please ensure Item Id provided is valid");
            }

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                itemToUpdate.Title = request.Title;
            }
            
            UnitOfWork.BookRepo.Update(itemToUpdate);
            await UnitOfWork.SaveAsync(cancellationToken);
            return new BookDto()
            {
                Id = itemToUpdate.Id, 
                Title = itemToUpdate.Title,
                SalesCount = itemToUpdate.SalesCount
            };
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            UnitOfWork.BookRepo.Delete(request.Id);
            await UnitOfWork.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
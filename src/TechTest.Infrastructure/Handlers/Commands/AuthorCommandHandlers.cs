using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Infrastructure.Handlers.Commands
{
    public class AuthorCommandHandlers : BaseCommandHandler, IRequestHandler<CreateAuthorCommand>, 
        IRequestHandler<UpdateAuthorCommand, Author>, IRequestHandler<DeleteAuthorCommand>
    {
        public AuthorCommandHandlers(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = new Author() { Name = request.Name };
            await UnitOfWork.AuthorRepo.AddAsync(newAuthor);
            await UnitOfWork.SaveAsync(cancellationToken);
            return Unit.Value;
        }

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = await UnitOfWork.AuthorRepo.GetAsync(request.Id);
            if (authorToUpdate == null)
            {
                throw new Exception("Author not found, please ensure Author Id is provided and is valid.");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                authorToUpdate.Name = request.Name;
            }

            UnitOfWork.AuthorRepo.Update(authorToUpdate);
            await UnitOfWork.SaveAsync(cancellationToken);
            return authorToUpdate;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            UnitOfWork.AuthorRepo.Delete(request.Id);
            await UnitOfWork.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
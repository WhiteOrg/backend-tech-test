using MediatR;
using TechTest.Core.Entities;

namespace TechTest.Core.Models
{
    public class SellBookCommand : IRequest<Book>
    {
        public SellBookCommand(int bookId)
        {
            Id = bookId;
        }

        public int Id { get;}
    }
}
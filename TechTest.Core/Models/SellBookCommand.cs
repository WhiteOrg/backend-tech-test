using MediatR;

namespace TechTest.Core.Models
{
    public class SellBookCommand : IRequest
    {
        public SellBookCommand(int bookId)
        {
            Id = bookId;
        }

        public int Id { get;}
    }
}
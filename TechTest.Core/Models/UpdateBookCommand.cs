using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.Core.Models
{
    public class UpdateBookCommand : IUpdateCommand, IRequest<BookDto>
    {
        public string Title { get; set; }

        public int Id { get; set; }
    }
}
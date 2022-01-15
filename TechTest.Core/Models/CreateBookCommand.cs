using MediatR;

namespace TechTest.Core.Models
{
    public class CreateBookCommand : IRequest
    {
        public string Title { get; set; }
    }
}
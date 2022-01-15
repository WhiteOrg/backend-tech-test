using MediatR;

namespace TechTest.Core.Models
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }
    }
}
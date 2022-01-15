using MediatR;

namespace TechTest.Core.Models
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
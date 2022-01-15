using MediatR;

namespace TechTest.Core.Models
{
    public class CreateAuthorCommand : IRequest
    {
        public string Name { get; set; }
    }
}
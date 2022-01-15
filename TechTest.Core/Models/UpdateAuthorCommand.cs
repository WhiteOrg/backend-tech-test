using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.Core.Models
{
    public class UpdateAuthorCommand : IUpdateCommand, IRequest<Author>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
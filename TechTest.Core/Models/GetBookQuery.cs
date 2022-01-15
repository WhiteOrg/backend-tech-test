using System.Collections.Generic;
using MediatR;
using TechTest.Core.Entities;

namespace TechTest.Core.Models
{
    public class GetAuthorQuery : IRequest<List<Author>>, IRequest<Author>
    {
        public GetAuthorQuery(int authorId = 0)
        {
            Id = authorId;
        }
        public int Id { get; }
    }
}
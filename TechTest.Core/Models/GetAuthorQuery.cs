using System.Collections.Generic;
using MediatR;
using TechTest.Core.Entities;

namespace TechTest.Core.Models
{
    public class GetBookQuery : IRequest<List<Book>>
    {
        public GetBookQuery(int bookId)
        {
            Id = bookId;
        }
        public int Id { get; }
    }
}
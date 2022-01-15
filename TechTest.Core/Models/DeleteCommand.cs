using TechTest.Core.Interfaces;

namespace TechTest.Core.Models
{
    public abstract class DeleteCommand : IDeleteCommand
    {
        public DeleteCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public sealed class DeleteAuthorCommand : DeleteCommand
    {
        public DeleteAuthorCommand(int id) : base(id)
        {
        }
    }

    public sealed class DeleteBookCommand : DeleteCommand
    {
        public DeleteBookCommand(int id) : base(id)
        {
        }
    }
}
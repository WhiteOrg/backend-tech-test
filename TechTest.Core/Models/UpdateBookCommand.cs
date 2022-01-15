using TechTest.Core.Interfaces;

namespace TechTest.Core.Models
{
    public class UpdateBookCommand : IUpdateCommand
    {
        public string Title { get; set; }

        public int Id { get; set; }
    }
}
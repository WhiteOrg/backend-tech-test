using System.Reflection.Metadata;
using TechTest.Core.Interfaces;

namespace TechTest.Core.Models
{
    public class UpdateAuthorCommand : IUpdateCommand
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
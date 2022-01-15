using System.Collections.Generic;

namespace TechTest.Core.Entities
{
    public class Author : BaseEntity
    {
        public IList<Book> Books { get; set; }
        public virtual string Name { get; set; }
    }
}
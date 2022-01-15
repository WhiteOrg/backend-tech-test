namespace TechTest.Core.Entities
{
    public class Book : BaseEntity
    {
        public Author Author { get; set; }

        public int SalesCount { get; set; }
    }
}
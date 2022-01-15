using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTest.Core.Entities;

namespace TechTest.DataLayer.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired();

            /*
             * Book can have One Author
             * Author can have many Books
             * On deletion of Author all Books are deleted.
             */
            builder.HasMany(book => book.Books)
                .WithOne(auth => auth.Author)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
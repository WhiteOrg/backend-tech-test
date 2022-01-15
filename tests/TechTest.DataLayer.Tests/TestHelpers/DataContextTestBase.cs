using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using TechTest.Core.Entities;

namespace TechTest.DataLayer.Tests.TestHelpers
{
    public abstract class DataContextTestBase
    {
        internal DataContextTestBase(DbContextOptions<LibraryDataContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Books = new List<Book>();
            Authors = new List<Author>();

            InternalSeed();
        }

        private void InternalSeed()
        {
            using (var context = new LibraryDataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Books = DefineBooks() ?? new List<Book>();
                Authors = DefineAuthors() ?? new List<Author>();

                context.AddRange(Books);
                context.AddRange(Authors);
                context.SaveChanges();
            }
        }

        protected virtual List<Author> DefineAuthors()
        {
            return null;
        }

        protected virtual List<Book> DefineBooks()
        {
            return null;
        }

        protected DbContextOptions<LibraryDataContext>
            ContextOptions { get; set; }

        public List<Book> Books { get; set; }

        public List<Author> Authors { get; set; }

        protected static DbConnection AsInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}
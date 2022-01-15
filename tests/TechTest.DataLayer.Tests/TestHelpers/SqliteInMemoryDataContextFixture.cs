using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;

namespace TechTest.DataLayer.Tests.TestHelpers
{
    public class SqliteInMemoryDataContextFixture : DataContextTestBase, IDisposable
    {
        private readonly DbConnection _connection;

        public SqliteInMemoryDataContextFixture() : base(
            new DbContextOptionsBuilder<LibraryDataContext>()
                .UseSqlite(AsInMemoryDatabase())
                .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            UoW = new UnitOfWork(new LibraryDataContext(ContextOptions));
        }

        public IUnitOfWork UoW { get; }


        public void Dispose()
        {
            ContextOptions = null;
            _connection?.Dispose();
        }

        protected override List<Author> DefineAuthors()
        {
            var authorData = new List<Author>();
            for (var i = 1; i <= 3; i++)
            {
                authorData.Add(new Author
                {
                    Name = $"Author {i}",
                    Books = new List<Book>
                    {
                        new Book() { Title = $"BookName {i}" }, new Book() { Title = $"BookName {i + 1}" }
                    }
                });
            }

            return authorData;
        }
    }
}
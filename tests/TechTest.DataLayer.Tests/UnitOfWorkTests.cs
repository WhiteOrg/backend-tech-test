using System.Linq;
using System.Threading.Tasks;
using TechTest.Core.Entities;
using TechTest.DataLayer.Tests.TestHelpers;
using TechTest.DataLayer.Tests.xUnitExtensions;
using Xunit;

namespace TechTest.DataLayer.Tests
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    [TestCaseOrderer("TechTest.DataLayer.Tests.xUnitExtensions.PriorityOrderer", "TechTest.DataLayer.Tests")]
    public class UnitOfWorkTests : IClassFixture<SqliteInMemoryDataContextFixture>
    {
        private readonly SqliteInMemoryDataContextFixture _fixture;

        public UnitOfWorkTests(SqliteInMemoryDataContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ShouldReturnCorrectAuthor(int authorId)
        {
            var author = await _fixture.UoW.AuthorRepo.GetAsync(authorId);
            Assert.Equal(authorId, author.Id);
        }

        
        [Fact]
        public async Task GetAllAsync_Returns_3Items()
        {
            var results = await _fixture.UoW.AuthorRepo.GetAllAsync();
            Assert.Equal(3, results.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Async_Book_Fetch_BySpecificBoxName(int authorId)
        {
            var author = await _fixture.UoW.AuthorRepo.FetchAsync(x => x.Name.Equals($"Author {authorId}"));
            Assert.NotNull(author);
            Assert.Equal($"Author {authorId}", author.Name);
        }

        [Fact, Priority(1)]
        public async Task AddAsync_Author_SuccesfulCreationOf_Author()
        {
            await _fixture.UoW.AuthorRepo.AddAsync(new Author
            {
                Name = nameof(AddAsync_Author_SuccesfulCreationOf_Author)
            });
            var added = await _fixture.UoW.SaveAsync();
            Assert.Equal(1, added);
        }

        [Theory, Priority(2)]
        [InlineData(nameof(AddAsync_Author_SuccesfulCreationOf_Author))]
        public async Task FetchAsync_Returns_RecentlyCreatedAuthor(string authorName)
        {
            var box = await _fixture.UoW.AuthorRepo.FetchAsync(b => b.Name == authorName);
            Assert.NotNull(box);
            Assert.Equal(authorName, box.Name);
        }

        [Fact, Priority(3)]
        public async Task Delete_Given_AuthorId_CreatedInPriority1_ThenCountShouldBe_3()
        {
            _fixture.UoW.AuthorRepo.Delete(4);
            await _fixture.UoW.SaveAsync();
            Assert.Equal(3, await _fixture.UoW.AuthorRepo.TotalCountAsync());
        }

        [Fact, Priority(4)]
        public async Task TotalCountAsync_Returns_3()
        {
            Assert.Equal(3, await _fixture.UoW.AuthorRepo.TotalCountAsync());
        }
    }
}

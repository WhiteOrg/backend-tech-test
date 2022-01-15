using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tappau.DateTimeProvider.Abstractions;
using TechTest.Core.Entities;

namespace TechTest.DataLayer
{
    public class LibraryDataContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public LibraryDataContext(DbContextOptions<LibraryDataContext> options, IDateTimeProvider dateTimeProvider) :
            base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public LibraryDataContext()
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDataContext).Assembly);
        }

        public override int SaveChanges()
        {
            SetCreatedModifiedOnValues();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            SetCreatedModifiedOnValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetCreatedModifiedOnValues()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added
                    or EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                var dateTime = _dateTimeProvider.UtcNow;
                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        ((BaseEntity)entityEntry.Entity).ModifiedOn = dateTime;
                        break;
                    case EntityState.Added:
                        ((BaseEntity)entityEntry.Entity).CreatedOn = dateTime;
                        ((BaseEntity)entityEntry.Entity).ModifiedOn = null;
                        break;
                }
            }
        }
    }
}
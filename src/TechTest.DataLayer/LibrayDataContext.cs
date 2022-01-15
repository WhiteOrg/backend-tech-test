using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTest.Core.Entities;

namespace TechTest.DataLayer
{
    internal class LibrayDataContext : DbContext
    {
        public LibrayDataContext(DbContextOptions options) : base(options)
        {
        }

        public LibrayDataContext(DbContextOptions<LibrayDataContext> options) : base(options)
        {
            
        }

        public LibrayDataContext()
        {
            
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibrayDataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetCreatedModifiedOnValues();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetCreatedModifiedOnValues();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetCreatedModifiedOnValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            SetCreatedModifiedOnValues();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetCreatedModifiedOnValues()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        ((BaseEntity) entityEntry.Entity).ModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        ((BaseEntity) entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                        ((BaseEntity) entityEntry.Entity).ModifiedOn = null;
                        break;
                }
            }
        }
    }
}
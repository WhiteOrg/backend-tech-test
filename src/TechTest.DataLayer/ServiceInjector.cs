using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechTest.Core.Interfaces;
using TechTest.DataLayer.Repositories;

namespace TechTest.DataLayer
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LibraryDataContext>(options =>
            {
                options.UseSqlite(connectionString, builder => builder.CommandTimeout(30));
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
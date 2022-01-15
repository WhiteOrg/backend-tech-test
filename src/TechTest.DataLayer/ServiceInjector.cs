using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TechTest.DataLayer
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LibrayDataContext>(options =>
            {
                options.UseInMemoryDatabase($"Library{Guid.NewGuid()}");
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            return services;
        }
    }
}
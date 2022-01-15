using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TechTest.Core.Models;
using TechTest.Infrastructure.Handlers.Commands;

namespace TechTest.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateAuthorCommand), typeof(AuthorCommandHandlers));
            return services;
        }
    }
}
﻿using Microsoft.Extensions.DependencyInjection;

namespace TechTest.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureMapping
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}

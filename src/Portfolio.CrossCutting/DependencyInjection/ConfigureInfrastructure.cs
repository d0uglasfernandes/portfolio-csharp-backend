using Portfolio.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Data.DataModule;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLServer"),
                    b => b.MigrationsAssembly(typeof(PortfolioContext).Assembly.FullName)));

            services.AddScoped<IDataModule, DataModule<PortfolioContext>>();
            services.AddScoped<IDataModuleDBPortfolio, DataModuleDBPortfolio>();

            return services;
        }
    }
}
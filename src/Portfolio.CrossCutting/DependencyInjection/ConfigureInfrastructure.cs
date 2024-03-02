using Portfolio.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Data.DataModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsEnvironment("Testing"))
            {
                services.AddDbContext<PortfolioContext>(options =>
                    options.UseSqlServer(
                        "Server=localhost\\SQL2019; Database=DBAps_Test; User Id=sa; Password=SIMERP;TrustServerCertificate=True;",
                        b => b.MigrationsAssembly(typeof(PortfolioContext).Assembly.FullName)));
            }
            else
            {
                services.AddDbContext<PortfolioContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("SQLServer"),
                        b => b.MigrationsAssembly(typeof(PortfolioContext).Assembly.FullName)));
            }

            services.AddScoped<IDataModule, DataModule<PortfolioContext>>();
            services.AddScoped<IDataModuleDBPortfolio, DataModuleDBPortfolio>();
            services.AddHttpClient();

            return services;
        }
    }
}
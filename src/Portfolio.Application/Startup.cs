using Portfolio.Application.ErrorHandling;
using Portfolio.CrossCutting.DependencyInjection;
using Portfolio.Data.Context;
using Portfolio.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Portfolio.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransitRabbitMQ();

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerConfiguration();

            services.AddIdentityInfrastructure(_configuration);

            services.AddFluentValidationConfiguration();

            services.AddMediatRConfiguration();

            services.AddAutoMapperConfiguration();
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseRouting();

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseSwaggerConfiguration();
            }

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            application.UseMiddleware(typeof(ErrorHandlingMiddleware));

            if (_configuration.GetValue<bool>("Migrations:Apply"))
            {
                using (var service = application.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<PortfolioContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
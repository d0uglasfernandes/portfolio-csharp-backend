using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureMassTransitRabbitMQ
    {
        public static IServiceCollection AddMassTransitRabbitMQ(this IServiceCollection services, string host, string username, string password)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<CompanyIntegrationCommandConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", c =>
                    {
                        c.Username(username);
                        c.Password(password);
                    });

                    cfg.ReceiveEndpoint("CompanyQueue", ec =>
                    {
                        //ec.ConfigureConsumer<CompanyIntegrationCommandConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
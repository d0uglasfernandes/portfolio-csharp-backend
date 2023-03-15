using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureMassTransitRabbitMQ
    {
        public static IServiceCollection AddMassTransitRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<CompanyIntegrationCommandConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
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